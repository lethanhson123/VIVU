namespace VIVU.API.Controllers;

[Route("api/v{version:apiVersion}/authenticate")]
[ApiVersion("1.0")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IProductQueries productQueries;

    public AuthenticateController(IMediator mediator, IProductQueries productQueries)
    {
        this.mediator = mediator;
        this.productQueries = productQueries;
    }

    

    [HttpPost]
    [ProducesResponseType(typeof(CommonResponseModel<object>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [Route("sign_up")]
    public async Task<ActionResult<CommonResponseModel<object>>> SignUp(
        [FromBody] SignUpCommand command)
    {
        if (command == null)
            return BadRequest();
        var response = new CommonResponseModel<object>();
        var result = await mediator.Send(command);
        return Ok(result.Success ? response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }
    [HttpPost]
    [ProducesResponseType(typeof(CommonResponseModel<AuthenticateModel>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [Route("sign_in")]
    public async Task<ActionResult<CommonResponseModel<AuthenticateModel>>> SignIn(
        [FromBody] LoginCommand command)
    {
        if (command == null)
            return BadRequest();
        var response = new CommonResponseModel<AuthenticateModel>();
        var result = await mediator.Send(command);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }
    [HttpPost]
    [ProducesResponseType(typeof(CommonResponseModel<AuthenticateModel>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [Route("refresh_token")]
    public async Task<ActionResult<CommonResponseModel<AuthenticateModel>>> RefreshToken(
        [FromBody] RefreshTokenCommand command)
    {
        if (command == null)
            return BadRequest();
        var response = new CommonResponseModel<AuthenticateModel>();
        var result = await mediator.Send(command);
        return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }
}
