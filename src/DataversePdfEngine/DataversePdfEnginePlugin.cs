using Malaker.DataverseUtilities.Common.Abstractions;
using Malaker.DataverseUtilities.DataversePdfEngine.Engines;
using Malaker.DataverseUtilities.DataversePdfReport;
using System;

namespace Malaker.DataverseUtilities.DataversePdfEngine
{
    public class DataversePdfEnginePlugin : PluginBase
    {
        public DataversePdfEnginePlugin() : base(typeof(DataversePdfEnginePlugin))
        {
        }

        protected override void ExecuteDataversePlugin(ILocalPluginContext localPluginContext)
        {
            if (localPluginContext == null)
            {
                throw new ArgumentNullException(nameof(localPluginContext));
            }

            var context = localPluginContext.PluginExecutionContext;

            var reportEngineService = new HtmlToPdfService(new BasePdfEngine(), context, localPluginContext.TracingService);

            reportEngineService.Execute();
        }
    }
}