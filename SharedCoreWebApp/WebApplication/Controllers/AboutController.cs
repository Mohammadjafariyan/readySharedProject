using Microsoft.AspNetCore.Mvc;
using SharedCoreWebApp.Service;
using WebApplication.Service;

namespace WebApplication.Controllers
{
    public class AboutController : Controller
    {
        private readonly TestService _service;
        private readonly HiService _hiService;

        public AboutController(TestService service,HiService hiService)
        {
            _service = service;
            _hiService = hiService;
        }

        // GET
        public IActionResult Index()
        {
            string hi=_service.Hi();
            string bye = _hiService.SayGoodBye();
            return View();
        }
    }
}