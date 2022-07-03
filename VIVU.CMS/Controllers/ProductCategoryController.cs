using Microsoft.AspNetCore.Mvc;

namespace VIVU.CMS.Controllers
{
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService productCategoryService;
        private readonly IAuthenticateHelper authenticateHelper;

        public ProductCategoryController(IProductCategoryService productCategoryService ,
           IAuthenticateHelper authenticateHelper)
        {
            this.productCategoryService = productCategoryService;
            this.authenticateHelper = authenticateHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Detail(string Id)
        {
            var model = new ProductCategoryModel();
            if (!string.IsNullOrEmpty(Id))
            {
                var remoteResponseData = await productCategoryService.GetDetail(Id);

                if (remoteResponseData.Data != null)
                {
                    model = remoteResponseData.Data;
                }
            }
            return View(model);
        }
        public async Task<ActionResult> Modal(string id)
        {
            var model = new ProductCategoryModel();

            if (!string.IsNullOrEmpty(id))
            {
                var remoteResponseData = await productCategoryService.GetDetail(id);

                if (remoteResponseData.Data != null)
                {
                    model = remoteResponseData.Data;
                }
            }

            return PartialView(model);
        }
        public async Task<ActionResult> List(ProductCategoryQueryModel query)
        {
            query.Keywords = query.Keywords ?? string.Empty;
            query.PageIndex = query.PageIndex > 1 ? query.PageIndex : 1;
            query.Limit = query.Limit > 0 ? query.Limit : 1000;
            var remoteResponseData = await productCategoryService.GetWithQuery(query);
            var model = remoteResponseData.Data;
            return PartialView(model);
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SaveChange(ProductCategoryModel model)
        {
            authenticateHelper.SetInforFromContext(HttpContext);
            string token = authenticateHelper.GetAccessToken();

            if (string.IsNullOrEmpty(model.Id))
            {
                var response = await productCategoryService.Create(model, token);
                return Json(response); ;
            }
            else
            {
                var response = await productCategoryService.Update(model, token);
                return Json(response);
            }
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(string id)
        {
            var response = new CommonResponseModel<object>();
            authenticateHelper.SetInforFromContext(HttpContext);
            string token = authenticateHelper.GetAccessToken();
            response = await productCategoryService.Delete(id, token);
            return Json(response);
        }
    }
}
