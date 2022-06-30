using Microsoft.AspNetCore.Mvc;

namespace VIVU.CMS.Controllers
{
    [Route("/file-manager")]
    public class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/modal")]
        public IActionResult Modal()
        {
            return PartialView();
        }
    }
}
