
using CalculatorService.Client.GetArguments;
using System.Net.Http.Headers;
string[] cmdArgs = Environment.GetCommandLineArgs();
if (cmdArgs.Length <= 1 || (cmdArgs[1] == "-h" || cmdArgs[1] == "--help"))
    ShowHelpMessage();

ICaller? caller = GetCall(cmdArgs);
using HttpClient client = new();
SetHttpClientHeaders(client, caller.TrackingID);
await ProccesRequest(client, caller.Content, caller.Url);

static async Task ProccesRequest(HttpClient client, StringContent content, string url)
{
    HttpResponseMessage json = await client.PostAsync(url, content);
    Console.Write(await json.Content.ReadAsStringAsync());
}

static void SetHttpClientHeaders(HttpClient client, string? trackingID)
{
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    if (trackingID != null)
        client.DefaultRequestHeaders.Add("XEviTrackingId", trackingID);
    //client.DefaultRequestHeaders.TryAddWithoutValidation("X‐Evi‐Tracking‐Id", "test");
}

static ICaller? GetCall(string[] cmdArgs)
{
    StringContent? content = null;
    string url = "https://localhost:7123/Calculator/";
    ICaller? caller = null;
    switch (cmdArgs[1])
    {
        case "add":
            try { caller = new AddCaller(cmdArgs, url); }
            catch (Exception) { ShowHelpAddMessage(); }
            break;
        case "sub":
            try { caller = new SubCaller(cmdArgs, url); }
            catch (Exception) { ShowHelpSubMessage(); }
            break;
        case "mult":
            try { caller = new MultCaller(cmdArgs, url); }
            catch (Exception) { ShowHelpMultMessage(); }
            break;
        case "div":
            try { caller = new DivCaller(cmdArgs, url); }
            catch (Exception) { ShowHelpDivMessage(); }
            break;
        case "sqrt":
            try { caller = new SqrtCaller(cmdArgs, url); }
            catch (Exception) { ShowHelpSqrtMessage(); }
            break;
        default:
            ShowHelpMessage();
            break;
    }

    return caller;
}
static void ShowHelpMessage()
{
    Console.WriteLine("Usage CalculatorService-Client.exe Operation [values] [User-id] \n" +
           "Operation: add, sub, mult, div, sqrt, journal ");
    Environment.Exit(1);
}
static void ShowHelpAddMessage()
{
    Console.WriteLine("Usage CalculatorService-Client.exe add [values] [User-id] \n" +
           "values: Formtat [1,2...]");
    Environment.Exit(1);
}
static void ShowHelpSubMessage()
{
    Console.WriteLine("Usage CalculatorService-Client.exe sub minuend subtrahend [User-id] \n" +
           "minuend: number \n" +
           "subtrahend: number");

    Environment.Exit(1);
}
static void ShowHelpMultMessage()
{
    Console.WriteLine("Usage CalculatorService-Client.exe mult [values] [User-id] \n" +
           "values: Formtat [1,2...]");
    Environment.Exit(1);
}
static void ShowHelpDivMessage()
{
    Console.WriteLine("Usage CalculatorService-Client.exe div dividend divisor [User-id] \n" +
           "dividend: number \n" +
           "divisor: number");

    Environment.Exit(1);
}
static void ShowHelpSqrtMessage()
{
    Console.WriteLine("Usage CalculatorService-Client.exe sqrt number [User-id] \n" +
           "number: number \n");

    Environment.Exit(1);
}
