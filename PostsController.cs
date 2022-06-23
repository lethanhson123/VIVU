using FintechFengShui.Common.Models;
using FintechFengShui.Logic.Queries.Interface;
using FintechFengShui.Shared.Model;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FintechFengShui.API.Controllers
{
    [Route("api/v{version:apiVersion}/posts")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IPostQueries postQueries;

        public PostsController(IMediator mediator,
            IPostQueries postQueries)
        {
            this.mediator = mediator;
            this.postQueries = postQueries;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<PostModel>>), 200)]
        public IActionResult Get(string? keywords)
        {
            var response = new CommonResponseModel<IEnumerable<PostModel>>();
            var data = postQueries.Get(keywords);
            return Ok(response.SetResult(true, string.Empty).SetData(data));
        }

        [HttpGet]
        [Route("with_query")]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<PostModel>>), 200)]
        public IActionResult Get([FromQuery]PostQueryModel query)
        {
            var response = new CommonResponseModel<IEnumerable<PostModel>>();
            var data = postQueries.Get(query);
            return Ok(response.SetResult(true, string.Empty).SetData(data));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CommonResponseModel<PostDetailModel>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetOne(int id)
        {
            var response = new CommonResponseModel<PostDetailModel>();
            var data = postQueries.GetDetail(id);

            if(data == null)
                return NotFound();

            return Ok(response.SetResult(true, string.Empty).SetData(data));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<PostModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CommonResponseModel<PostModel>>> Create(
            [FromBody] CreatePostCommand command)
        {
            var response = new CommonResponseModel<PostModel>();

            var userName = HttpContext.GetClaimByKey("UserName");
            command.UserName = userName;

            var commandResult = await mediator.Send(command);
            return Ok(response.SetResult(commandResult.Success, commandResult.Message)
                .SetData(commandResult.Data));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<object>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<object>>> Update(int id,
            [FromBody] UpdatePostCommand command)
        {
            if (command.Id != id)
                return BadRequest();

            var response = new CommonResponseModel<object>();

            var userName = HttpContext.GetClaimByKey("UserName");
            command.UserName = userName;

            var commandResult = await mediator.Send(command);
            return Ok(response.SetResult(commandResult.Success, commandResult.Message));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<object>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<object>>> Delete(int id)
        {
            var userName = HttpContext.GetClaimByKey("UserName");
            var command = new DeletePostCommand()
            {
                Id = id,
                UserName = userName
            };
            var response = new CommonResponseModel<object>();
            var commandResult = await mediator.Send(command);
            return Ok(response.SetResult(commandResult.Success, commandResult.Message));
        }
    }
}
