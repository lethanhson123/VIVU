using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VIVU.Common.Models;
using VIVU.Shared.Model;

namespace VIVU.API.Controllers
{
    [Route("api/v{version:apiVersion}/market_leads")]
    [ApiVersion("1.0")]
    [ApiController]
    public class MarketLeadsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMarketLeadQueries marketLeadQueries;

        public MarketLeadsController(IMediator mediator, IMarketLeadQueries marketLeadQueries)
        {
            this.mediator = mediator;
            this.marketLeadQueries = marketLeadQueries;
        }

        [HttpGet]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<MarketLeadModel>>>> GetAll(
            [FromQuery] string? keywords)
        {
            var response = new CommonResponseModel<IEnumerable<MarketLeadModel>>();
            var result = marketLeadQueries.Get();
            return Ok(response.SetResult(true, String.Empty).SetData(result));
        }

        [HttpGet]
        [Route("with_query")]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<MarketLeadModel>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<MarketLeadModel>>>> Get(
            [FromQuery] MarketLeadQueryModel query)
        {
            var response = new CommonResponseModel<IEnumerable<MarketLeadModel>>();
            var result = marketLeadQueries.Get(query);
            return Ok(response.SetResult(true, String.Empty).SetData(result));
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CommonResponseModel<MarketLeadModel>), 200)]
        public async Task<ActionResult<CommonResponseModel<MarketLeadModel>>> GetOne(string id)
        {
            var response = new CommonResponseModel<IEnumerable<MarketLeadModel>>();
            var result = marketLeadQueries.Get(id);
            return Ok(response.SetResult(true, String.Empty).SetData(result));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<MarketLeadModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CommonResponseModel<MarketLeadModel>>> Create(
            [FromBody] CreateMarketLeadCommand command)
        {
            var response = new CommonResponseModel<MarketLeadModel>();
            var result = await mediator.Send(command);
            return Ok(result.Success ? response.SetData(result.Data).SetResult(result.Success, result.Message ?? String.Empty)
                  : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
             );
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<object>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<object>>> Update(string id,
            [FromBody] UpdateMarketLeadCommand command)
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
            var result = await mediator.Send(new DeleteMarketLeadCommand { Id = id });
            return Ok(result.Success ? response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty)
                     : response.SetData(null).SetResult(result.Success, result.Message ?? String.Empty));
        }
    }
}
