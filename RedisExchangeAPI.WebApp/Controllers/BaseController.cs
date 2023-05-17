using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.WebApp.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.WebApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly RedisService redisService;
        protected readonly IDatabase db;

        public BaseController(RedisService redisService)
        {
            this.redisService = redisService;
            redisService.Connect();
            db = redisService.GetDatabase(0);
        }
    }
}