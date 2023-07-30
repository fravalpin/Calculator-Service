namespace CalculatorService.Server.Domain.Calculations
{
    public interface ICalculation
    {
        string Operation { get; }

        string ToString();
    }
}
