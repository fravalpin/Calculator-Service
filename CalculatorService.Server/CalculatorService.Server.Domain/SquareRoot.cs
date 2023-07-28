namespace CalculatorService.Server.Domain
{
    public class SquareRoot
    {
        public SquareRoot(double number)
        {
            if (number < 0) throw new ArgumentException("Number must be greater than or equal to zero");

            Value = Math.Sqrt(number);
        }
        public double Value { get; }
    }
}