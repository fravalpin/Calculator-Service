using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Server.Domain
{
    public class Factor
    {

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
        }

        public double Value { get; }
    }
}
