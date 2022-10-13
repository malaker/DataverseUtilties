using Newtonsoft.Json;


namespace Malaker.DataverseUtilities.DataversePdfEngine.Engines
{
    public class PdfSettingsGeneration
    {
        [JsonProperty("FontSettings")]
        public PdfFontSettings FontSettings { get; set; }

        [JsonProperty("PageNumbericSettings")]
        public PageNumbericSettings NumbericSettings { get; set; }

        public static PdfSettingsGeneration Default = new PdfSettingsGeneration()
        { FontSettings = new PdfFontSettings() { LoadPluginsFonts = true }, NumbericSettings = new PageNumbericSettings() { IsTurnedOn = false } };
    }
}
