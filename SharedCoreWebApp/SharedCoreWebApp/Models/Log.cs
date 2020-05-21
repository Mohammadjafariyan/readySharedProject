using System;
using SharedCoreWebApp.Service;

namespace SharedCoreWebApp.Models
{
    public class Log:IEntity
    {
        public Log()
        {
            CreateDate=DateTime.Now;
        }
        
        public string Exception { get; set; }
        public int Id { get; set; }
        public int? NextId { get; set; }
        public int? PrevId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}