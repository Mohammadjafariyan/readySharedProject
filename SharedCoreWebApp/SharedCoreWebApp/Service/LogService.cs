using System;
using SharedCoreWebApp.ContextFactory;
using SharedCoreWebApp.GlobalHelpers;
using SharedCoreWebApp.Models;

namespace SharedCoreWebApp.Service
{
    public class LogService:GenericService<Log>
    {
        public LogService(ContextFactoryService contextFactoryService) : base(contextFactoryService)
        {
        }

        public void Log(Exception exception)
        {
            try
            {

                Create(new Log
                {
                    Exception = MyGlobal.RecursiveExecptionMsg(exception)
                });
            }
            catch (Exception e)
            {
                // ignored
            }
        }

    }
}