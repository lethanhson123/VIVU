using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using VIVU.Common.Models;
using VIVU.Data.Entities;
using VIVU.Logic.Queries.Interfaces;
using VIVU.Shared.Model;

namespace VIVU.API.Controllers;

[Route("api/v{version:apiVersion}/blog")]
[ApiVersion("1.0")]
[ApiController]
public class BlogsController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IBlogQueries blogQueries;
    public BlogsController(IMediator mediator,
             IBlogQueries blogQueries)
    {
        this.mediator = mediator;
        this.blogQueries = blogQueries;
    }

    [HttpGet]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<CategoryModel>>), 200)]
    [AllowAnonymous]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<BlogModel>>>> GetAll(
            [FromQuery] string? keywords)
    {
        var response = new CommonResponseModel<IEnumerable<BlogModel>>();
        var result = blogQueries.Get();
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpGet]
    [Route("with_query")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<BlogModel>>), 200)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<BlogModel>>>> Get(
        [FromQuery] BlogQueryModel query)
    {
        var response = new CommonResponseModel<IEnumerable<BlogModel>>();
        var result = blogQueries.Get(query);
        return Ok(response.SetResult(true, String.Empty).SetData(result));

    }

    [HttpGet]
    [Route("{id}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommonResponseModel<BlogModel>), 200)]
    public async Task<ActionResult<CommonResponseModel<BlogModel>>> GetOne(int id)
    {
        var response = new CommonResponseModel<BlogModel>();
        var result = await blogQueries.GetDetail(id);
        return Ok(response.SetResult(true, String.Empty).SetData(result));
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CommonResponseModel<BlogModel>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<CommonResponseModel<BlogModel>>> Create(
        [FromBody] CreateBlogCommand command)
    {
        if (command == null)
            return BadRequest();
        var response = new CommonResponseModel<BlogModel>();
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
        [FromBody] UpdateBlogCommand command)
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
    [ProducesResponseType(typeof(CommonResponseModel<BlogModel>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<BlogModel>>> Delete(int id)
    {
        if (string.IsNullOrEmpty(id.ToString()))
            return BadRequest();
        var response = new CommonResponseModel<object>();
        var result = await mediator.Send(new DeleteBlogCommand { Id = id });
        return Ok(result.Success ? response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
                 : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
    }

}
