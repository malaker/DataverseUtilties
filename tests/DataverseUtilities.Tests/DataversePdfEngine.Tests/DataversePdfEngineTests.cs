using Malaker.DataverseUtilities.DataversePdfEngine;
using Malaker.DataverseUtilities.DataversePdfEngine.Engines;
using Malaker.DataverseUtilities.Tests;
using Malaker.DataverseUtilities.Tests.TestUtils;
using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;

namespace DataverseUtilities.Tests.DataversePdfEngine.Tests
{
    public class DataversePdfEngineTests : BaseFakeXrmTestClass
    {
        [Fact]
        public void Plugin_should_generate_base64_pdf_content()
        {
            //Arrange
            var pluginContext = GetDefaultContext();

            pluginContext.InputParameters.Add("htmlContent", @"<p>Krzysztof has to do:</p><ul><li>Hello World!</li><li>Hello World2!</li></ul>");

            //Act
            _context.ExecutePluginWith<DataversePdfEnginePlugin>(pluginContext);

            //Assert
            Assert.NotNull(pluginContext.OutputParameters["pdfContent"]);

            string pdfContentBase64 = (string)pluginContext.OutputParameters["pdfContent"];

            Assert.NotEmpty(pdfContentBase64);

            PdfUtils.OpenAndClosePdf(pdfContentBase64);
        }

        [Fact]
        public void Plugin_should_throw_exception_when_inputparameters_are_not_provided()
        {
            //Arrange
            var pluginContext = GetDefaultContext();
            //Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                _context.ExecutePluginWith<DataversePdfEnginePlugin>(pluginContext);
            });
        }

        [Fact]
        public void Plugin_should_generate_pdf_from_invoice_example()
        {

            //Arrange
            var pluginContext = GetDefaultContext();
            PdfSettingsGeneration pdfSettingsGeneration = new PdfSettingsGeneration()
            {
                NumbericSettings = new PageNumbericSettings()
                {
                    IsTurnedOn = true,
                    Format = "{0}/{1}",
                    Position = PageNumberingPosition.BottomCenter
                }
            };
            pluginContext.InputParameters.Add("htmlContent", File.ReadAllText("./ExampleDocument.html"));
            pluginContext.InputParameters.Add("pdfSettingsGeneration", JsonConvert.SerializeObject(pdfSettingsGeneration));

            //Act
            _context.ExecutePluginWith<DataversePdfEnginePlugin>(pluginContext);

            //Assert
            Assert.NotNull(pluginContext.OutputParameters["pdfContent"]);

            string pdfContentBase64 = (string)pluginContext.OutputParameters["pdfContent"];

            Assert.NotEmpty(pdfContentBase64);

            PdfUtils.OpenAndSaveDocument(pdfContentBase64, "./ExampleDocument.pdf");
        }
    }
}