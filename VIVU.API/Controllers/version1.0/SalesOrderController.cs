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
        private readonly ISaleOrderQueries saleOrderQueries;

        public SalesOrderController(IMediator mediator, ISaleOrderQueries saleOrderQueries)
        {
            this.saleOrderQueries = saleOrderQueries;
            this.mediator = mediator;
        }
        [HttpGet]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<SalesOrderModelResponse>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<SalesOrderModelResponse>>>> GetAll()
        {
            var response = new CommonResponseModel<IEnumerable<SalesOrderModelResponse>>();
            var result = saleOrderQueries.Get();
            return Ok(response.SetResult(true, String.Empty).SetData(result));
        }

        [HttpGet]
        [Route("with_query")]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<SalesOrderModelResponse>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<SalesOrderModelResponse>>>> Get(
            [FromQuery] SalesOrderQueryModel query)
        {
            var response = new CommonResponseModel<IEnumerable<SalesOrderModelResponse>>();
            var result = saleOrderQueries.Get(query);
            return Ok(response.SetResult(true, String.Empty).SetData(result));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CommonResponseModel<SalesOrderModelResponse>), 200)]
        [AllowAnonymous]
        public async Task<ActionResult<CommonResponseModel<SalesOrderModelResponse>>> GetOne(string id)
        {
            var response = new CommonResponseModel<SalesOrderModelResponse>();
            var result = await saleOrderQueries.GetDetail(id);
            return Ok(response.SetResult(true, String.Empty).SetData(result));
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
        public async Task<ActionResult<CommonResponseModel<object>>> Update(string id,
            [FromBody] UpdateSalesOrderCommand command)
        {
            if (command == null)
                return BadRequest();
            var response = new CommonResponseModel<object>();
            var result = await mediator.Send(command);
            return Ok(result.Success ? response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
                     : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
        }
        [HttpPut]
        [Route("status/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<object>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<object>>> Status(string id,
           [FromBody] UpdateSalesOrderCommand command)
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
