using System.Collections.Generic;
using SharedCoreWebApp.GlobalHelpers;

namespace SharedCoreWebApp.Models
{
    public class MyResponse<T>
    {
        public MyJsonResponseType Type { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }

    }
    public class MyDataTableResponse<T> : MyResponse<T>
    {
        public List<T> List { get; set; }
        public int Total { get; set; }
        public int? LastSkip { get; set; }
        public int LastTake { get; set; }
        
    }

    public class MyEntityResponse<T>:MyResponse<T>
    {
        public T Single { get; set; }
    }

    
}