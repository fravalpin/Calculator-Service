using System.Text;

namespace CalculatorService.Client.GetArguments
{
    internal class DivCaller : ICaller
    {

        public DivCaller(string[] cmdArgs, string url)
        {
            if (cmdArgs.Length > 5 || cmdArgs.Length < 2 || cmdArgs.Length == 3)
                throw new ArgumentException();

            string dividend = cmdArgs[2];
            string divisor = cmdArgs[3];
            Content = new("{\"dividend\" : " + dividend + ", \"divisor\": " + divisor + "}", Encoding.UTF8, "application/json");
            Url = url + "Calculator/div";

            if (cmdArgs.Length == 5)
                TrackingID = cmdArgs[4];
        }

        public StringContent Content { get; }

        public string Url { get; }

        public string? TrackingID { get; }
    }
}
