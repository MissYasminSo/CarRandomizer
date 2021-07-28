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
            
            var CarGUID = File.ReadAllLines(@"Car GUID.txt");
            
            Random rand = new Random();  
            //adding file path
            string orgXml = args[0];
            // Option1: Using SetAttributeValue()
            XDocument xmlDoc = XDocument.Load(orgXml);  
            // Update Element value  
            var items = from item in xmlDoc.Descendants("LibraryObject")  
                        select item;  

            foreach (XElement itemElement in items)  
            {  
                var currentName =itemElement.Attribute("name").Value;
                if (CarGUID.Contains(currentName.TrimStart('{').TrimEnd('}'))) {
                int index = rand.Next(CarGUID.Length);  
                itemElement.SetAttributeValue("name", "{"+CarGUID[index]+"}");  
                }
                
            }  
            
            xmlDoc.Save(args[0]);  
            Console.WriteLine(); 

        }
    }
}
