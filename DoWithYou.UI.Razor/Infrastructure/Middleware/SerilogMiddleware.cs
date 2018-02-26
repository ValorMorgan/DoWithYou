using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DoWithYou.Shared.Constants;
using DoWithYou.Shared.Extensions;
using DoWithYou.UI.Razor.Utilities;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace DoWithYou.UI.Razor.Infrastructure.Middleware
{
    class SerilogMiddleware
    {
        #region VARIABLES
        private readonly RequestDelegate _next;
        #endregion

        #region CONSTRUCTORS
        public SerilogMiddleware(RequestDelegate next)
        {
            Log.Logger.LogEventVerbose(LoggerEvents.CONSTRUCTOR, "Constructing {Class}", nameof(SerilogMiddleware));

            _next = next ?? throw new ArgumentNullException(nameof(next));
        }
        #endregion

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            Stopwatch sw = Stopwatch.StartNew();

            await _next(httpContext);
            sw.Stop();

            RequestLogger logger = new RequestLogger();
            logger.LogRequest(httpContext, sw.Elapsed.TotalMilliseconds);
        }
    }
}