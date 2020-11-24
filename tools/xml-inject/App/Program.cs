using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var i = new Injection();

        }


    }

    class InjectXMLParams {
        public string SourceFilePath { get; set; }
        public string SourceParentTagName { get; set; }
        public string TargetFilePath { get; set; }
        public string TargetParentTagName { get; set; }
        public string OutputFolder { get; set; }
        public string OutputFileName { get; set; }

    }


    class Injection {

        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        XmlReaderSettings xmlSettings = new XmlReaderSettings() {
            IgnoreComments = true,
        };

        public void InjectXMLToBottom(InjectXMLParams settings) {

            XmlReader sourceReader = XmlReader.Create(settings.SourceFilePath, xmlSettings);
            var source = new XmlDocument();
            source.Load(sourceReader);

            XmlReader targetReader = XmlReader.Create(settings.TargetFilePath, xmlSettings);
            var target = new XmlDocument();
            target.Load(targetReader);

            XmlNode parentNode = target.SelectSingleNode(settings.TargetParentTagName);

            foreach(var node in source.SelectSingleNode(settings.SourceParentTagName).ChildNodes) {
                XmlNode importedNode = parentNode.OwnerDocument.ImportNode(node as XmlNode, true);
            }
            
            Directory.CreateDirectory(settings.OutputFolder);
            string outputPath = Path.GetFullPath(Path.Combine(settings.OutputFolder, settings.OutputFileName));
            File.WriteAllText(outputPath, target.OuterXml);

            sourceReader.Dispose();
            targetReader.Dispose();

        }

        public void InjectAbilities() {

            InjectXMLToBottom(new InjectXMLParams() {
                SourceFilePath = Path.GetFullPath(Path.Combine(".", "workbench", "src", "abilities", "abilities.xml"), basePath),
                SourceParentTagName = "NEDAbilities",
                TargetFilePath = Path.GetFullPath(Path.Combine(".", "workbench", "in", "abilities", "abilities.xml"), basePath),
                TargetParentTagName = "abilities",
                OutputFolder = Path.GetFullPath(Path.Combine(".", "workbench", "out", "abilities"), basePath),
                OutputFileName = "abilities.xml"
            });

        }

        public void InjectPowers() {

            InjectXMLToBottom(new InjectXMLParams() {
                SourceFilePath = Path.GetFullPath(Path.Combine(".", "workbench", "src", "abilities", "powers.xml"), basePath),
                SourceParentTagName = "NEDPowers",
                TargetFilePath = Path.GetFullPath(Path.Combine(".", "workbench", "in", "abilities", "powers.xml"), basePath),
                TargetParentTagName = "powers",
                OutputFolder = Path.GetFullPath(Path.Combine(".", "workbench", "out", "abilities"), basePath),
                OutputFileName = "powers.xml"
            });

        }

        public void InjectI18nStrings() {

            var i18nTable = new Dictionary<string, List<string>>() {
                // format: 
                // (lang), List [
                //     (srcFilePath),
                //     (targetFilePath),
                //     (outputFilePath)
                // ]
                {
                    "en", new List<string>() {
                        Path.GetFullPath(Path.Combine(".", "workbench", "src", "stringTable", "stringtabley-en.xml"), basePath),
                        Path.GetFullPath(Path.Combine(".", "workbench", "in", "strings", "English", "stringtabley.xml"), basePath),
                        Path.GetFullPath(Path.Combine(".", "workbench", "out", "strings", "English"), basePath),
                    }
                },
                {
                    "cn", new List<string>() {
                        Path.GetFullPath(Path.Combine(".", "workbench", "src", "stringTable", "stringtabley-cn.xml"), basePath),
                        Path.GetFullPath(Path.Combine(".", "workbench", "in", "strings", "SimplifiedChinese", "stringtabley.xml"), basePath),
                        Path.GetFullPath(Path.Combine(".", "workbench", "out", "strings", "SimplifiedChinese"), basePath),
                    }
                }
            };


            foreach(var lang in i18nTable) {

                InjectXMLToBottom(new InjectXMLParams() {
                    SourceFilePath = lang.Value[0],
                    SourceParentTagName = "nedStrings",
                    TargetFilePath = lang.Value[1],
                    TargetParentTagName = "/stringtable/language",
                    OutputFolder = lang.Value[2],
                    OutputFileName = "stringtabley.xml"
                });

            }

        }

        public void InjectProtos() {

            XmlReader reader = XmlReader.Create(Path.GetFullPath(Path.Combine(".", "workbench", "src", "protoy.xml"), basePath), xmlSettings);
            var source = new XmlDocument();
            source.Load(reader);

            reader = XmlReader.Create(Path.GetFullPath(Path.Combine(".", "workbench", "in", "protoy.xml"), basePath), xmlSettings);
            var target = new XmlDocument();
            target.Load(reader);
            

            // Offering explorers the privileges to build Hero Structures

            var explorerStore = new XmlDocument(); // TODO: Write this map

            // Town Center can build wagons to deploy the Hero Structures

            XmlNode townCenter = target.SelectSingleNode("/proto/unit@id='294'");

            // Attach the new units to the bottom




            string outputFolder = Path.GetFullPath(Path.Combine(".", "workbench", "out"), basePath);
            Directory.CreateDirectory(outputFolder);
            string outputPath = Path.GetFullPath(Path.Combine(outputFolder, "protoy.xml"));
            File.WriteAllText(outputPath, target.OuterXml);

            reader.Dispose();
        }

        public void InjectTechtrees() {

            XmlReader reader = XmlReader.Create(Path.GetFullPath(Path.Combine(".", "workbench", "src", "techtreey.xml"), basePath), xmlSettings);
            var source = new XmlDocument();
            source.Load(reader);

            reader = XmlReader.Create(Path.GetFullPath(Path.Combine(".", "workbench", "in", "techtreey.xml"), basePath), xmlSettings);
            var target = new XmlDocument();
            target.Load(reader);

            // Countries enable their own heroes
            var civStore = new XmlDocument(); // TODO: Write this map

            // Attach the new techs to the bottom




            string outputFolder = Path.GetFullPath(Path.Combine(".", "workbench", "out"), basePath);
            Directory.CreateDirectory(outputFolder);
            string outputPath = Path.GetFullPath(Path.Combine(outputFolder, "techtreey.xml"));
            File.WriteAllText(outputPath, target.OuterXml);

            reader.Dispose();

        }

        // Inject protoy.xml (Add new content + Edit exixting node)

        // Inject techtreey.xml (Add new content + Edit exixting node)

        public Injection()
        {

            try
            {
                // InjectAbilities();
                // InjectPowers();
                // InjectI18nStrings();
                InjectProtos();
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Error!: {e}");
            }
            
        }

    }
}
