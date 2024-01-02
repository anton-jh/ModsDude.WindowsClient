using System.Xml;

namespace ModsDude.WindowsClient.Application.Adapters.FileSystem.Xml;
public class XmlDocumentProxy(XmlDocument document)
{
    public string? Get(string xpath)
    {
        var node = document.SelectSingleNode(xpath);

        return node?.NodeType switch
        {
            XmlNodeType.Attribute => node.Value,
            XmlNodeType.Element => node.InnerText,
            _ => null
        };
    }
}
