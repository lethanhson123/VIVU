using Microsoft.AspNetCore.Mvc;

namespace VIVU.CMS.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
