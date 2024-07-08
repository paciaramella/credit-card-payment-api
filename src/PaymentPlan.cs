public readonly struct PaymentPlan
{
    public PaymentPlan(int Id, string Name, int MonthsToPayOff, double AmountPaidInInterest)
    {   
        this.Id = Id;
        this.Name = Name;
        this.MonthsToPayOff = MonthsToPayOff;
        this.AmountPaidInInterest = AmountPaidInInterest;
    }

    public int Id { get; init; }
    public string Name { get; init; }
    public int MonthsToPayOff { get; init; }
    public double AmountPaidInInterest { get; init; }
}