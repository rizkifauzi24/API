﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class TestCorsController : Controller
    {
        [HttpGet("testcors")]
        public IActionResult Index()
        {
            return View();
        }
    }
}