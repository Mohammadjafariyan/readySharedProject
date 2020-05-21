using Microsoft.EntityFrameworkCore;
using SharedCoreWebApp.Models;

namespace SharedCoreWebApp.ContextFactory
{
    public class AbstractDbContext:DbContext
    {
        
        
        
        public DbSet<Log> Logs { get; set; }
    }
}