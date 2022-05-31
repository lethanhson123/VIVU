using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VIVU.Common.Models;
using VIVU.Shared.Model.Customer;

namespace VIVU.API.Controllers
{
    [Route("api/v{version:apiVersion}/customers")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<CustomerModel>>>> GetAll(
            [FromQuery] string? keywords)
        {
            return Ok();
        }

        [HttpGet]
        [Route("with_query")]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<CustomerModel>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<CustomerModel>>>> Get(
            [FromQuery] CustomerQueryModel query)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CommonResponseModel<CustomerModel>), 200)]
        public async Task<ActionResult<CommonResponseModel<CustomerModel>>> GetOne(string id)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<CustomerModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CommonResponseModel<CustomerModel>>> Create(
            [FromBody] CreateCustomerCommand command)
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<CustomerModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<CustomerModel>>> Update(string id,
            [FromBody] UpdateCustomerCommand command)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<CustomerModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<CustomerModel>>> Delete(string id)
        {
            return Ok();
        }
    }
}
