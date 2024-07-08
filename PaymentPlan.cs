public readonly struct PaymentPlan
{
    public PaymentPlan(String Name, int MonthsToPayOff)
    {
        this.Name = Name;
        this.MonthsToPayOff = MonthsToPayOff;
    }

    public String Name { get; init; }
    public int MonthsToPayOff { get; init; }
}