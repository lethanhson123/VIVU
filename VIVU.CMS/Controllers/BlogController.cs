using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VIVU.CMS.Models;

namespace VIVU.CMS.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;

        public BlogController(ILogger<BlogController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int Id)
        {
            return View();
        }
    }
}