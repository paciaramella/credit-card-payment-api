public class CreditCardPaymentHelper
{
    public static int CalculateMonthsToPayOff(CreditLine creditLine)
    {
        Console.Write("we get here!!!");
        decimal balance = creditLine.Balance;
        decimal monthlyInterestRate = creditLine.InterestRate / 12; // Convert annual rate to monthly rate
        decimal minMonthlyPayment = creditLine.MinMonthlyPayment;
        int months = 0;

        while (balance > 0)
        {
            // Calculate interest for the month
            decimal interestForMonth = balance * monthlyInterestRate;
            balance += interestForMonth;

            // Subtract the minimum payment
            balance -= minMonthlyPayment;

            // If balance goes negative, it means we've paid it off
            if (balance < 0)
            {
                balance = 0;
            }

            months++;
        }

        return months;
    }
    public static List<PaymentPlan> CalculateDebtSnowballPayoff(List<CreditLine> creditLines, decimal extraPayment)
    {
        List<PaymentPlan> payments = [];
        creditLines = creditLines.OrderBy(cl => cl.Balance).ToList();
        decimal totalPayment = 0;
        
        while (creditLines.Any(cl => cl.Balance > 0))
        {
            decimal monthPayment = 0;
            foreach (var creditLine in creditLines)
            {
                int totalMonths = 0;
                if (creditLine.Balance <= 0) {
                    string creditLineName = creditLine.Name ?? "";
                    var paymentPlan = new PaymentPlan(creditLineName, totalMonths);
                    payments.Add(paymentPlan);
                    continue;
                }

                decimal monthlyInterestRate = creditLine.InterestRate / 12;
                decimal interestForMonth = creditLine.Balance * monthlyInterestRate;
                creditLine.Balance += interestForMonth;

                decimal payment = creditLine.MinMonthlyPayment;
                if (monthPayment + creditLine.MinMonthlyPayment + extraPayment <= totalPayment)
                {
                    payment += extraPayment;
                    extraPayment = 0;
                }
                else if (creditLine.Balance <= payment)
                {
                    payment = creditLine.Balance;
                }
                creditLine.Balance -= payment;
                monthPayment += payment;
                totalMonths++;
            }
        }

        return payments;
    }
}
