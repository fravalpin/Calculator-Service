using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService.Client.GetArguments
{
    internal interface ICaller
    {
        public StringContent Content { get; }
        public string Url { get; }
        public string? TrackingID { get; }
    }
}
