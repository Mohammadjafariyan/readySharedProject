using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using SharedCoreWebApp.GlobalHelpers;
using SharedCoreWebApp.Models;
using SharedCoreWebApp.Service;

namespace SharedCoreWebApp.Controllers
{
    public abstract class GenericController<T> : Controller where T:class,IEntity
    {
        private readonly IService<T> _service;

        protected GenericController(IService<T> service)
        {
            _service = service;
        }

        // GET: Admin/Generic
        public virtual ActionResult Index(int? take, int? skip)
        {
            take ??= 20;
            MyDataTableResponse<T> response = _service.GetAsPaging(take.Value, skip);
            return View(response);
        }

        public virtual  ActionResult Detail(int id)
        {
            MyEntityResponse<T> response = _service.GetById(id);
            return View(response);
        }

        [HttpPost]
        public virtual  ActionResult Save(T model)
        {
            MyEntityResponse<int> response = _service.Save(model);
            return View(response);
        }


        [HttpPost]
        public virtual  ActionResult Delete(int id)
        {
            MyEntityResponse<T> response = _service.DeleteById(id);
            return View(response);
        }

       
    }
}