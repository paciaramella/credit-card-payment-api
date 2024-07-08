public class CreditCardPaymentHelper
{
    public static int CalculateMonthsToPayOff(double balance, double interestRate, double monthlyPayment)
    {
        int months = 0;

        while (balance > 0)
        {
            balance += balance * interestRate / 12;
            balance -= monthlyPayment;
            months++;

            if (balance <= 0)
                break;
        }

        return months;
    }
    public static List<PaymentPlan> CalculateDebtSnowballPayoff(List<CreditLine> creditLines, double extraPayment)
    {
        // Sort the credit lines by balance
        var sortedCreditLines = creditLines.OrderBy(cl => cl.Balance).ToList();

        // List to store the result
        List<PaymentPlan> payoffResults = new List<PaymentPlan>();

        // Variable to keep track of total additional payment
        double totalExtraPayment = extraPayment;

        // Iterate over each credit line and calculate months to pay off
        foreach (var creditLine in sortedCreditLines)
        {
            int monthsToPayOff = CalculateMonthsToPayOff(creditLine.Balance, creditLine.InterestRate, creditLine.MinMonthlyPayment + totalExtraPayment);
            totalExtraPayment += creditLine.MinMonthlyPayment;

            payoffResults.Add(new PaymentPlan
            {
                Name = creditLine.Name,
                MonthsToPayOff = monthsToPayOff
            });
        }

        return payoffResults;
    }
}
