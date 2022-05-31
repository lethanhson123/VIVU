using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VIVU.API.Controllers
{
    [Route("api/v{version:apiVersion}/blog/")]
    [ApiVersion("1.0")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
    }
}
