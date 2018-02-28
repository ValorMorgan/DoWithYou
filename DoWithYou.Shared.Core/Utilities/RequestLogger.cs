using System;
using System.Collections.Generic;
using System.Linq;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;

namespace DoWithYou.Shared.Core.Utilities
{
    public class RequestLogger
    {
        #region VARIABLES
        private static readonly ILogger _log = Log.ForContext<RequestLogger>();
        #endregion

        public void LogRequest(HttpContext httpContext, double elapsedTime = 0)
        {
            try
            {
                int statusCode = httpContext.Response?.StatusCode ?? StatusCodes.Status400BadRequest;

                LogEventLevel level = statusCode >= StatusCodes.Status500InternalServerError ?
                    LogEventLevel.Error :
                    LogEventLevel.Information;

                ILogger log = level == LogEventLevel.Error ?
                    GetLoggerForErrorContext(httpContext) :
                    _log;

                log.LogEvent(level, LoggerEvents.REQUEST, LoggerTemplates.LOG_WEB_REQUEST, httpContext.Request.Method, httpContext.Request.Path, statusCode, elapsedTime);
            }
            catch (Exception ex)
            {
                LogException(ex, httpContext, elapsedTime);
                throw;
            }
        }

        #region PRIVATE
        private static ILogger GetLoggerForErrorContext(HttpContext httpContext)
        {
            HttpRequest request = httpContext.Request;

            IDictionary<string, string> headers = request.Headers.ToDictionary(
                h => h.Key,
                h => h.Value.ToString());

            ILogger result = _log
                .ForContext("RequestHeaders", headers, destructureObjects: true)
                .ForContext("RequestHost", request.Host)
                .ForContext("RequestProtocol", request.Protocol);

            if (!request.HasFormContentType)
                return result;

            IDictionary<string, string> requestForm = request.Form.ToDictionary(
                v => v.Key,
                v => v.Value.ToString());

            return result.ForContext("RequestForm", requestForm);
        }

        private static void LogException(Exception ex, HttpContext httpContext, double elapsedTime)
        {
            try
            {
                GetLoggerForErrorContext(httpContext)
                    .LogEventError(ex, LoggerEvents.REQUEST, LoggerTemplates.LOG_WEB_REQUEST, httpContext.Request.Method, httpContext.Request.Path, 500, elapsedTime);
            }
            catch
            {
                /* Digest */
            }
        }
        #endregion
    }
}