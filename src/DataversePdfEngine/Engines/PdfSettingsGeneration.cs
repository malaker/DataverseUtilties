using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malaker.DataverseUtilities.DataversePdfEngine.Engines
{
    public class PdfSettingsGeneration
    {
        [JsonProperty("PageNumbericSettings")]
        public PageNumbericSettings NumbericSettings { get; set; }

        public static PdfSettingsGeneration Default = new PdfSettingsGeneration()
        { NumbericSettings = new PageNumbericSettings() { IsTurnedOn = false } };
    }
}
