public class CreditCardPaymentHelper
{
    public static int CalculateMonthsToPayOff(double balance, double interestRate, double monthlyPayment)
    {
        int months = 0;

        while (balance > 0)
        {
            var currBalance = balance;
            balance += balance * interestRate / 12;
            balance -= monthlyPayment;
            // would not be able to pay off the balance at this rate and min monthly
            if (balance >= currBalance) {
                throw new Exception("Interest Rate too high for this monthly payment");
            }
            months++;

            if (balance <= 0)
                break;
        }

        return months;
    }

    public static List<PaymentPlan> CalculateMinPaymentPayoff(List<CreditLine> creditLines) {
        List<PaymentPlan> payoffResults = new List<PaymentPlan>();
        foreach (var creditLine in creditLines) {
            int monthsToPayOff = CalculateMonthsToPayOff(creditLine.Balance, creditLine.InterestRate, creditLine.MinMonthlyPayment);
            payoffResults.Add(new PaymentPlan
                {
                    Id = creditLine.Id,
                    Name = creditLine.Name ?? "",
                    MonthsToPayOff = monthsToPayOff
                });
        };
        return payoffResults;
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
                Id = creditLine.Id,
                Name = creditLine.Name ?? "",
                MonthsToPayOff = monthsToPayOff
            });
        }

        return payoffResults;
    }
}
