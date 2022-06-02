using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using VIVU.Common.Models;
using VIVU.Data.Entities;
using VIVU.Logic.Queries.Interfaces;
using VIVU.Shared.Model;

namespace VIVU.API.Controllers;

[Route("api/v{version:apiVersion}/blogs")]
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
    public async Task<ActionResult<CommonResponseModel<IEnumerable<BlogModel>>>> GetAll(
            [FromQuery] string? keywords)
    {
        return Ok();
    }

    [HttpGet]
    [Route("with_query")]
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<BlogModel>>), 200)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<BlogModel>>>> Get(
        [FromQuery] BlogQueryModel query)
    {
        return Ok();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(CommonResponseModel<BlogModel>), 200)]
    public async Task<ActionResult<CommonResponseModel<BlogModel>>> GetOne(string id)
    {
        return Ok();
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CommonResponseModel<BlogModel>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<CommonResponseModel<BlogModel>>> Create(
        [FromBody] CreateBlogCommand command)
    {
        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CommonResponseModel<BlogModel>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<BlogModel>>> Update(string id,
        [FromBody] UpdateBlogCommand command)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CommonResponseModel<BlogModel>), 200)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<CommonResponseModel<BlogModel>>> Delete(string id)
    {
        return Ok();
    }

}
