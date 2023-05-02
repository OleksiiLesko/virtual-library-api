using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Net;
using ILogger = Serilog.ILogger;

namespace VirtualLibraryAPI.Library
{
    public class Middleware
    {
        public RequestDelegate _requestDelegate;
        private readonly ILogger<Middleware> _logger;

        public Middleware(RequestDelegate requestDelegate, ILogger<Middleware> logger)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            _logger.LogError(ex.ToString());
            var errorMessageObject = new { Message = ex.Message, Code = "system_error" };

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(errorMessage);
        }
    }
}
