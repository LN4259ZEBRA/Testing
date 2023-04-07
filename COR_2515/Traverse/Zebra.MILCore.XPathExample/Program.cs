using System.Xml;
using System.Linq;

namespace Zebra.MILCore.XPathExample
{
   internal class Program
   {
      static void Main(string[] args) => new Program().MainInstance(args);

      void MainInstance(string[] args)
      {
         var xmlContent = File.ReadAllText("Installer.xml");

         var files = GetFilesFromFeatureName(xmlContent, "DriversFiles");

         foreach (var file in files)
         {
            Console.WriteLine(file);
         }
      }

      private IEnumerable<string> GetFilesFromComponentName(string xmlContent, string componentName)
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.LoadXml(xmlContent); // can also use Load(filePath);

         var nodes = xmlDoc.SelectNodes($@"//Component[@Name='{componentName}']/File/@Source");
         //   //Component                    finds Component node anywhere in the structure
         //   //[@Name='{componentName}']    With attribute name equals to componentName 
         //   /File/                         Selects "File" nodes
         //   @Source                        Select attribute "Source"

         if (nodes != null)
         {
            return nodes.Cast<XmlNode>().Select(x => x.Value);
            // if we find nodes, we create a new collection with only their value
         }

         return Array.Empty<string>();
      }

      private IEnumerable<string> GetFilesFromFeatureName(string xmlContent, string featureName)
      {
         var xmlDoc = new XmlDocument();
         xmlDoc.LoadXml(xmlContent); // can also use Load(filePath);

         var nodes = xmlDoc.SelectNodes($@"//Feature[@Name='{featureName}']//File/@Source");
         //   //Feature                      finds Component node anywhere in the structure
         //   //[@Name='{featureName}']      With attribute name equals to featureName 
         //   //File/                        Find file nodes in all descendants
         //   @Source                        Select attribute "Source"

         if (nodes != null)
         {
            return nodes.Cast<XmlNode>().Select(x => x.Value);
            // if we find nodes, we create a new collection with only their value
         }

         return Array.Empty<string>();
      }
   }
}