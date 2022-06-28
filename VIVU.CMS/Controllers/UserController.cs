using Microsoft.AspNetCore.Mvc;

namespace VIVU.CMS.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
