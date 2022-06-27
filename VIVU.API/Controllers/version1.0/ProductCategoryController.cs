namespace VIVU.API.Controllers;

[Route("api/v{version:apiVersion}/product_category")]
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class ProductCategoryController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IProductCategoryQueries productCategoryQueries;

    public ProductCategoryController(IMediator mediator, IProductCategoryQueries productCategoryQueries)
    {
        this.mediator = mediator;
        this.productCategoryQueries = productCategoryQueries;
    }
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<ProductCategoryModel>>), 200)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<ProductCategoryModel>>>> GetAll()
    {
        var response = new CommonResponseModel<IEnumerable<ProductCategoryModel>>();
        var result = productCategoryQueries.Get();
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpGet]
    [Route("with_query")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<ProductCategoryModel>>), 200)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<ProductCategoryModel>>>> Get(
        [FromQuery] ProductCategoryQueryModel query)
    {
        var response = new CommonResponseModel<IEnumerable<ProductCategoryModel>>();
        var result = productCategoryQueries.Get(query);
        return  Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpGet]
    [Route("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommonResponseModel<ProductCategoryModel>), 200)]
    public async Task<ActionResult<CommonResponseModel<ProductCategoryModel>>> GetOne(string id)
    {
        var response = new CommonResponseModel<ProductCategoryModel>();
        var result = await productCategoryQueries.GetDetail(id);
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CommonResponseModel<ProductCategoryModel>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<CommonResponseModel<ProductCategoryModel>>> Create(
        [FromBody] CreateProductCategoryCommand command)
    {
        if (command == null)
            return BadRequest();
        var response = new CommonResponseModel<ProductCategoryModel>();
        var result = await mediator.Send(command);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(CommonResponseModel<object>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<object>>> Update(string id,
        [FromBody] UpdateProductCategoryCommand command)
    {
        if (command == null)
            return BadRequest();
        var response = new CommonResponseModel<object>();
        var result = await mediator.Send(command);
        return Ok(result.Success ? response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(CommonResponseModel<object>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<object>>> Delete(string id)
    {
        if (string.IsNullOrEmpty(id.ToString()))
            return BadRequest();
        var response = new CommonResponseModel<object>();
        var result = await mediator.Send(new DeleteProductCategoryCommand { Id = id});
        return Ok(result.Success ? response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }
}
