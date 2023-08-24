using System.Xml;
using Unity;

namespace WordUp
{
    public static class WordUpContainerExtensions
    {
        // public static IUnityContainer LoadXML(this IUnityContainer container, string xmlFilePath = ".\\Config\\Unity.config")
        // {
        //     XmlDocument doc = new XmlDocument();
        //     doc.Load(xmlFilePath);
        //     var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = xmlFilePath };
        //     Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        //     UnityConfigurationSection section = (UnityConfigurationSection)configuration.GetSection("unity");
        //     return section.Configure(container, string.Empty);
        // }
    }
}