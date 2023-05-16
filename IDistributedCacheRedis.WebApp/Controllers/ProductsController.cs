using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace IDistributedCacheRedis.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private IDistributedCache distributedCache;

        public ProductsController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

            distributedCache.SetString("name", "metin", cacheOptions);
            return View();
        }

        public IActionResult Show()
        {
            string name = distributedCache.GetString("name");
            ViewBag.name = name;
            return View();
        }

        public IActionResult Remove()
        {
            distributedCache.Remove("name");
            return View();
        }
    }
}