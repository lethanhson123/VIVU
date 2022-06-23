using FintechFengShui.Common.Models;
using FintechFengShui.Logic.Queries.Interface;
using FintechFengShui.Shared.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FintechFengShui.API.Controllers
{
    [Route("api/v{version:apiVersion}/tags")]
    [ApiVersion("1.0")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ITagQueries tagQueries;

        public TagsController(IMediator mediator,
            ITagQueries tagQueries)
        {
            this.mediator = mediator;
            this.tagQueries = tagQueries;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<TagModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CommonResponseModel<TagModel>>> Create(
            [FromBody] CreateTagCommand command)
        {
            var response = new CommonResponseModel<TagModel>();

            var userName = HttpContext.GetClaimByKey("UserName");
            command.UserName = userName;

            var commandResult = await mediator.Send(command);
            return Ok(response.SetResult(commandResult.Success, commandResult.Message)
                .SetData(commandResult.Data));
        }

        [HttpGet]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<TagModel>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<TagModel>>>> Get(
            [FromQuery]string? keywords)
        {
            var response = new CommonResponseModel<IEnumerable<TagModel>>();
            var data = tagQueries.Get(keywords);
            return Ok(response.SetResult(true, string.Empty).SetData(data));
        }

        [HttpGet]
        [Route("with_query")]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<TagModel>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<TagModel>>>> Get(
            [FromQuery] TagQueryModel query)
        {
            var response = new CommonResponseModel<IEnumerable<TagModel>>();
            var data = tagQueries.Get(query);
            return Ok(response.SetResult(true, string.Empty).SetData(data));
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(CommonResponseModel<TagDetailModel>), 200)]
        public async Task<ActionResult<CommonResponseModel<TagDetailModel>>> GetOne(int id)
        {
            var response = new CommonResponseModel<TagDetailModel>();
            var data = await tagQueries.GetDetail(id);
            return Ok(response.SetResult(true, string.Empty).SetData(data));
        }

        [HttpPut]
        [Route("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<TagModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<TagModel>>> Update(int id,
            [FromBody] UpdateTagCommand command)
        {
            if (command.Id != id)
                return BadRequest();

            var response = new CommonResponseModel<TagModel>();

            var userName = HttpContext.GetClaimByKey("UserName");
            command.UserName = userName;

            var commandResult = await mediator.Send(command);
            return Ok(response.SetResult(commandResult.Success, commandResult.Message));
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<TagModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<TagModel>>> Delete(int id)
        {
            var userName = HttpContext.GetClaimByKey("UserName");
            var command = new DeleteTagCommand()
            {
                Id = id,
                UserName = userName
            };
            var response = new CommonResponseModel<TagModel>();
            var commandResult = await mediator.Send(command);
            return Ok(response.SetResult(commandResult.Success, commandResult.Message));
        }
    }
}
