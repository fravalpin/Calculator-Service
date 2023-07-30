using System.Text;

namespace CalculatorService.Client.GetArguments
{
    internal class MultCaller : ICaller
    {

        public MultCaller(string[] cmdArgs, string url)
        {
            if (cmdArgs.Length > 4 || cmdArgs.Length < 2)
                throw new ArgumentException();

            string factors = cmdArgs[2];
            Content = new("{\"Factors\" : " + factors + "}", Encoding.UTF8, "application/json");
            Url = url + "Calculator/mult";

            if (cmdArgs.Length == 4)
                TrackingID = cmdArgs[3];
        }

        public StringContent Content { get; }

        public string Url { get; }

        public string? TrackingID { get; }
    }
}
