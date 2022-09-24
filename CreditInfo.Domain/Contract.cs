using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CreditInfo.Domain
{
    public class Contract
    {
        [Key]
        public string ContractCode { get; set; }

        public Phase Phase { get; set; }

        public Price OriginalAmount { get; set; }

        public Price InstallmentAmount { get; set; }

        public Price CurrentBalance { get; set; }

        public Price OverDueBalance { get; set; }

        public DateTime DateOfLastPayment { get; set; }

        public DateTime NextPaymentDate { get; set; }

        public DateTime DateAccountOpened { get; set; }

        public DateTime RealEndDate { get; set; }

        public List<Individual> Individuals { get; set; }

        public List<SubjectRole> SubjectRoles { get; set; }

    }

    public class ContractValidator : AbstractValidator<Contract>
    {
        public ContractValidator()
        {
            RuleFor(x => x.ContractCode).NotEmpty();
            RuleFor(x => x.DateOfLastPayment).LessThan(x => x.NextPaymentDate).WithMessage("Date Of Last Payment must be less than Next Payment Date");
            RuleFor(x => x.DateAccountOpened).LessThan(x => x.DateOfLastPayment);

            // I assumed that the currencies will be in same. Otherwise we need to introduce currency exchange rates.
            RuleFor(x => x.SubjectRoles.Sum(y => y.GuaranteeAmount.Amount) < x.OriginalAmount.Amount);

            // Not best practice to create an object here.
            RuleForEach(x => x.Individuals).SetValidator(new IndividualValidator());
        }
    }

}