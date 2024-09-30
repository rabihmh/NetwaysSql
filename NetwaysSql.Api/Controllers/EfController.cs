using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetwaysSql.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class EfController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion(1)]
        public async Task<IActionResult> Test()
        {
            return Ok("Test");
        }

    }
}
