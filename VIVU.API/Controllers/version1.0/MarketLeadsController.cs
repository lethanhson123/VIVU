﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        [HttpGet]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<MarketLeadModel>>>> GetAll(
            [FromQuery] string? keywords)
        {
            return Ok();
        }

        [HttpGet]
        [Route("with_query")]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<MarketLeadModel>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<MarketLeadModel>>>> Get(
            [FromQuery] MarketLeadQueryModel query)
        {
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CommonResponseModel<MarketLeadModel>), 200)]
        public async Task<ActionResult<CommonResponseModel<MarketLeadModel>>> GetOne(string id)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<MarketLeadModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<CommonResponseModel<MarketLeadModel>>> Create(
            [FromBody] CreateMarketLeadCommand command)
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<MarketLeadModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<MarketLeadModel>>> Update(string id,
            [FromBody] UpdateMarketLeadCommand command)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(CommonResponseModel<MarketLeadModel>), 200)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CommonResponseModel<MarketLeadModel>>> Delete(string id)
        {
            return Ok();
        }
    }
}
