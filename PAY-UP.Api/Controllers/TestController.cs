using Microsoft.AspNetCore.Mvc;
using PAY_UP.Application.Dtos;

namespace PAY_UP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateSms(SmSDto sms)
        {
            return Ok();
        }
    }
}
