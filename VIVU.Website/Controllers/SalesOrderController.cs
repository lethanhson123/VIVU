﻿using Microsoft.AspNetCore.Mvc;

namespace VIVU.Website.Controllers
{
    public class SalesOrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
