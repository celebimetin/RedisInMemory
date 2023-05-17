using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.WebApp.Services;

namespace RedisExchangeAPI.WebApp.Controllers
{
    public class ListTypesController : BaseController
    {
        private string listKey = "names";

        public ListTypesController(RedisService redisService) : base(redisService) { }

        public IActionResult Index()
        {
            List<string> list = new List<string>();
            if (db.KeyExists(listKey))
            {
                db.ListRange(listKey).ToList().ForEach(x =>
                {
                    list.Add(x.ToString());
                });
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            db.ListRightPush(listKey, name);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            db.ListRemoveAsync(listKey, name).Wait();
            return RedirectToAction("Index");
        }
    }
}