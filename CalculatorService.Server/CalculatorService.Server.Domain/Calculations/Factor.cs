using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Server.Domain.Calculations
{
    public class Factor : ICalculation
    {
        private readonly double[] _factors;

        public Factor(double[] factors)
        {
            if (factors == null)
                throw new ArgumentNullException("The request should include at least two numeric operands to add");
            if (factors.Length < 2)
                throw new ArgumentException("The request should include at least two numeric operands to add");

            Value = factors[0];
            for (int i = 1; i < factors.Length; i++)
            {
                Value *= factors[i];
            }

            _factors = factors;
        }
        public double Value { get; }

        public string Operation => "Mul";

        public override string ToString()
        {
            return $"{string.Join(" x ", _factors)} = {Value}";
        }
    }
}
