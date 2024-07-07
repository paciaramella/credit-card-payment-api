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
}
