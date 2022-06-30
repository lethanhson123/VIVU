using Microsoft.AspNetCore.Mvc;

namespace VIVU.CMS.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoriesService categoryService;
        private readonly IAuthenticateHelper authenticateHelper;

        public CategoryController(ICategoriesService categoryService,
            IAuthenticateHelper authenticateHelper)
        {
            this.categoryService = categoryService;
            this.authenticateHelper = authenticateHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Modal(int id)
        {
            var model = new CategoryModel();

            if (id > 0)
            {
                var remoteResponseData = await categoryService.Get(id);

                if (remoteResponseData.Data != null)
                {
                    model = remoteResponseData.Data;
                }
            }

            return PartialView(model);
        }

        public async Task<ActionResult> List(CategoryQueryModel query)
        {
            var remoteResponseData = await categoryService.GetAll(query.Keywords ?? string.Empty);
            var model = remoteResponseData.Data;
            return PartialView(model);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SaveChange(CategoryModel model)
        {
            var response = new CommonResponseModel<CategoryModel>();
            authenticateHelper.SetInforFromContext(HttpContext);
            string token = authenticateHelper.GetAccessToken();

            if (model.Id == 0)
            {
                response = await categoryService.Create(model, token);
            }
            else
            {
                response = await categoryService.Update(model, token);
            }

            return Json(response);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var response = new CommonResponseModel<object>();
            authenticateHelper.SetInforFromContext(HttpContext);
            string token = authenticateHelper.GetAccessToken();

            response = await categoryService.Delete(id, token);
            return Json(response);
        }
    }
}
