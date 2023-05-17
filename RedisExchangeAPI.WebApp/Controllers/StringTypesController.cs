using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.WebApp.Services;

namespace RedisExchangeAPI.WebApp.Controllers
{
    public class StringTypesController : BaseController
    {
        public StringTypesController(RedisService redisService) : base(redisService) { }

        public IActionResult Index()
        {
            db.StringSet("name", "metin celebi");

            return View();
        }

        public IActionResult Show()
        {
            var value = db.StringGet("name");
            if (value.HasValue) { ViewBag.name = value.ToString(); }
            return View();
        }
    }
}