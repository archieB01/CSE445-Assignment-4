using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        // URLs for the XML files and XSD schema hosted on ASU personal website
        public static string xmlURL = "https://www.public.asu.edu/~abir2/Hotels.xml";
        public static string xmlErrorURL = "https://www.public.asu.edu/~abir2/HotelsErrors.xml";
        public static string xsdURL = "https://www.public.asu.edu/~abir2/Hotels.xsd";

        public static void Main(string[] args)
        {
            // Test 1: Verify the XML file (Hotels.xml) without errors
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);

            // Test 2: Verify the XML file (HotelsErrors.xml) with intentional errors
            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);

            // Test 3: Convert the XML (Hotels.xml) to JSON format
            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1

        // Method to verify XML against the schema (XSD)
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            try
            {
                // Load the XSD schema
                XmlSchemaSet schema = new XmlSchemaSet();
                schema.Add("", xsdUrl);

                // Load the XML file with the schema for validation
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas = schema;
                settings.ValidationType = ValidationType.Schema;

                // Capture validation errors
                string errorMsg = "";
                settings.ValidationEventHandler += (sender, args) =>
                {
                    errorMsg += $"Validation Error: {args.Message}\n";
                };

                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                    // Parse XML document
                    while (reader.Read()) { }
                }

                //return "No Error" if XML is valid. Otherwise, return the desired exception message.

                // If errorMsg is empty, XML is valid
                return string.IsNullOrEmpty(errorMsg) ? "No Error" : errorMsg;
            }

            catch (Exception ex)
            {
                // Catch and return any unexpected errors
                return $"Exception: {ex.Message}";
            }
        }

        // Method to convert XML to JSON
        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                // Load XML from the URL
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlUrl);

                // Convert XML to JSON using Newtonsoft.Json
                string jsonText = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);

                // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))

                // Return JSON string
                Console.WriteLine(jsonText); // to check only
                return jsonText;
            }
            catch (Exception ex)
            {
                // Handle any errors encountered during XML to JSON conversion
                return $"Exception: {ex.Message}";
            }
        }
    }
}
