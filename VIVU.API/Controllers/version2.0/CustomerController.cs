using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VIVU.Common.Models;
using VIVU.Data.Entities;

namespace VIVU.API.Controllers
{
    [Route("api/v{version:apiVersion}/customer/")]
    [ApiVersion("2.0")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public CustomerController()
        {

        }

        [HttpGet]
        [ProducesResponseType(typeof(CommonResponseModel<IEnumerable<Blog>>), 200)]
        public async Task<ActionResult<CommonResponseModel<IEnumerable<Blog>>>> GetAll()
        {
            return Ok();
        }
    }
}
