using VIVU.Common.Models;
using VIVU.Data.Entities;
using VIVU.Logic.Queries.Interfaces;

namespace VIVU.API.Controllers;

[Route("api/v{version:apiVersion}/blog/")]
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
    [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<Blog>>), 200)]
    public async Task<ActionResult<CommonResponseModel<IEnumerable<Blog>>>> GetAll()
    {
        var response = new CommonResponseModel<IEnumerable<Blog>>();
        var data = await blogQueries.GetAll();
        return Ok(response.SetResult(true, string.Empty).SetData(data));
    }
    
}
