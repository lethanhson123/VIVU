
namespace VIVU.CMS.Controllers;

public class ProductController : Controller
{
    private readonly IProductService productService;
    private readonly IAuthenticateHelper authenticateHelper;

    public ProductController(IProductService productService,
        IAuthenticateHelper authenticateHelper)
    {
        this.productService = productService;
        this.authenticateHelper = authenticateHelper;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<ActionResult> Detail(string Id)
    {
        var model = new ProductModel();
        if (!string.IsNullOrEmpty(Id))
        {
            var remoteResponseData = await productService.GetDetail(Id);

            if (remoteResponseData.Data != null)
            {
                model = remoteResponseData.Data;
            }
        }
        return View(model);
    }

    public async Task<ActionResult> List(ProductQueryModel query)
    {
        query.Keywords = query.Keywords ?? string.Empty;
        query.PageIndex = query.PageIndex > 1 ? query.PageIndex : 1;
        query.Limit = query.Limit > 0 ? query.Limit : 1000;
        var remoteResponseData = await productService.GetWithQuery(query);
        var model = remoteResponseData.Data;
        return PartialView(model);
    }

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<ActionResult> SaveChange(ProductModel model)
    {
        authenticateHelper.SetInforFromContext(HttpContext);
        string token = authenticateHelper.GetAccessToken();

        if (string.IsNullOrEmpty(model.Id))
        {
            var response = await productService.Create(model, token);
            return Json(response); ;
        }
        else
        {
            var response = await productService.Update(model, token);
            return Json(response);
        }
    }

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Delete(string id)
    {
        var response = new CommonResponseModel<object>();
        authenticateHelper.SetInforFromContext(HttpContext);
        string token = authenticateHelper.GetAccessToken();
        response = await productService.Delete(id, token);
        return Json(response);
    }
}
