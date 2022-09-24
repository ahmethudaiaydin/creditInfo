using System.ComponentModel.DataAnnotations;

namespace CreditInfo.Domain
{
    public class ContractProcess
    {
        [Key]
        public int Id { get; set; }
        // The source of the file
        public string Provider { get; protected set; }

        public string ContractCode { get; protected set; }

        public Status Status { get; protected set; }

        public List<string> Errors { get; protected set; } = new();

        //Should be private but to access of EF, it is defined as public.
        public ContractProcess()
        {

        }

        public static ContractProcess Create(string provider, string contractCode)
        {
            return new()
            {
                Provider = provider,
                ContractCode = contractCode,
                Status = Status.InProgress,

            };
        }

        public void ProcessFailed()
        {
            Status = Status.Failed;
        }

        public void ProcessSucceed()
        {
            Status = Status.Succeed;
        }
        public void AddError(string Error)
        {
            Errors.Add(Error);
        }
    }
}
