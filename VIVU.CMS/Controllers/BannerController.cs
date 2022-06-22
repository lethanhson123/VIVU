using Microsoft.AspNetCore.Mvc;

namespace VIVU.CMS.Controllers
{
    public class BannerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
