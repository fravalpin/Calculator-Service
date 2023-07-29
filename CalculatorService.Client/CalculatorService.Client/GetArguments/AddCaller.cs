using System.Text;

namespace CalculatorService.Client.GetArguments
{
    internal class AddCaller : ICaller
    {

        public AddCaller(string[] cmdArgs, string url)
        {
            if (cmdArgs.Length > 4 || cmdArgs.Length < 2)
                throw new ArgumentException();

            string addends = cmdArgs[2];
            Content = new("{\"Addends\" : " + addends + "}", Encoding.UTF8, "application/json");
            Url = url + "add";

            if (cmdArgs.Length == 4)
                TrackingID = cmdArgs[3];
        }

        public StringContent Content { get; }

        public string Url { get; }

        public string? TrackingID { get; }
    }
}
