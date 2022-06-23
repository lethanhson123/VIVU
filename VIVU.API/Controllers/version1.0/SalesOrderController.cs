using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VIVU.Common.Models;
using VIVU.Logic.Commands;
using VIVU.Shared.Model;

namespace VIVU.API.Controllers
{
    [Route("api/v{version:apiVersion}/sales_order")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IProductQueries productQueries;

        public SalesOrderController(IMediator mediator, IProductQueries productQueries)
        {
            this.productQueries = productQueries;
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<SalesOrderModel>>>> GetAll(
            [FromQuery] string? keywords)
        {
            return Ok();
        }

        [HttpGet]
        [Route("with_query")]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<SalesOrderModel>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<SalesOrderModel>>>> Get(
            [FromQuery] SalesOrderQueryModel query)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CommonResponseModel<SalesOrderModel>), 200)]
        public async Task<ActionResult<CommonResponseModel<SalesOrderModel>>> GetOne(string id)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<SalesOrderModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [AllowAnonymous]
        public async Task<ActionResult<CommonResponseModel<SalesOrderModel>>> Create(
            [FromBody] CreateSalesOrderCommand command)
        {
            if (command == null)
                return BadRequest();
            var response = new CommonResponseModel<SalesOrderModel>();
            var result = await mediator.Send(command);
            return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                     : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<SalesOrderModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<SalesOrderModel>>> Update(string id,
            [FromBody] UpdateSalesOrderCommand command)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(CommonResponseModel<SalesOrderModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<SalesOrderModel>>> Delete(string id)
        {
            if (string.IsNullOrEmpty(id.ToString()))
                return BadRequest();
            var response = new CommonResponseModel<object>();
            var result = await mediator.Send(new DeleteSalesOrderCommand { Id = id });
            return Ok(result.Success ? response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
                     : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
        }
    }
}
