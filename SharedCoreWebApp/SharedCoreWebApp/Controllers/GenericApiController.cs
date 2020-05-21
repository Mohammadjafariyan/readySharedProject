using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SharedCoreWebApp.GlobalHelpers;
using SharedCoreWebApp.Models;
using SharedCoreWebApp.Service;

namespace SharedCoreWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class GenericApiController<T> : ControllerBase where T:class,IEntity
    {
        private readonly IService<T> _service;

        protected GenericApiController(IService<T> service)
        {
            _service = service;
        }

        // GET: Admin/Generic
        [HttpGet("[action]")]
        public virtual MyDataTableResponse<T> Index(int? take, int? skip)
        {
            take ??= 20;
            MyDataTableResponse<T> response = _service.GetAsPaging(take.Value, skip);
            return response;
        }

        [HttpGet("[action]")]
        public virtual  MyEntityResponse<T> Detail(int id)
        {
            MyEntityResponse<T> response = _service.GetById(id);
            return response;
        }

        [HttpPost("[action]")]
        public virtual  MyEntityResponse<int> Save(T model)
        {
            MyEntityResponse<int> response = _service.Save(model);
            return response;
        }


        [HttpPost]
        public virtual  MyEntityResponse<T>  Delete(int id)
        {
            MyEntityResponse<T> response = _service.DeleteById(id);
            return response;
        }

       
    }
}