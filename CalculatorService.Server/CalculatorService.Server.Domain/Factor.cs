using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Server.Domain
{
    public class Factor
    {
        private readonly double[] _factors;

        public Factor(double[] factors)
        {
            _factors = factors;
        }

        public double Calculate()
        {
            if (_factors == null)
                throw new ArgumentNullException("The request should include at least two numeric operands to add");
            if (_factors.Length < 2)
                throw new ArgumentException("The request should include at least two numeric operands to add");

            double result = _factors[0];
            for (int i = 1; i < _factors.Length; i++)
            {
                result *= _factors[i];
            }

            return result;
        }
    }
}
