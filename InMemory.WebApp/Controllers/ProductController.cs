using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemory.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMemoryCache memoryCache;

        public ProductController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(1);
            options.SlidingExpiration = TimeSpan.FromSeconds(10);
            options.Priority = CacheItemPriority.Normal;

            memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);


            return View();
        }

        public IActionResult Show()
        {
            memoryCache.TryGetValue("zaman", out string zamancache);
            ViewBag.zaman = zamancache;
            return View();
        }
    }
}