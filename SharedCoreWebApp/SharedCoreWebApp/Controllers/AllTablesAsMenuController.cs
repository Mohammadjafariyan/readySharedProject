using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SharedCoreWebApp.Service;

namespace SharedCoreWebApp.Controllers
{
    public class AllTablesAsMenuController : Controller
    {
        private readonly GenericTablesService _genericTablesService;

        public AllTablesAsMenuController(GenericTablesService genericTablesService)
        {
            _genericTablesService = genericTablesService;
        }

        private Dictionary<string,string> _tableNamesDictionary=new Dictionary<string, string>
        {
            {"Feeds","فید ها"},
            {"SocialChannels","کانال های تلگرام"},
            {"TelegramUsers","کاربران تلگرام"},
            {"TemporaryDatas","دیتای موقت"},
            {"UserAccounts","اکانت کاربران ها"},
            {"WebApiPosts","پست های سایت های شخص ثالث"},
            {"TelegramPosts","پست های خوانده شده از فید ها"},
            {"Logs","لاگ سیستم ها"},
            {"WebApiPostSettings","تنظیمات پست های شخص ثالث"},
        };
        // GET: Admin/AllTablesAsMenu
        public IActionResult Index()
        {
            var list = _genericTablesService.GetAllTableNames();

            var names = _genericTablesService.SetNames(list, _tableNamesDictionary);
            return View(names);
        }

        public IActionResult TableIndex(string key)
        {
            var first = _tableNamesDictionary.Keys.First(k => k == key);

            var readAllDataWithStruture = _genericTablesService.ReadAllDataWithStruture(first);

            readAllDataWithStruture.TableName = first;
            readAllDataWithStruture.TableFaName = _tableNamesDictionary[first];

            return View(readAllDataWithStruture);
        }

    }
}