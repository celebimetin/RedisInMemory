using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.WebApp.Services;

namespace RedisExchangeAPI.WebApp.Controllers
{
    public class SetTypesController : BaseController
    {
        private string listKey = "setnames";

        public SetTypesController(RedisService redisService) : base(redisService) { }

        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();
            if (db.KeyExists(listKey))
            {
                db.SetMembers(listKey).ToList().ForEach(x => list.Add(x.ToString()));
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            if (!db.KeyExists(listKey))
            {
                db.KeyExpire(listKey, DateTime.Now.AddMinutes(2));
            }

            db.SetAdd(listKey, name);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            db.SetRemoveAsync(listKey, name).Wait();
            return RedirectToAction("Index");
        }
    }
}