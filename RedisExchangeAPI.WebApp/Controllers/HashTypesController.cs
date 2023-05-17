﻿using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.WebApp.Services;

namespace RedisExchangeAPI.WebApp.Controllers
{
    public class HashTypesController : BaseController
    {
        private string hashKey { get; set; } = "hashkey";

        public HashTypesController(RedisService redisService) : base(redisService) { }

        public IActionResult Index()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();
            if(db.KeyExists(hashKey))
            {
                db.HashGetAll(hashKey).ToList().ForEach(x =>
                {
                    list.Add(x.Name, x.Value);
                });
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string key, string val)
        {
            db.HashSet(hashKey, key, val);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteItem(string name)
        {
            db.HashDelete(hashKey, name);
            return RedirectToAction("Index");
        }
    }
}