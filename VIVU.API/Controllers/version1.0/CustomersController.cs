namespace VIVU.API.Controllers;

[Route("api/v{version:apiVersion}/customers")]
[ApiVersion("1.0")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly ICustomerQueries customerQueries;

    public CustomersController(IMediator mediator, ICustomerQueries customerQueries)
    {
        this.mediator = mediator;
        this.customerQueries = customerQueries;
    }
    [HttpGet]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<CustomerModel>>>> GetAll(
        [FromQuery] string? keywords)
    {
        var response = new CommonResponseModel<IEnumerable<CustomerModel>>();
        var result = customerQueries.Get();
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpGet]
    [Route("with_query")]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<CustomerModel>>), 200)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<CustomerModel>>>> Get(
        [FromQuery] CustomerQueryModel query)
    {
        var response = new CommonResponseModel<IEnumerable<CustomerModel>>();
        var result = customerQueries.Get(query);
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(CommonResponseModel<CustomerModel>), 200)]
    public async Task<ActionResult<CommonResponseModel<CustomerModel>>> GetOne(string id)
    {
        var response = new CommonResponseModel<IEnumerable<CustomerModel>>();
        var result = customerQueries.Get(id);
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CommonResponseModel<CustomerModel>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<CommonResponseModel<CustomerModel>>> Create(
        [FromBody] CreateCustomerCommand command)
    {

        if (command == null)
            return BadRequest();
        var response = new CommonResponseModel<CustomerModel>();
        var result = await mediator.Send(command);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CommonResponseModel<object>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<object>>> Update(string id,
        [FromBody] UpdateCustomerCommand command)
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
        var result = await mediator.Send(new DeleteCustomerCommand { Id = id });
        return Ok(result.Success ? response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }
}
