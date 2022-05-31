using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VIVU.Common.Models;
using VIVU.Shared.Model.Product;

namespace VIVU.API.Controllers
{
    [Route("api/v{version:apiVersion}/product")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<ProductModel>>>> GetAll(
            [FromQuery] string? keywords)
        {
            return Ok();
        }

        [HttpGet]
        [Route("with_query")]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<ProductModel>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<ProductModel>>>> Get(
            [FromQuery] ProductQueryModel query)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CommonResponseModel<ProductModel>), 200)]
        public async Task<ActionResult<CommonResponseModel<ProductModel>>> GetOne(string id)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<ProductModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CommonResponseModel<ProductModel>>> Create(
            [FromBody] CreateProductCommand command)
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<ProductModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<ProductModel>>> Update(string id,
            [FromBody] UpdateProductCommand command)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<ProductModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<ProductModel>>> Delete(string id)
        {
            return Ok();
        }
    }
}
