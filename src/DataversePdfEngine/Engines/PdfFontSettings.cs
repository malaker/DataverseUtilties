using Newtonsoft.Json;

namespace Malaker.DataverseUtilities.DataversePdfEngine.Engines
{
    public class PdfFontSettings
    {
        [JsonProperty("LoadPluginsFonts")]
        public bool LoadPluginsFonts { get; set; }
    }
}
