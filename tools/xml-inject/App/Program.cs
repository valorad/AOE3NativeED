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

        public void InjectXMLToBottom(InjectXMLParams settings) {

            var xmlSettings = new XmlReaderSettings();
            xmlSettings.IgnoreComments = true;


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

        string basePath = AppDomain.CurrentDomain.BaseDirectory;

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


        // Inject protoy.xml (Add new content + Edit exixting node)

        // Inject techtreey.xml (Add new content + Edit exixting node)

        public Injection()
        {
            // Inject abilities/abilities.xml (Just add new content to bottom)

            try
            {
                InjectAbilities();
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Error!: {e}");
            }


            // Inject abilities/powers.xml (Just add new content to bottom)
            
            try
            {
                InjectPowers();
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Error!: {e}");
            }

            // Inject strings/English/stringtabley.xml (Just add new content to bottom)
            // Inject strings/SimplifiedChinese/stringtabley.xml (Just add new content to bottom)
            try
            {
                InjectI18nStrings();
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Error!: {e}");
            }

        }

    }
}
