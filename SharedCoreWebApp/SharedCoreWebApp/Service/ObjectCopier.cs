using Newtonsoft.Json;
using SharedCoreWebApp.Service;

namespace SharedCoreWebApp.GlobalHelpers
{
    public class ObjectCopier
    {
        public static T Copy<T>(T record) where T : class, IEntity
        {
            var json = JsonConvert.SerializeObject(record, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });

            var copy=JsonConvert.DeserializeObject<T>(json);
            return copy;
        }
    }
}