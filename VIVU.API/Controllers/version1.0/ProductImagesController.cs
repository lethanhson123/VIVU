namespace VIVU.API.Controllers;

[Route("api/v{version:apiVersion}/product_image")]
[ApiVersion("1.0")]
[ApiController]
public class ProductImagesController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IProductImageQueries productImageQueries;

    public ProductImagesController(IMediator mediator, IProductImageQueries productImageQueries)
    {
        this.mediator = mediator;
        this.productImageQueries = productImageQueries;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<ProductImageModel>>>> GetAll(
        [FromQuery] string? keywords)
    {
        var response = new CommonResponseModel<IEnumerable<ProductImageModel>>();
        var result = productImageQueries.Get();
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpGet]
    [Route("with_query")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<ProductImageModel>>), 200)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<ProductImageModel>>>> Get(
        [FromQuery] ProductImageQueryModel query)
    {
        var response = new CommonResponseModel<IEnumerable<ProductImageModel>>();
        var result = productImageQueries.Get(query);
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(CommonResponseModel<ProductImageModel>), 200)]
    [AllowAnonymous]

    public async Task<ActionResult<CommonResponseModel<ProductImageModel>>> GetOne(int id)
    {
        var response = new CommonResponseModel<ProductImageModel>();
        var result =await productImageQueries.GetDetail(id);
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CommonResponseModel<ProductImageModel>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<CommonResponseModel<ProductImageModel>>> Create(
        [FromBody] CreateProductImageCommand command)
    {
        if (command == null)
            return BadRequest();
        var response = new CommonResponseModel<ProductImageModel>();
        var result = await mediator.Send(command);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CommonResponseModel<object>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<object>>> Update(int id,
        [FromBody] UpdateProductImageCommand command)
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CommonResponseModel<object>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<object>>> Delete(int id)
    {
        if (string.IsNullOrEmpty(id.ToString()))
            return BadRequest();
        var response = new CommonResponseModel<object>();
        var result = await mediator.Send(new DeleteProductImageCommand { Id = id});
        return Ok(result.Success ? response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }
}
