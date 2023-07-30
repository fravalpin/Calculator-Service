using System.Text;

namespace CalculatorService.Client.GetArguments
{
    internal class SqrtCaller : ICaller
    {
        public SqrtCaller(string[] cmdArgs, string url)
        {
            if (cmdArgs.Length > 4 || cmdArgs.Length < 2)
                throw new ArgumentException();

            string number = cmdArgs[2];
            Content = new("{\"number\" : " + number + "}", Encoding.UTF8, "application/json");
            Url = url + "Calculator/sqrt";

            if (cmdArgs.Length == 5)
                TrackingID = cmdArgs[4];
        }

        public StringContent Content { get; }

        public string Url { get; }

        public string? TrackingID { get; }
    }
}
