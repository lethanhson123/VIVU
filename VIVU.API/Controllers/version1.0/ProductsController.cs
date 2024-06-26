﻿namespace VIVU.API.Controllers;

[Route("api/v{version:apiVersion}/product")]
[ApiVersion("1.0")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

public class ProductsController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IProductQueries productQueries;

    public ProductsController(IMediator mediator, IProductQueries productQueries)
    {
        this.mediator = mediator;
        this.productQueries = productQueries;
    }
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<SalesOrderModelResponse>>), 200)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<ProductModel>>>> GetAll()
    {
        var response = new CommonResponseModel<IEnumerable<ProductModel>>();
        var result = productQueries.Get();
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpGet]
    [Route("with_query")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<ProductModel>>), 200)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<ProductModel>>>> Get(
        [FromQuery] ProductQueryModel query)
    {
        var response = new CommonResponseModel<IEnumerable<ProductModel>>();
        var result =productQueries.Get(query);
        return  Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpGet]
    [Route("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommonResponseModel<ProductModel>), 200)]
    public async Task<ActionResult<CommonResponseModel<ProductModel>>> GetOne(string id)
    {
        var response = new CommonResponseModel<ProductModel>();
        var result = await productQueries.GetDetail(id);
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpPost]
    [ProducesResponseType(typeof(CommonResponseModel<ProductModel>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<CommonResponseModel<ProductModel>>> Create(
        [FromBody] CreateProductCommand command)
    {
        if (command == null)
            return BadRequest();
        var response = new CommonResponseModel<ProductModel>();
        var result = await mediator.Send(command);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(CommonResponseModel<object>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<object>>> Update(string id,
        [FromBody] UpdateProductCommand command)
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
        var result = await mediator.Send(new DeleteProductCommand { Id = id});
        return Ok(result.Success ? response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }
}
