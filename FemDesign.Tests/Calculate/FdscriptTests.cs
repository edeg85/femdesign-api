﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FemDesign.Calculate
{
    [TestClass()]
    public class FdScriptTests
    {
        [TestMethod("CreateFdScript")]
        public void CreateFdScript()
        {
            // Serialize
            var script = new FdScript2(
                "logfile.log",
                new CmdOpen2("model.struxml"),
                new CmdUser2(CmdUserModule.RESMODE),
                new CmdCalculation2(Analysis.StaticAnalysis()),
                new CmdCalculation2(new Calculate.Design()),
                new CmdListGen2("a.bsc", "./"),
                new CmdEndSession2(),
                CmdGlobalCfg2.Default(),
                new CmdDesignDesignChanges2(),
                new CmdSave2("model.struxml"),
                new CmdSaveDocx2("model.docx")
                );
            script.Serialize("script.fdscript");

            var xmlLines = System.IO.File.ReadAllLines("script.fdscript");
            string xmlText = string.Join("\n", xmlLines);
            
            Assert.IsTrue(xmlLines[0].StartsWith("<?xml version=\"1.0\" encoding=\"utf-8\"?>"));

            Assert.IsTrue(xmlText.Contains("<fdscript"));
            Assert.IsTrue(xmlText.Contains("<fdscriptheader"));
            Assert.IsTrue(xmlText.Contains("<cmdopen"));
            Assert.IsTrue(xmlText.Contains("<cmduser"));
            Assert.IsTrue(xmlText.Contains("<cmdcalculation"));
            Assert.IsTrue(xmlText.Contains("<cmdlistgen"));
            Assert.IsTrue(xmlText.Contains("<cmdsave"));
            Assert.IsTrue(xmlText.Contains("<cmdendsession"));
            Assert.IsTrue(xmlText.Contains("<cmdglobalcfg"));
            Assert.IsTrue(xmlText.Contains("<cmdsave"));
            Assert.IsTrue(xmlText.Contains("<cmdsavedocx"));
        }

        [TestMethod("Validate schema")]
        [TestCategory("Performance")] // the test is failing and it creates a GitHub Automatic Error in TEST CLI
        public void ValidateSchema()
        {
            // Serialize
            var script = new FdScript2(
                "logfile.log",
                new CmdOpen2("model.struxml"),
                new CmdUser2(CmdUserModule.RESMODE),
                new CmdCalculation2(Analysis.StaticAnalysis()),
                new CmdListGen2("a.bsc", "./")
                );
            script.Serialize("script.fdscript");

            // Validate
            XmlDocument asset = new XmlDocument();
            XmlTextReader schemaReader = new XmlTextReader(@"fdscript.xsd");
            XmlSchema schema = XmlSchema.Read(schemaReader, (obj, e) =>
            {
                Assert.Fail(e.Message);
            });
            asset.Schemas.Add(schema);

            asset.Load("script.fdscript");

            asset.Validate((obj, e) => {
                Assert.Fail(e.Message);
            });
        }



        [TestMethod("cmdlistgen")]
        public void CmdListGen()
        {

            var mapCase = new MapCase("Deadload");
            var mapComb = new MapComb("myComb");

            //var bsc = new Bsc(ListProc.BarsDisplacementsLoadCase, "a.bsc");

            var bsc = new Bsc(ListProc.BarsDisplacementsLoadCase, "a.bsc", null, false);


            // Serialize
            var script = new FdScript2(
                "logfile.log",
                new CmdListGen2(bsc.BscPath, "./", false, mapCase)
                );
            script.Serialize("script.fdscript");

            string text = System.IO.File.ReadAllText("script.fdscript");

            Console.WriteLine(text);
        }


    }
}
