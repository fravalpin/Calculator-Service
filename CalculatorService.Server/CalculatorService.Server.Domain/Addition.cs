namespace CalculatorService.Server.Domain
{
    public class Addition
    {
        private double[] _operands;

        public Addition(double[] operands)
        {
            _operands = operands;
        }

        public double Calculate()
        {
            if(_operands == null )  
                throw new ArgumentNullException("The request should include at least two numeric operands to add"); 
            if(_operands.Length < 2)
                throw new ArgumentException("The request should include at least two numeric operands to add"); 

            return _operands.Sum();
        }
    }
}