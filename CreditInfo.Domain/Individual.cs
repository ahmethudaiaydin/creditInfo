using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CreditInfo.Domain
{
    public class Individual
    {
        [Key]
        public string CustomerCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirt { get; set; }
        public Dictionary<IdentificationType, string> Identifications { get; set; }

    }

    public class IndividualValidator : AbstractValidator<Individual>
    {
        public IndividualValidator()
        {
            RuleFor(x => x.DateOfBirt).Must(BeBetween18And99);
        }

        protected bool BeBetween18And99(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int dobYear = date.Year;

            if (dobYear <= currentYear + 99 && dobYear > (currentYear - 18))
            {
                return true;
            }

            return false;
        }
    }
}