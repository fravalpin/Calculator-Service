namespace CalculatorService.Server.Domain.Calculations
{
    public class Addition : ICalculation
    {

        private readonly double[] _operands;
        public Addition(double[] operands)
        {
            if (operands == null)
                throw new ArgumentNullException("The request should include at least two numeric operands to add");
            if (operands.Length < 2)
                throw new ArgumentException("The request should include at least two numeric operands to add");
            _operands = operands;
            Value = operands.Sum();
        }

        public double Value { get; }

        public string Operation => "Sum";

        public override string ToString()
        {
            return $"{string.Join(" + ", _operands)} = {Value}";
        }
    }
}