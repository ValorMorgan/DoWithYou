using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace DoWithYou.Shared.Core.Middleware
{
    public class BadResponseMiddleware
    {
        #region VARIABLES
        private readonly RequestDelegate _next;
        #endregion

        #region CONSTRUCTORS
        public BadResponseMiddleware(RequestDelegate next)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(BadResponseMiddleware));

            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        #endregion

        public async Task Invoke(HttpContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // If headers already sent to client or we fail to handle response
                if (context.Response.HasStarted || !await TryHandleBadResponseAsync(context, ex))
                    throw;
            }
        }

        #region PRIVATE
        private BadResponse GetResponse(HttpContext context, Exception ex)
        {
            var response = new BadResponse
            {
                Error = new Error
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                }
            };

            if (ex.InnerException == null)
                return response;

            IList<AdditionalError> errors = new List<AdditionalError>();
            Exception inner = ex.InnerException;

            while (inner != null)
            {
                errors.Add(new AdditionalError
                {
                    Domain = context.Request.Path,
                    Message = inner.Message,
                    Reason = inner.GetType().Name
                });

                inner = inner.InnerException;
            }

            response.Error.Errors = errors.ToArray();
            return response;
        }

        private string GetResponseBody(BadResponse response) =>
            JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });

        private async Task<bool> TryHandleBadResponseAsync(HttpContext context, Exception ex)
        {
            try
            {
                Log.Logger.LogEventWarning(LoggerEvents.RESPONSE, "Bad response with status code {Status} being handled.", context.Response.StatusCode);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = @"application/json";

                var response = GetResponse(context, ex);
                string body = GetResponseBody(response);

                await context.Response.WriteAsync(body);
            }
            catch
            {
                Log.Logger.LogEventError(LoggerEvents.RESPONSE, "Failed to handle bad response.");

                return false;
            }

            return true;
        }
        #endregion
    }

    class BadResponse
    {
        #region PROPERTIES
        public Error Error { get; set; }
        #endregion
    }

    class Error
    {
        #region PROPERTIES
        public int Code { get; set; }

        public AdditionalError[] Errors { get; set; }

        public string Message { get; set; }
        #endregion
    }

    class AdditionalError
    {
        #region PROPERTIES
        public string Domain { get; set; }

        public string Message { get; set; }

        public string Reason { get; set; }
        #endregion
    }
}