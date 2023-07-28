namespace CalculatorService.Server.Domain
{
    public class Subtraction
    {

        public Subtraction(double minuend, double subtrahend)
        {
            Value = minuend + subtrahend;
        }
        public double Value { get; }

    }
}