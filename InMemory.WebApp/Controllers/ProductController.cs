using InMemory.WebApp.Models;
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

            options.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                memoryCache.Set("callback", $"{key}->{value} => sebep: {reason}");
            });

            memoryCache.Set<string>("zaman", DateTime.Now.ToString(), options);

            Product product = new Product { Id = 1, Name = "Kalem", Price = 100 };
            memoryCache.Set<Product>("product:1", product);
            return View();
        }

        public IActionResult Show()
        {
            memoryCache.TryGetValue("zaman", out string zamancache);
            memoryCache.TryGetValue("callback", out string callback);
            ViewBag.zaman = zamancache;
            ViewBag.zaman = callback;
            ViewBag.product = memoryCache.Get<Product>("product:1");
            return View();
        }
    }
}