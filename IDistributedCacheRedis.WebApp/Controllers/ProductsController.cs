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
            return View();
        }
    }
}