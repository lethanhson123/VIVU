
namespace VIVU.CMS.Controllers;

public class ProductImageController : Controller
{
    private readonly IProductImageService productImageService;
    private readonly IAuthenticateHelper authenticateHelper;

    public ProductImageController(IProductImageService productImageService,
        IAuthenticateHelper authenticateHelper)
    {
        this.productImageService = productImageService;
        this.authenticateHelper = authenticateHelper;
    }

    public IActionResult Index(string id)
    {
        ProductImageQueryModel query = new ProductImageQueryModel();
        query.ProductId = id;
        query.Keywords = query.Keywords ?? string.Empty;
        query.PageIndex = query.PageIndex > 1 ? query.PageIndex : 1;
        query.Limit = query.Limit > 0 ? query.Limit : 1000;
        return View(query);
    }

    public async Task<ActionResult> Modal(int id, string productId)
    {
        var model = new ProductImageModel();
        if (id > 0)
        {
            var remoteResponseData = await productImageService.GetDetail(id);

            if (remoteResponseData.Data != null)
            {
                model = remoteResponseData.Data;
            }
        }
        else
            model.ProductId = productId;
        return PartialView(model);
    }
    public async Task<ActionResult> List(ProductImageQueryModel query)
    {
        query.Keywords = query.Keywords ?? string.Empty;
        query.PageIndex = query.PageIndex > 1 ? query.PageIndex : 1;
        query.Limit = query.Limit > 0 ? query.Limit : 1000;
        var remoteResponseData = await productImageService.GetWithQuery(query);
        var model = remoteResponseData.Data;
        return PartialView(model);
    }

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<ActionResult> SaveChange(ProductImageModel model)
    {
        authenticateHelper.SetInforFromContext(HttpContext);
        string token = authenticateHelper.GetAccessToken();

        if(model.Id==0)
        {
            var response = await productImageService.Create(model, token);
            return Json(response); ;
        }
        else
        {
            var response = await productImageService.Update(model, token);
            return Json(response);
        }
    }

    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Delete(int id)
    {
        var response = new CommonResponseModel<object>();
        authenticateHelper.SetInforFromContext(HttpContext);
        string token = authenticateHelper.GetAccessToken();
        response = await productImageService.Delete(id, token);
        return Json(response);
    }
}
