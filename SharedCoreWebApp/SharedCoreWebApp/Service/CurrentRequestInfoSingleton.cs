using Microsoft.AspNetCore.Http;

namespace SharedCoreWebApp.Service
{
    public class CurrentRequestInfoSingleton
    {      
        public CurrentRequestInfo CurrentRequestInfo { get; set; }

        public void SetValues(HttpContext httpContext)
        {
            if (CurrentRequestInfo)
            {
                
            }
        }
    }
}