namespace CalculatorService.Server.Domain.Calculations
{
    public class Division : ICalculation
    {
        private readonly double _dividend;
        private readonly double _divisor;

        public Division(double dividend, double divisor)
        {
            if (divisor == 0) throw new DivideByZeroException();

            _dividend = dividend;
            _divisor = divisor;

            Quotient = Math.Round(dividend / divisor, MidpointRounding.ToZero);
            Remainder = dividend % divisor;
        }

        public double Quotient { get; }
        public double Remainder { get; }

        public string Operation => "Div";

        public override string ToString()
        {
            return $"{_dividend} / {_divisor} = q:{Quotient} r:{Remainder}";
        }
    }
}