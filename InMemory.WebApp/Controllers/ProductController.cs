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
            memoryCache.Set<string>("zaman", DateTime.Now.ToString());
            return View();
        }

        public IActionResult Show()
        {
            ViewBag.zaman = memoryCache.Get<string>("zaman");
            return View();
        }
    }
}