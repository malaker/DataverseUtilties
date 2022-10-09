using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Malaker.DataverseUtilities.DataversePdfEngine.Engines
{
    public class PageNumbericSettings
    {
        [JsonProperty("IsTurnedOn")]
        public bool IsTurnedOn { get; set; }

        /// <summary>
        /// Example: page {0} of {1}
        /// </summary>
        [JsonProperty("Format")]
        public string Format { get; set; }

        [JsonProperty("Position")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PageNumberingPosition Position { get; set; }

        [JsonProperty("FontFamily")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PdfFontFamily PdfFontFamily { get; set; }

    }
}
