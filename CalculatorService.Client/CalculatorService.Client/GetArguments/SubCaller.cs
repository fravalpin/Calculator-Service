using System.Text;

namespace CalculatorService.Client.GetArguments
{
    internal class SubCaller : ICaller
    {

        public SubCaller(string[] cmdArgs, string url)
        {
            if (cmdArgs.Length > 5 || cmdArgs.Length < 2 || cmdArgs.Length == 3)
                throw new ArgumentException();

            string minuend = cmdArgs[2];
            string subtrahend = cmdArgs[3];
            Content = new("{\"minuend\" : " + minuend + ", \"subtrahend\": " + subtrahend + "}", Encoding.UTF8, "application/json");
            Url = url + "sub";

            if (cmdArgs.Length == 5)
                TrackingID = cmdArgs[4];
        }

        public StringContent Content { get; }

        public string Url { get; }

        public string? TrackingID { get; }
    }
}
