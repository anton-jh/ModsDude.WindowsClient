using MoonSharp.Interpreter;
using System.Xml;

namespace ModsDude.WindowsClient.Application.Adapters.FileSystem.Xml;

[MoonSharpUserData]
public static class XmlFacade
{
    public static XmlDocument? Read(Stream stream)
    {
        var document = new XmlDocument();
        document.Load(stream);

        return document;
    }
}
