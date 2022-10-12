using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAY_UP.Application.Abstracts.Services;
using PAY_UP.Application.Dtos.Email;

namespace PAY_UP.Api.Controllers
{
    [ApiController, Authorize]
    [Route("api/[controller]")]
    public class SchedlingController : ControllerBase
    {
        private readonly ISchedulingService _schedulingService;

        public SchedlingController(ISchedulingService schedulingService)
        {
            _schedulingService = schedulingService;
        }

        [HttpPost(Name = nameof(ScheduleMailing)), ProducesResponseType(typeof(bool), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> ScheduleMailing(ScheduleEmailDto email)
        {
            var result = await _schedulingService.ScheduleEmail(email);
            return Ok(result);
        }

        [HttpPost("stop-reminder", Name = nameof(StopEmail)), ProducesResponseType(typeof(void), StatusCodes.Status204NoContent), ProducesDefaultResponseType]
        public IActionResult StopEmail(Guid debtorId)
        {
            _schedulingService.StopEmail(debtorId);
            return NoContent();
        }
    }
}