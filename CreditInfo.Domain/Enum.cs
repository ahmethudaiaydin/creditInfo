namespace CreditInfo.Domain
{
    public enum Gender
    {
        Male,
        Female,
        NotSpecified // For whome that prefers not to share
    }
    public enum Currency
    {
        CZK,
        TRY
    }

    public enum Role
    {
        MainDebtor,
        CoDebtor,
        Guarantor
    }

    public enum Phase
    {
        Open,
        Closed
    }

    public enum IdentificationType
    {
        NationalId,
        PassportNumber
    }

    public enum Status
    {
        InProgress,
        Succeed,
        Failed,
    }


}
