using Microsoft.AspNetCore.Mvc;
using PAY_UP.Application.Abstracts.Services;
using PAY_UP.Application.Dtos.Common;
using PAY_UP.Application.Dtos.Debtors;
using PAY_UP.Common.Helpers;

namespace PAY_UP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebtorsController : ControllerBase
    {
        private readonly IDebitorService _debitorService;

        public DebtorsController(IDebitorService debitorService)
        {
            _debitorService = debitorService;
        }

        [HttpGet(Name = nameof(GetAllDebitors)), ProducesResponseType(typeof(ResponseObject<IEnumerable<GetDebtorDto>>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllDebitors()
        {
            var result = await _debitorService.GetAllDebtorsAsync();
            return Ok(result);
        }

        [HttpGet("debitors-for-user", Name = nameof(GetDebitorsForUser)), ProducesResponseType(typeof(ResponseObject<IEnumerable<GetDebtorDto>>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> GetDebitorsForUser(string userId)
        {
            var result = await _debitorService.GetDebtorsForUserAsync(userId);
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetDebitor)), ProducesResponseType(typeof(ResponseObject<GetDebtorDto>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> GetDebitor(Guid Id)
        {
            var result = await _debitorService.GetDebtorAsync(Id);
            if (!result.IsSuccessfull)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPost(Name = nameof(CreateDebitor)), ProducesResponseType(typeof(ResponseObject<GetDebtorDto>), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> CreateDebitor(CreateDebtorDto Debitor)
        {
            var result = await _debitorService.CreateDebtorAsync(Debitor);
            if (!result.IsSuccessfull)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPost("make-repayment", Name = nameof(MakeDebtRepayment)), ProducesResponseType(typeof(ResponseObject<GetDebtorDto>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> MakeDebtRepayment(PaymentDto payment)
        {
            var result = await _debitorService.MakePayment(payment);
            if (!result.IsSuccessfull)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPut(Name = nameof(UpdateDebitor)), ProducesResponseType(typeof(ResponseObject<GetDebtorDto>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateDebitor(UpdateDebtorDto Debitor)
        {
            var result = await _debitorService.UpdateDebtorAsync(Debitor);
            if (!result.IsSuccessfull)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpDelete("{id}", Name = nameof(DeleteDebitor)), ProducesResponseType(typeof(ResponseObject<bool>), StatusCodes.Status204NoContent), ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteDebitor(Guid id)
        {
            var result = await _debitorService.DeleteDebtorAsync(id);
            if (!result.IsSuccessfull)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }
    }
}