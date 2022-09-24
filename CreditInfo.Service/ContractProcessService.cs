using CreditInfo.Data;
using CreditInfo.Domain;
using FluentValidation;
using TanvirArjel.EFCore.GenericRepository;

namespace CreditInfo.Service
{
    public class ContractProcessService : IContractProcessService
    {
        private readonly IValidator<Contract> _contractValidator;
        private readonly IRepository<ContractContext> _contractRepository;

        public ContractProcessService(IValidator<Contract> contractValidator,
            IRepository<ContractContext> contractRepository)
        {
            _contractValidator = contractValidator;
            _contractRepository = contractRepository;
        }
        public async Task<ContractProcess> ProcessContract(Contract contract)
        {
         

            // Validate object
            // Save it.
            // Save process record.

            var processResult = ContractProcess.Create("CreditInfo", contract.ContractCode);


            // TODO: That part could be desinged in aspect oriented programming...

            var validationResult = await _contractValidator.ValidateAsync(contract);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(x => processResult.AddError(x.ErrorMessage));
                processResult.ProcessFailed();

                await _contractRepository.InsertAsync(processResult);
                await _contractRepository.SaveChangesAsync();
                return processResult;
            }


            processResult.ProcessSucceed();

            await _contractRepository.InsertAsync(processResult);
            await _contractRepository.InsertAsync(contract);
            await _contractRepository.SaveChangesAsync();

            return processResult;
        }
    }

}