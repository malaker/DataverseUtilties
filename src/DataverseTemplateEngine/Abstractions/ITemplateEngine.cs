namespace Malaker.DataverseUtilities.DataverseTemplateEngine.Abstractions
{
    public interface ITemplateEngine
    {
        string Parse(string templateContent, dynamic model);
    }
}