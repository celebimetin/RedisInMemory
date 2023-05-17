using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.WebApp.Services;

namespace RedisExchangeAPI.WebApp.Controllers
{
    public class SortedTypesController : BaseController
    {
        private string listKey = "sortednames";

        public SortedTypesController(RedisService redisService) : base(redisService) { }

        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();
            if (db.KeyExists(listKey))
            {
                db.SortedSetScan(listKey).ToList().ForEach(x => list.Add(x.ToString()));
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name, int score)
        {
            if (!db.KeyExists(listKey))
            {
                db.KeyExpire(listKey, DateTime.Now.AddMinutes(2));
            }

            db.SortedSetAdd(listKey, name, score);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            db.SortedSetRemove(listKey, name);
            return RedirectToAction("Index");
        }
    }
}