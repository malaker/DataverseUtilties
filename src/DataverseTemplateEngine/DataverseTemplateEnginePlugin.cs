using Malaker.DataverseUtilities.Common.Abstractions;
using Malaker.DataverseUtilities.DataverseTemplateEngine.Engines;
using Malaker.DataverseUtilities.DataverseTemplateEngine.Services;
using System;

namespace Malaker.DataverseUtilities.DataverseTemplateEngine
{
    public class DataverseTemplateEnginePlugin : PluginBase
    {
        public DataverseTemplateEnginePlugin() : base(typeof(DataverseTemplateEnginePlugin))
        {
        }

        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;

            var templateEngineService = new TemplateEngineService(new BaseTemplateEngine(), context, localPluginContext.TracingService);

            templateEngineService.Execute();
        }
    }
}