public class CreditLine
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Balance { get; set; }
    public double InterestRate { get; set;}
    public double MinMonthlyPayment { get; set; }
}

public class CreditLineRequest
{
    public required List<CreditLine> CreditLines { get; set; }
    public double ExtraPayment { get; set; }
}
