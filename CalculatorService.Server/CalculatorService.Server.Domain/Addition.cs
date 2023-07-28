namespace CalculatorService.Server.Domain
{
    public class Addition
    {

        public Addition(double[] operands)
        {
            if(operands == null )  
                throw new ArgumentNullException("The request should include at least two numeric operands to add"); 
            if(operands.Length < 2)
                throw new ArgumentException("The request should include at least two numeric operands to add");

            Value = operands.Sum();
        }

        public double Value { get; }
    }
}