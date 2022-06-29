using Microsoft.AspNetCore.Mvc;

namespace VIVU.CMS.Controllers
{
    public class SaleOrderController : Controller
    {
        private readonly ISalesOrderService salesOrderService;
        private readonly IAuthenticateHelper authenticateHelper;

        public SaleOrderController(ISalesOrderService salesOrderService,
           IAuthenticateHelper authenticateHelper)
        {
            this.salesOrderService = salesOrderService;
            this.authenticateHelper = authenticateHelper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Detail(string Id)
        {
            var model = new SalesOrderModelResponse();
            if (!string.IsNullOrEmpty(Id))
            {
                var remoteResponseData = await salesOrderService.GetDetail(Id);

                if (remoteResponseData.Data != null)
                {
                    model = remoteResponseData.Data;
                }
            }
            return View(model);
        }

        public async Task<ActionResult> List(SalesOrderQueryModel query)
        {
            var remoteResponseData = await salesOrderService.Get(query.Keywords ?? string.Empty);
            var model = remoteResponseData.Data;
            return PartialView(model);
        }
        public async Task<ActionResult> Create(SalesOrderModel model)
        {
            authenticateHelper.SetInforFromContext(HttpContext);
            string token = authenticateHelper.GetAccessToken();

            if (string.IsNullOrEmpty(model.Id))
            {
                var response = await salesOrderService.Create(model, token);
                return Json(response); ;
            }
            return Json(new CommonResponseModel<bool> { Success = false }); ;
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Update(SalesOrderModel model)
        {
            authenticateHelper.SetInforFromContext(HttpContext);
            string token = authenticateHelper.GetAccessToken();
            if (!string.IsNullOrEmpty(model.Id))
            {
                var response = await salesOrderService.Update(model, token);
                return Json(response);
            }
            return Json(new CommonResponseModel<bool> { Success = false }); ;
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(string id)
        {
            var response = new CommonResponseModel<object>();
            authenticateHelper.SetInforFromContext(HttpContext);
            string token = authenticateHelper.GetAccessToken();
            response = await salesOrderService.Delete(id, token);
            return Json(response);
        }
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult> UpdateStatus(string id, int status)
        {
            var response = new CommonResponseModel<object>();
            authenticateHelper.SetInforFromContext(HttpContext);
            string token = authenticateHelper.GetAccessToken();
            response = await salesOrderService.Status(id, status, token);
            return Json(response);
        }
    }
}
