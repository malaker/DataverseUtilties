using System;
using System.Dynamic;

namespace Malaker.DataverseUtilities.DataverseTemplateEngine.Services
{
    using Malaker.DataverseUtilities.Common.Abstractions;
    using Malaker.DataverseUtilities.DataverseTemplateEngine.Abstractions;
    using Microsoft.Xrm.Sdk;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class TemplateEngineService : IService
    {
        private IPluginExecutionContext _context;
        private ITracingService _tracingService;
        private ITemplateEngine _templateEngine;

        public TemplateEngineService(ITemplateEngine templateEngine, IPluginExecutionContext context, ITracingService tracingService)
        {
            _context = context;
            _tracingService = tracingService;
            _templateEngine = templateEngine;
        }

        public void Execute()
        {
            bool templateContentExists = _context.InputParameters.Contains("templateContent");
            bool templateModelStrExists = _context.InputParameters.Contains("templateModelStr");

            if (!templateContentExists)
            {
                throw new InvalidOperationException("templateContent");
            }

            if (!templateModelStrExists)
            {
                throw new InvalidOperationException("templateModelStr");
            }

            string templateContent = (string)_context.InputParameters["templateContent"];
            string templateModelStr = (string)_context.InputParameters["templateModelStr"];

            var converter = new ExpandoObjectConverter();

            dynamic templateModel = JsonConvert.DeserializeObject<ExpandoObject>(templateModelStr, converter);

            if (templateModel != null)
            {
                _context.OutputParameters["renderedTemplate"] = _templateEngine.Parse(templateContent, templateModel);
            }
            else
            {
                throw new InvalidOperationException("Cannot deserialize templateModelStr input parameter");
            }
        }
    }
}