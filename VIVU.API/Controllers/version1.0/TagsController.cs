using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VIVU.Common.Models;
using VIVU.Shared.Model;

namespace VIVU.API.Controllers
{
    [Route("api/v{version:apiVersion}/tag")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class TagsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ITagQueries tagQueries;

        public TagsController(IMediator mediator, ITagQueries tagQueries)
        {
            this.mediator = mediator;
            this.tagQueries = tagQueries;
        }
        [HttpGet]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<TagModel>>), 200)]
        [AllowAnonymous]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<TagModel>>>> GetAll(
            [FromQuery] string? keywords)
        {
            var response = new CommonResponseModel<IEnumerable<TagModel>>();
            var result = tagQueries.Get();
            return Ok(response.SetResult(true, String.Empty).SetData(result));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("with_query")]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<TagModel>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<TagModel>>>> Get(
            [FromQuery] TagQueryModel query)
        {
            var response = new CommonResponseModel<IEnumerable<TagModel>>();
            var result = tagQueries.Get(query);
            return Ok(response.SetResult(true, String.Empty).SetData(result));
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CommonResponseModel<TagModel>), 200)]
        public async Task<ActionResult<CommonResponseModel<TagModel>>> GetOne(int id)
        {

            var response = new CommonResponseModel<TagModel>();
            var result = await tagQueries.GetDetail(id);
            return Ok(response.SetResult(true, String.Empty).SetData(result));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<TagModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CommonResponseModel<TagModel>>> Create(
            [FromBody] CreateTagCommand command)
        {
            if (command == null)
                return BadRequest();
            var response = new CommonResponseModel<TagModel>();
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
            [FromBody] UpdateTagCommand command)
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
            var result = await mediator.Send(new DeleteTagCommand { Id = id });
            return Ok(result.Success ? response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
                     : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
        }
    }
}
