using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VIVU.Common.Models;
using VIVU.Shared.Model.Cateogry;

namespace VIVU.API.Controllers
{
    [Route("api/v{version:apiVersion}/categories")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<CategoryModel>>>> GetAll(
            [FromQuery] string? keywords)
        {
            return Ok();
        }

        [HttpGet]
        [Route("with_query")]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<CategoryModel>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<CategoryModel>>>> Get(
            [FromQuery] CategoryQueryModel query)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CommonResponseModel<CategoryModel>), 200)]
        public async Task<ActionResult<CommonResponseModel<CategoryModel>>> GetOne(string id)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<CategoryModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CommonResponseModel<CategoryModel>>> Create(
            [FromBody] CreateCategoryCommand command)
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<CategoryModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<CategoryModel>>> Update(string id,
            [FromBody] UpdateCategoryCommand command)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<CategoryModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<CategoryModel>>> Delete(string id)
        {
            return Ok();
        }
    }
}
