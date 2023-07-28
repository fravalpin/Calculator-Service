namespace CalculatorService.Server.Domain
{
    public class Division
    {

        public Division(double dividend, double divisor)
        {
            if (divisor == 0) throw new DivideByZeroException();


            Quotient = Math.Round(dividend / divisor, MidpointRounding.ToZero);
            Remainder = dividend % divisor;
        }

        public double Quotient { get; }
        public double Remainder { get; }
    }
}