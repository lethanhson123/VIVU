using Microsoft.AspNetCore.Mvc;

namespace VIVU.CMS.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
