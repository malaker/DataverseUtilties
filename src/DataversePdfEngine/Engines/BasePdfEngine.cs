using iText.Html2pdf;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Malaker.DataverseUtilities.DataversePdfEngine.Abstractions;
using System;
using System.IO;

namespace Malaker.DataverseUtilities.DataversePdfEngine.Engines
{
    public class BasePdfEngine : IPdfEngine
    {
        public string ConvertHtmlToPdf(string htmlContent, PdfSettingsGeneration settings)
        {
            using (var ms = new MemoryStream())
            {
                ConverterProperties converterProperties = new ConverterProperties();
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                pdf.SetDefaultPageSize(PageSize.LEGAL);
              


                var document = HtmlConverter.ConvertToDocument(htmlContent, pdf, converterProperties);

                RenderPageNumber(document, settings.NumbericSettings);

                document.Close();

                byte[] pdfBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(pdfBytes);
                return base64String;
            }
        }

        public void RenderPageNumber(Document document, PageNumbericSettings settings)
        {
            if (settings.IsTurnedOn)
            {
                int numberOfPages = document.GetPdfDocument().GetNumberOfPages();
                for (int i = 1; i <= numberOfPages; i++)
                {
                    var p = document.GetPdfDocument().GetPage(i);
                    var size = p.GetPageSize();


                    // Write aligned text to the specified by parameters point
                    var paragraph = new Paragraph(String.Format(settings.Format, i, numberOfPages));

                    float pageX = 0;
                    float pageY = 0;

                    if (settings.Position == PageNumberingPosition.BottomLeft)
                    {
                        pageX = size.GetLeft() + document.GetLeftMargin();
                        pageY = size.GetBottom() + 30;
                    }
                    else if (settings.Position == PageNumberingPosition.BottomRight)
                    {
                        pageX = size.GetRight() - document.GetRightMargin();
                        pageY = size.GetBottom() + 30;
                    }
                    else if (settings.Position == PageNumberingPosition.BottomCenter)
                    {
                        pageX = size.GetRight() / 2;
                        pageY = size.GetBottom() + 30;
                    }
                    else if (settings.Position == PageNumberingPosition.TopCenter)
                    {
                        pageX = size.GetRight() / 2;
                        pageY = size.GetTop() - 30;
                    }
                    else if (settings.Position == PageNumberingPosition.TopLeft)
                    {
                        pageX = size.GetLeft() + document.GetLeftMargin();
                        pageY = size.GetTop() - 30;
                    }
                    else if (settings.Position == PageNumberingPosition.TopRight)
                    {
                        pageX = size.GetRight() - document.GetRightMargin();
                        pageY = size.GetTop() - 30;
                    }

                    document.ShowTextAligned(paragraph,
                            pageX, pageY, i, TextAlignment.CENTER, VerticalAlignment.TOP, 0);
                }
            }
        }
    }
}