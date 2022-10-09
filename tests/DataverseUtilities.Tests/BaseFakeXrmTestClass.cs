using FakeXrmEasy;
using Microsoft.Xrm.Sdk;

namespace Malaker.DataverseUtilities.Tests
{
    public class BaseFakeXrmTestClass
    {
        protected XrmFakedContext _context = new XrmFakedContext();

        public XrmFakedPluginExecutionContext GetDefaultContext()
        {
            var pluginContext = _context.GetDefaultPluginContext();

            ParameterCollection inputParameters = new ParameterCollection();
            ParameterCollection outputParameters = new ParameterCollection();

            pluginContext.InputParameters = inputParameters;
            pluginContext.OutputParameters = outputParameters;

            return pluginContext;
        }
    }
}