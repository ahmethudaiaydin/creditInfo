using System.ComponentModel.DataAnnotations;

namespace CreditInfo.Domain
{
    public class SubjectRole
    {
        [Key]
        public string CustomerCode { get; set; }
        public Role Role { get; set; }
        public Price GuaranteeAmount { get; set; }

    }
}
