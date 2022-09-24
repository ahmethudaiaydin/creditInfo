using CreditInfo.Domain;

namespace CreditInfo.Service
{
    public interface IContractProcessService
    {
        Task<ContractProcess> ProcessContract(Contract contract);
    }
}