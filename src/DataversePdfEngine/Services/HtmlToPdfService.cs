using Malaker.DataverseUtilities.Common.Abstractions;
using Malaker.DataverseUtilities.DataversePdfEngine.Abstractions;
using Malaker.DataverseUtilities.DataversePdfEngine.Engines;
using Microsoft.Xrm.Sdk;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Malaker.DataverseUtilities.DataversePdfReport
{
    public class HtmlToPdfService : IService
    {
        private IPluginExecutionContext _context;
        private IPdfEngine _pdfEngine;
        private ITracingService _tracingService;

        public HtmlToPdfService(IPdfEngine pdfEngine, IPluginExecutionContext context, ITracingService tracingService)
        {
            _context = context;
            _pdfEngine = pdfEngine;
            _tracingService = tracingService;
        }

        public void Execute()
        {
            bool htmlContentExists = _context.InputParameters.Contains("htmlContent");
            bool pdfSettingsGenerationExists = _context.InputParameters.Contains("pdfSettingsGeneration");

            if (!htmlContentExists)
            {
                throw new InvalidOperationException("htmlContent");
            }

            PdfSettingsGeneration settings = PdfSettingsGeneration.Default;

            if (pdfSettingsGenerationExists)
            {
                settings = JsonConvert.DeserializeObject<PdfSettingsGeneration>((string)_context.InputParameters["pdfSettingsGeneration"]);
            }

            string htmlContent = (string)_context.InputParameters["htmlContent"];

            string base64String = _pdfEngine.ConvertHtmlToPdf(htmlContent, settings);

            _context.OutputParameters.Add("pdfContent", base64String);
        }

    }
}
