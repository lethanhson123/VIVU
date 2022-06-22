using Microsoft.AspNetCore.Mvc;

namespace VIVU.CMS.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
