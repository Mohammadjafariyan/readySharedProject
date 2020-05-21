using Microsoft.AspNetCore.Builder;

namespace SharedCoreWebApp.Service
{
    public static class MyGlobalExtentions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
        
        public static void ConfigureGlobalSingletons(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}