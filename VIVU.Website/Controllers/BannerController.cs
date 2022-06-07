using Microsoft.AspNetCore.Mvc;

namespace VIVU.Website.Controllers
{
    public class BannerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
