using Microsoft.AspNetCore.Mvc;

namespace VIVU.Website.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
