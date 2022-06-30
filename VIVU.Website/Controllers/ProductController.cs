using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VIVU.Website.Models;

namespace VIVU.Website.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }

    }
}