using Microsoft.EntityFrameworkCore;

namespace SharedCoreWebApp.ContextFactory
{
    public class ContextFactoryService
    {
        
        
        public AbstractDbContext GetDbContext(string name)
        {
            
            // injected context
            return new AbstractDbContext();
        }
    }
}