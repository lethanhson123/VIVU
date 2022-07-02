using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VIVU.Common.Models;
using VIVU.Shared.Model;

namespace VIVU.API.Controllers
{
    [Route("api/v{version:apiVersion}/categories")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ICategoryQueries categoryQueries;

        public CategoriesController(IMediator mediator, ICategoryQueries categoryQueries)
        {
            this.mediator = mediator;
            this.categoryQueries = categoryQueries;
        }
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<CategoryModel>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<CategoryModel>>>> GetAll(
            [FromQuery] string? keywords)
        {
            var response = new CommonResponseModel<IEnumerable<CategoryModel>>();
            var result = categoryQueries.Get();
            return Ok(response.SetResult(true, String.Empty).SetData(result));
        }

        [HttpGet]
        [Route("with_query")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<CategoryModel>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<CategoryModel>>>> Get(
            [FromQuery] CategoryQueryModel query)
        {
            var response = new CommonResponseModel<IEnumerable<CategoryModel>>();
            var result = categoryQueries.Get(query);
            return Ok(response.SetResult(true, String.Empty).SetData(result));
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CommonResponseModel<CategoryModel>), 200)]
        public async Task<ActionResult<CommonResponseModel<CategoryModel>>> GetOne(int id)
        {
            var response = new CommonResponseModel<CategoryModel>();
            var result = await categoryQueries.GetDetail(id);
            return Ok(response.SetResult(true, String.Empty).SetData(result));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<CategoryModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CommonResponseModel<CategoryModel>>> Create(
            [FromBody] CreateCategoryCommand command)
        {
            if (command == null)
                return BadRequest();
            var response = new CommonResponseModel<CategoryModel>();
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
            [FromBody] UpdateCategoryCommand command)
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
        [ProducesResponseType(typeof(CommonResponseModel<CategoryModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<CategoryModel>>> Delete(int id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
                return BadRequest();
            var response = new CommonResponseModel<object>();
            var result = await mediator.Send(new DeleteCategoryCommand { Id = id });
            return Ok(result.Success ? response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
                     : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
        }
    }
}
