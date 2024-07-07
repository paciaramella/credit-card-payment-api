public class CreditLine
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal Balance { get; set; }
    public decimal InterestRate { get; set;}
    public decimal MinMonthlyPayment { get; set; }
}