using AutoMapper;
using PAY_UP.Application.Abstracts.Repositories;
using PAY_UP.Application.Abstracts.Services;
using PAY_UP.Application.Dtos;
using PAY_UP.Application.Dtos.Common;
using PAY_UP.Application.Dtos.Creditors;
using PAY_UP.Common.Helpers;
using PAY_UP.Domain.Creditors;

namespace PAY_UP.Application.Services{
    public class CreditorService : ICreditorService
    {
        private readonly ICreditorRepository _creditorRepo;
        private readonly IMapper _mapper;

        public CreditorService(ICreditorRepository creditorRepo, IMapper mapper)
        {
            _creditorRepo = creditorRepo;
            _mapper = mapper;
        }

        public async Task<ResponseObject<GetCreditorDto>> CreateCreditorAsync(CreateCreditorDto creditor)
        {
            var creditorToCreate = _mapper.Map<Creditor>(creditor);
            creditorToCreate.Balance = creditor.AmountOwed;
            var result = await _creditorRepo.CreateCreditorAsync(creditorToCreate);
            if(result == null){
                return new ResponseObject<GetCreditorDto>().CreateResponse($"Creditor could not be created", false, null);
            }
            return new ResponseObject<GetCreditorDto>().CreateResponse("Creditor created successfully", true, _mapper.Map<GetCreditorDto>(result));
        }

        public async Task<ResponseObject<bool>> DeleteCreditorAsync(Guid id)
        {
            var result = await _creditorRepo.DeleteCreditorAsync(id);
            if(!result){
                return new ResponseObject<bool>().CreateResponse($"Creditor could not be deleted", false, result);
            }
            return new ResponseObject<bool>().CreateResponse($"Creditor deleted successfully", true, result);
        }

        public async Task<ResponseObject<IEnumerable<GetCreditorDto>>> GetAllCreditorsAsync()
        {
            var creditors = await _creditorRepo.GetCreditorsAsync();
            return new ResponseObject<IEnumerable<GetCreditorDto>>().CreateResponse($"{creditors.Count()} retrieved successfully", true,
                    _mapper.Map<IEnumerable<GetCreditorDto>>(creditors));
        }

        public async Task<ResponseObject<GetCreditorDto>> GetCreditorAsync(Guid id)
        {
            var creditor = await _creditorRepo.GetCreditorAsync(id);
            if(creditor == null){
                return new ResponseObject<GetCreditorDto>().CreateResponse($"No creditor with Id: {id}", false, null);
            }
            return new ResponseObject<GetCreditorDto>().CreateResponse($"Creditor retrieved successfully", true, 
                    _mapper.Map<GetCreditorDto>(creditor));
        }

        public async Task<ResponseObject<IEnumerable<GetCreditorDto>>> GetCreditorsForUserAsync(string userId)
        {
            var creditors = await _creditorRepo.GetCreditorsForUserAsync(userId);
            return new ResponseObject<IEnumerable<GetCreditorDto>>().CreateResponse($"{creditors.Count()} retrieved successfully for user with Id: {userId}", true,
                    _mapper.Map<IEnumerable<GetCreditorDto>>(creditors));
        }

        public async Task<ResponseObject<GetCreditorDto>> MakePayment(PaymentDto payment)
        {
            var creditor = await _creditorRepo.GetCreditorAsync(payment.Id);
            creditor.AmountPaid += payment.Amount;
            creditor.Balance -= payment.Amount;
            creditor.Installment += 1;
            var result = await _creditorRepo.UpdateCreditorAsync(creditor);
            if(result == null){
                return new ResponseObject<GetCreditorDto>().CreateResponse($"Payment could not be updated", false, null);
            }
            return new ResponseObject<GetCreditorDto>().CreateResponse($"Payment updated successfully", true, 
                _mapper.Map<GetCreditorDto>(result));
        }

        public async Task<ResponseObject<GetCreditorDto>> UpdateCreditorAsync(UpdateCreditorDto creditor)
        {
            var creditorToUpdate = _mapper.Map<Creditor>(creditor);
            var result = await _creditorRepo.UpdateCreditorAsync(creditorToUpdate);
            if(result == null){
                return new ResponseObject<GetCreditorDto>().CreateResponse($"Creditor could not be updated", false, null);
            }
            return new ResponseObject<GetCreditorDto>().CreateResponse($"Creditor updated successfully", true, 
                _mapper.Map<GetCreditorDto>(result));
        }

        private void CalculateBalance(){

        }
    }
}