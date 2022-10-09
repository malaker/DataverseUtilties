using Malaker.DataverseUtilities.DataversePdfEngine.Engines;

namespace Malaker.DataverseUtilities.DataversePdfEngine.Abstractions
{
    public interface IPdfEngine
    {
        string ConvertHtmlToPdf(string htmlContent, PdfSettingsGeneration settings);
    }
}