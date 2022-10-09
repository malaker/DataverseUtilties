using Malaker.DataverseUtilities.DataverseTemplateEngine;
using Malaker.DataverseUtilities.Tests.TestUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Malaker.DataverseUtilities.Tests.DataverseTemplateEngine.Tests
{
    public class DataverseTemplateEngineTests : BaseFakeXrmTestClass
    {
        [Fact]
        public void Plugin_should_parse_template()
        {
            //Arrange
            var pluginContext = GetDefaultContext();

            pluginContext.InputParameters.Add("templateContent", @"<p>{{ user.name }} has to do:</p><ul>{% for item in user.tasks -%}<li>{{ item.name }}</li>{% endfor -%}</ul>");

            TestTemplateModel templateModel = new TestTemplateModel()
            {
                User = new TestTemplateModel.TestTemplateModelUser()
                {
                    Name = "SystemUser",
                    Tasks = new List<TestTemplateModel.TestTemplateModelUser.TestTemplateModelTask>()
                    {
                        new TestTemplateModel.TestTemplateModelUser.TestTemplateModelTask()
                        {
                            Name = "Hello world task 1"
                        },
        new TestTemplateModel.TestTemplateModelUser.TestTemplateModelTask()
                        {
                            Name = "Hello world task 2"
                        }
}
                }
            };

            pluginContext.InputParameters.Add("templateModelStr", JsonConvert.SerializeObject(templateModel));

            //Act
            _context.ExecutePluginWith<DataverseTemplateEnginePlugin>(pluginContext);

            //Assert
            Assert.NotNull(pluginContext.OutputParameters["renderedTemplate"]);
            Assert.NotEmpty((string)pluginContext.OutputParameters["renderedTemplate"]);
        }

        [Fact]
        public void Plugin_should_throw_exception_when_inputparameters_are_not_provided()
        {
            //Arrange
            var pluginContext = GetDefaultContext();
            //Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                _context.ExecutePluginWith<DataverseTemplateEnginePlugin>(pluginContext);
            });
        }


        [Fact]
        public void Plugin_should_generate_template_from_example()
        {

            //Arrange
            var pluginContext = GetDefaultContext();

            var model = new
            {
                Invoice = new
                {
                    Id = "#123456",
                    Date = DateTime.Now.ToString("d"),
                    Place = "New York",
                    Account = new
                    {
                        Id = "CX123456",
                        Name = "John Smith",
                        Address = new
                        {
                            Line1 = "Wall Street 1/5",
                            PostalCode = "11-111",
                            City = "New York",
                            CountryCode = "US"

                        }
                    },
                    Items = new List<dynamic>()
                {
                    new {Id=1,Name="Chair",UnitPrice="50$", Quantity=1,Price="50$"},
                    new {Id=2,Name="Desk",UnitPrice="80$", Quantity=1,Price="80$"},
                    new {Id=3,Name="Lamp",UnitPrice="40$", Quantity=1,Price="40$"},
                    new {Id=4,Name="Shipment cost", UnitPrice="10$", Quantity=1,Price="10$"}
                },
                    TotalSum = "180$"
                }
            };




            pluginContext.InputParameters.Add("templateContent", File.ReadAllText("./ExampleLiquidTemplate.html"));
            pluginContext.InputParameters.Add("templateModelStr", JsonConvert.SerializeObject(model));

            //Act
            _context.ExecutePluginWith<DataverseTemplateEnginePlugin>(pluginContext);

            //Assert
            Assert.NotNull(pluginContext.OutputParameters["renderedTemplate"]);

            string renderedTemplate = (string)pluginContext.OutputParameters["renderedTemplate"];

            Assert.NotEmpty(renderedTemplate);

            FileUtils.SaveContent(renderedTemplate, "./RenderedLiquidTemplate.html");


        }


    }
}