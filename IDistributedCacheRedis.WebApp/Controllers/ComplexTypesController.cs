using IDistributedCacheRedis.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;

namespace IDistributedCacheRedis.WebApp.Controllers
{
    public class ComplexTypesController : Controller
    {
        private IDistributedCache distributedCache;
        public ComplexTypesController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }
        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions();
            cacheOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(2);

            Product product = new Product { Id = 1, Name = "Kalem1", Price = 100 };

            string jsonProduct = JsonConvert.SerializeObject(product);

            Byte[] byteProduct = Encoding.UTF8.GetBytes(jsonProduct);

            distributedCache.Set("product:1", byteProduct);

            //await distributedCache.SetStringAsync("product:1", jsonProduct, cacheOptions);

            return View();
        }

        public IActionResult Show()
        {
            Byte[] byteProduct = distributedCache.Get("product:1");

            string jsonProduct = Encoding.UTF8.GetString(byteProduct);

            //string jsonProduct = distributedCache.GetString("product:1");

            Product product = JsonConvert.DeserializeObject<Product>(jsonProduct);

            ViewBag.product = product;
            return View();
        }

        public IActionResult Remove()
        {
            distributedCache.Remove("product:1");
            return View();
        }

        public IActionResult ImageCache()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/newyear.jpg");

            byte[] imageByte = System.IO.File.ReadAllBytes(path);

            distributedCache.Set("resim", imageByte);

            return View();
        }

        public IActionResult ImageUrl()
        {
            byte[] imageByte = distributedCache.Get("resim");

            return File(imageByte, "image/jpg");
        }
    }
}