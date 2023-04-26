using System.Xml;
using System.Linq;

using System.Windows;
namespace Zebra.MILCore.XPathExample
{
   internal class Program
   {
      public XmlDocument? InstallerXML = null;

      static void Main(string[] args) => new Program().MainInstance(args);

      /// <summary>
      /// Entry point of Installer
      /// </summary>
      /// <param name="argument">
      ///    generatemedia
      ///    install
      ///    uninstall
      /// </param>
      void MainInstance(string[] args)
         {
         InstallerXML = new XmlDocument();
         if (args.Length > 0)
            {
            if (File.Exists(args[0]))
               {
               InstallerXML.LoadXml(args[0]);
               }
            else 
               {
               Console.WriteLine(args[0] + " does not exist");
               }
            }

         if (InstallerXML != null)
            {
            if (args[1].compare)
            }
         else
            {
            Console.WriteLine("Invalid argument passed to program");
            }
         var xmlContent = File.ReadAllText("Installer.xml");

         var files = GetFilesFromFeatureName(xmlContent, "DriversFiles");

         foreach (var file in files)
            {

            }
         }

      public bool GenerateMedia(string xmlPath)
         {
         bool noError = true;


         return noError;
         }

      public bool ValidateXML(string xmlPath)
         { 
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