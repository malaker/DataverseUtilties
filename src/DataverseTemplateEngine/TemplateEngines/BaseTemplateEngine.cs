using DotLiquid;
using Malaker.DataverseUtilities.DataverseTemplateEngine.Abstractions;
using System;

namespace Malaker.DataverseUtilities.DataverseTemplateEngine.Engines
{
    public class BaseTemplateEngine : ITemplateEngine
    {
        public string Parse(string templateContent, dynamic dataModel)
        {
            if (string.IsNullOrEmpty(templateContent))
            {
                throw new ArgumentNullException(nameof(templateContent));
            }
            if (dataModel == null)
            {
                throw new ArgumentNullException(nameof(dataModel));
            }
            var liquidTemplate = Template.Parse(templateContent);

            string renderedTemplate = liquidTemplate.Render(Hash.FromDictionary(dataModel));

            return renderedTemplate;
        }
    }
}