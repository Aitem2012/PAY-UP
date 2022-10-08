using AutoMapper;
using PAY_UP.Application.Abstracts.Repositories;
using PAY_UP.Application.Abstracts.Services;
using PAY_UP.Application.Dtos.Common;
using PAY_UP.Application.Dtos.Debtors;
using PAY_UP.Common.Helpers;
using PAY_UP.Domain.Debtors;

namespace PAY_UP.Application.Services{
    public class DebitorService : IDebitorService
    {
        private readonly IDebitorRepository _debitorRepo;
        private readonly IMapper _mapper;
        public DebitorService(IDebitorRepository debitorRepo, IMapper mapper)
        {
            _debitorRepo = debitorRepo;
            _mapper = mapper;
        }

        public async Task<ResponseObject<GetDebtorDto>> CreateDebtorAsync(CreateDebtorDto debtor)
        {
            var debtorToCreate = _mapper.Map<Debtor>(debtor);
            debtorToCreate.Balance = debtor.AmountOwed;
            var result = await _debitorRepo.CreateDebtorAsync(debtorToCreate);
            if(result == null){
                return new ResponseObject<GetDebtorDto>().CreateResponse($"Debtor could not be created", false, null);
            }

            return new ResponseObject<GetDebtorDto>().CreateResponse($"Debtor created successfully", true,
                     _mapper.Map<GetDebtorDto>(result));
        }

        public async Task<ResponseObject<bool>> DeleteDebtorAsync(Guid id)
        {
            var result = await _debitorRepo.DeleteDebtorAsync(id);
            if(!result){
                return new ResponseObject<bool>().CreateResponse($"Debtor could not be deleted", false, result);
            }
            return new ResponseObject<bool>().CreateResponse($"Debtor deleted successfully", true, result);
        }

        public async Task<ResponseObject<IEnumerable<GetDebtorDto>>> GetAllDebtorsAsync()
        {
            var debtors = await _debitorRepo.GetAllDebtorsAsync();
            return new ResponseObject<IEnumerable<GetDebtorDto>>().CreateResponse($"Retrieved {debtors.Count()} Debtor successfully", true, 
                    _mapper.Map<IEnumerable<GetDebtorDto>>(debtors));
        }

        public async Task<ResponseObject<GetDebtorDto>> GetDebtorAsync(Guid id)
        {
            var debtor = await _debitorRepo.GetDebtorAsync(id);
            if(debtor == null){
                return new ResponseObject<GetDebtorDto>().CreateResponse($"Debtor with Id {id} does not exist", false, null);
            }
            return new ResponseObject<GetDebtorDto>().CreateResponse($"Debtor retrieved successfully", true, 
                    _mapper.Map<GetDebtorDto>(debtor));
        }

        public async Task<ResponseObject<IEnumerable<GetDebtorDto>>> GetDebtorsForUserAsync(string userId)
        {
            var debtors = await _debitorRepo.GetDebitorsForUserAsync(userId);
            return new ResponseObject<IEnumerable<GetDebtorDto>>().CreateResponse($"Retrieved {debtors.Count()} Debtor successfully", true, 
                    _mapper.Map<IEnumerable<GetDebtorDto>>(debtors));
        }

        public async Task<ResponseObject<GetDebtorDto>> MakePayment(PaymentDto payment)
        {
            var debtor = await _debitorRepo.GetDebtorAsync(payment.Id);
            if(debtor.Balance == 0){
                return new ResponseObject<GetDebtorDto>().CreateResponse($"You don't have outstanding payment from this debtor", false, null);
            }
            if (payment.Amount > debtor.Balance)
            {
                return new ResponseObject<GetDebtorDto>().CreateResponse($"Overpayment is not allowed. Please try again.", false, null);
            }
            debtor.AmountPaid += payment.Amount;
            debtor.Balance -= payment.Amount;
            debtor.Installment += 1;
            var result = await _debitorRepo.UpdateDebtorAsync(debtor);
            if(result == null){
                return new ResponseObject<GetDebtorDto>().CreateResponse($"Payment could not be updated", false, null);
            }
            return new ResponseObject<GetDebtorDto>().CreateResponse($"Payment updated successfully", true, 
                _mapper.Map<GetDebtorDto>(result));
        }

        public async Task<ResponseObject<GetDebtorDto>> UpdateDebtorAsync(UpdateDebtorDto debtor)
        {
            var debtorToUpdate = _mapper.Map<Debtor>(debtor);
            var result = await _debitorRepo.UpdateDebtorAsync(debtorToUpdate);
            if(result == null){
                return new ResponseObject<GetDebtorDto>().CreateResponse($"Debtor could not be updated", false, null);
            }
            return new ResponseObject<GetDebtorDto>().CreateResponse($"Debtor updated successfully", true, 
                    _mapper.Map<GetDebtorDto>(result));
        }
    }
}