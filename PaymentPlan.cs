public readonly struct PaymentPlan
{
    public PaymentPlan(int Id, string Name, int MonthsToPayOff)
    {   
        this.Id = Id;
        this.Name = Name;
        this.MonthsToPayOff = MonthsToPayOff;
    }

    public int Id { get; init; }
    public string Name { get; init; }
    public int MonthsToPayOff { get; init; }
}