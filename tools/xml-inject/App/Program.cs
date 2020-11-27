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

            XmlReader reader = XmlReader.Create(Path.GetFullPath(Path.Combine(".", "workbench", "src", "proto-addition.xml"), basePath), xmlSettings);
            var source = new XmlDocument();
            source.Load(reader);

            reader = XmlReader.Create(Path.GetFullPath(Path.Combine(".", "workbench", "in", "protoy.xml"), basePath), xmlSettings);
            var target = new XmlDocument();
            target.Load(reader);
            

            // Offering explorers the privileges to build Hero Structures

            reader = XmlReader.Create(Path.GetFullPath(Path.Combine(".", "workbench", "src", "proto-modifs.xml"), basePath), xmlSettings);
            var protoModifsStore = new XmlDocument();
            protoModifsStore.Load(reader);

            // target.ImportNode(protoModifsStore.SelectSingleNode("/nedProtos"), false);

            XmlNode storeExplorers = protoModifsStore.SelectSingleNode("/nedProtos/explorers");

            foreach (XmlNode explorer in storeExplorers.ChildNodes) {
                string id = explorer.Attributes["id"].Value;
                XmlNode targetExplorer = target.SelectSingleNode($"/proto/unit[@id='{id}']");
                targetExplorer.OwnerDocument.ImportNode(explorer.ChildNodes[0], true);
                // targetExplorer.AppendChild(explorer.ChildNodes[0]);
            }

            // Town Center can build wagons to deploy the Hero Structures

            XmlNode storeTownCenter = protoModifsStore.SelectSingleNode("/nedProtos/townCenter");

            XmlNode targetTownCenter = target.SelectSingleNode("/proto/unit[@id='294']");

            foreach (XmlNode trainables in storeTownCenter.ChildNodes) {
                targetTownCenter.OwnerDocument.ImportNode(trainables, true);
            //   targetTownCenter.AppendChild(trainables);
            }

            // Attach the new units to the bottom
            
            XmlNode parentNode = target.SelectSingleNode("/proto");

            foreach(XmlNode node in source.SelectSingleNode("/nedProtos").ChildNodes) {
                XmlNode importedNode = parentNode.OwnerDocument.ImportNode(node, true);
            }

            // output to a file
            string outputFolder = Path.GetFullPath(Path.Combine(".", "workbench", "out"), basePath);
            Directory.CreateDirectory(outputFolder);
            string outputPath = Path.GetFullPath(Path.Combine(outputFolder, "protoy.xml"));
            File.WriteAllText(outputPath, target.OuterXml);

            reader.Dispose();
        }

        public void InjectTechtrees() {

            XmlReader reader = XmlReader.Create(Path.GetFullPath(Path.Combine(".", "workbench", "src", "techtree-addition.xml"), basePath), xmlSettings);
            var source = new XmlDocument();
            source.Load(reader);

            reader = XmlReader.Create(Path.GetFullPath(Path.Combine(".", "workbench", "in", "techtreey.xml"), basePath), xmlSettings);
            var target = new XmlDocument();
            target.Load(reader);

            // Countries enable their own heroes
            reader = XmlReader.Create(Path.GetFullPath(Path.Combine(".", "workbench", "src", "techtree-modifs.xml"), basePath), xmlSettings);
            var techtreeModifs = new XmlDocument();
            techtreeModifs.Load(reader);

            // target.ImportNode(techtreeModifs, false);

            XmlNode storeCountries = techtreeModifs.SelectSingleNode("/nedTechtrees/countries");

            foreach (XmlNode country in storeCountries.ChildNodes) {
                string factionID = country.Attributes["faction"].Value;
                string countryName = country.Attributes["name"].Value;
                XmlNode faction = techtreeModifs.SelectSingleNode($"/nedTechtrees/factions/faction[@id='{factionID}']");
                
                XmlNode targetCountry = target.SelectSingleNode($"/techtree/tech[@name='{countryName}']");
                XmlNode effects = targetCountry.SelectSingleNode("effects");
                
                foreach (XmlNode effect in faction.ChildNodes) {
                    effects.OwnerDocument.ImportNode(effect, true);
                    // effects.AppendChild(effect);
                }

            }

            // Attach the new techs to the bottom

            XmlNode parentNode = target.SelectSingleNode("/techtree");

            foreach(XmlNode node in source.SelectSingleNode("/nedTechtrees").ChildNodes) {
                XmlNode importedNode = parentNode.OwnerDocument.ImportNode(node, true);
            }

            // output to a file
            string outputFolder = Path.GetFullPath(Path.Combine(".", "workbench", "out"), basePath);
            Directory.CreateDirectory(outputFolder);
            string outputPath = Path.GetFullPath(Path.Combine(outputFolder, "techtreey.xml"));
            File.WriteAllText(outputPath, target.OuterXml);

            reader.Dispose();

        }

        public Injection()
        {

            try
            {
                InjectAbilities();
                InjectPowers();
                InjectI18nStrings();
                InjectProtos();
                InjectTechtrees();
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Error!: {e}");
            }
            
        }

    }
}
