using Newtonsoft.Json;
using System.Collections.Generic;

namespace Malaker.DataverseUtilities.Tests.DataverseTemplateEngine.Tests
{
    public class TestTemplateModel
    {
        [JsonProperty("user")]
        public TestTemplateModelUser User { get; set; }

        public class TestTemplateModelUser
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("tasks")]
            public List<TestTemplateModelTask> Tasks { get; set; }

            public class TestTemplateModelTask
            {
                [JsonProperty("name")]
                public string Name { get; set; }
            }
        }
    }
}