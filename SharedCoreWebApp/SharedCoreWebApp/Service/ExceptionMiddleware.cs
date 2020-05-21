using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SharedCoreWebApp.GlobalHelpers;
using SharedCoreWebApp.Models;

namespace SharedCoreWebApp.Service
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private LogService _logger;

        public ExceptionMiddleware(RequestDelegate next, LogService logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(
                new MyEntityResponse<ErrorViewModel>()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = MyGlobal.RecursiveExecptionMsg(exception)
                }.ToString());
        }
    }

    public class SingletonSetterMiddleware
    {
        private readonly RequestDelegate _next;
        private LogService _logger;
        private readonly CurrentRequestInfoSingleton _currentRequestInfoSingleton;

        public SingletonSetterMiddleware(RequestDelegate next, LogService logger,
            CurrentRequestInfoSingleton currentRequestInfoSingleton)
        {
            _logger = logger;
            _currentRequestInfoSingleton = currentRequestInfoSingleton;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            _currentRequestInfoSingleton.SetValues(httpContext);
            await _next(httpContext);
        }
    }
}