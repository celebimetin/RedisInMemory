using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;

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
            //Memory de olup olmadığını kontrol etmek için 1.ci yol
            if (String.IsNullOrEmpty(memoryCache.Get<string>("zaman")))
            {
                memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            }

            //Memory de olup olmadığını kontrol etmek için 2.ci yol
            if(!memoryCache.TryGetValue("zaman", out string zamancache))
            {
                memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            }

            return View();
        }

        public IActionResult Show()
        {
            memoryCache.Remove("zaman");

            memoryCache.GetOrCreate<string>("zaman", entry =>
            {
                return DateTime.Now.ToString();
            });

            ViewBag.zaman = memoryCache.Get<string>("zaman");
            return View();
        }
    }
}