using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PAY_UP.Application.Abstracts.Services;
using PAY_UP.Application.Dtos;
using PAY_UP.Application.Dtos.Common;
using PAY_UP.Application.Dtos.Creditors;
using PAY_UP.Common.Helpers;

namespace PAY_UP.Api.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    [Authorize]
    public class CreditorsController : ControllerBase
    {
        private readonly ICreditorService _creditorService;

        public CreditorsController(ICreditorService creditorService)
        {
            _creditorService = creditorService;
        }

        [HttpGet(Name = nameof(GetAllCreditors)), ProducesResponseType(typeof(ResponseObject<GetCreditorDto>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllCreditors()
        {
            var result = await _creditorService.GetAllCreditorsAsync();
            return Ok(result);
        }

        [HttpGet("creditors-for-user", Name = nameof(GetCreditorsForUser)), ProducesResponseType(typeof(ResponseObject<GetCreditorDto>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> GetCreditorsForUser(string userId)
        {
            var result = await _creditorService.GetCreditorsForUserAsync(userId);
            return Ok(result);
        }

        [HttpGet("{id}", Name = nameof(GetCreditor)), ProducesResponseType(typeof(ResponseObject<GetCreditorDto>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> GetCreditor(Guid Id)
        {
            var result = await _creditorService.GetCreditorAsync(Id);
            if (!result.IsSuccessfull)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPost(Name = nameof(CreateCreditor)), ProducesResponseType(typeof(ResponseObject<GetCreditorDto>), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> CreateCreditor(CreateCreditorDto creditor)
        {
            var result = await _creditorService.CreateCreditorAsync(creditor);
            if (!result.IsSuccessfull)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
        [HttpPost("make-repayment", Name = nameof(MakeRepayment)), ProducesResponseType(typeof(ResponseObject<GetCreditorDto>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> MakeRepayment(PaymentDto payment)
        {
            var result = await _creditorService.MakePayment(payment);
            if (!result.IsSuccessfull)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpPut(Name = nameof(UpdateCreditor)), ProducesResponseType(typeof(ResponseObject<GetCreditorDto>), StatusCodes.Status200OK), ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateCreditor(UpdateCreditorDto creditor)
        {
            var result = await _creditorService.UpdateCreditorAsync(creditor);
            if (!result.IsSuccessfull)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [HttpDelete("{id}", Name = nameof(DeleteCreditor)), ProducesResponseType(typeof(ResponseObject<bool>), StatusCodes.Status204NoContent), ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteCreditor(Guid id)
        {
            var result = await _creditorService.DeleteCreditorAsync(id);
            if (!result.IsSuccessfull)
            {
                return BadRequest(result.Message);
            }
            return NoContent();
        }
    }
}