using System;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace CarRandomizer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please put path to XML file as the First Argument");
                return;
            }

            var carGuid = File.ReadAllLines(@"Car GUID.txt");
            string orgXml = args[0];
            Random rand = new Random();

            XDocument xmlDoc = XDocument.Load(orgXml);

            foreach (XElement libraryObject in xmlDoc.Descendants("LibraryObject"))
            {
                var currentName = libraryObject.Attribute("name").Value;
                if (carGuid.Contains(currentName.TrimStart('{').TrimEnd('}')))
                {
                    int index = rand.Next(carGuid.Length);
                    libraryObject.SetAttributeValue("name", "{" + carGuid[index] + "}");
                }
            }

            xmlDoc.Save(args[0]);

        }
    }
}
