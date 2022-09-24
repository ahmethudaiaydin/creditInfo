namespace CreditInfo.Domain
{
    public record Price : IEquatable<Price>
    {
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

    }
}
