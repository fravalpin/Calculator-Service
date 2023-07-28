namespace CalculatorService.Server.Domain
{
    public class Subtraction
    {
        private readonly double _minuend;
        private readonly double _subtrahend;

        public Subtraction(double minuend, double subtrahend)
        {
            _minuend = minuend;
            _subtrahend = subtrahend;
        }

        public double Calculate()
        {
            return _minuend + _subtrahend;
        }
    }
}