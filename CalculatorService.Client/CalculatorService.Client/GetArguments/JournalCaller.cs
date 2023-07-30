using System.Text;

namespace CalculatorService.Client.GetArguments
{
    internal class JournalCaller : ICaller
    {

        public JournalCaller(string[] cmdArgs, string url)
        {
            if (cmdArgs.Length > 3 || cmdArgs.Length < 2)
                throw new ArgumentException();

            TrackingID = cmdArgs[2];
            Content = new("{\"Id\" : \"" + TrackingID + "\"}", Encoding.UTF8, "application/json");
            Url = url + "journal/query";

        }

        public StringContent Content { get; }

        public string Url { get; }

        public string? TrackingID { get; }
    }
}
