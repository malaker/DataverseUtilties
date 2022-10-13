using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Font;
using iText.Layout.Properties;
using Malaker.DataverseUtilities.DataversePdfEngine.Abstractions;
using System;
using System.IO;
using System.Reflection;

namespace Malaker.DataverseUtilities.DataversePdfEngine.Engines
{
    public class BasePdfEngine : IPdfEngine
    {
        public virtual string ConvertHtmlToPdf(string htmlContent, PdfSettingsGeneration settings)
        {
            using (var ms = new MemoryStream())
            {
                ConverterProperties converterProperties = new ConverterProperties();
                LoadFonts(converterProperties, settings);

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

        protected virtual void LoadFonts(ConverterProperties converterProperties, PdfSettingsGeneration settings)
        {
            if (settings.FontSettings.LoadPluginsFonts)
            {
                var libDirName = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var libFontDir = System.IO.Path.Combine(libDirName, "Fonts");
                bool libFontInCurrentFolderExists = Directory.Exists(libFontDir);
                string ttfFolderPath;
                if (libFontInCurrentFolderExists)
                {
                    ttfFolderPath = libFontDir;
                }
                else
                {
                    var rootLibFolder = Directory.GetParent(
                   Directory.GetParent(libDirName).FullName
                   ).FullName;
                    ttfFolderPath = System.IO.Path.Combine(rootLibFolder, "content", "Fonts");
                }
                FontProvider fontProvider = new DefaultFontProvider();
                fontProvider.AddDirectory(ttfFolderPath);
                converterProperties.SetFontProvider(fontProvider);
            }
        }

        protected virtual void RenderPageNumber(Document document, PageNumbericSettings settings)
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