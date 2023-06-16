using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using FemDesign;

namespace FemDesign.Tests.Model
{
    [TestClass]
    public class Refactor
    {
        [TestMethod]
        public void TestMethod1()
        {
            string inputFile = "Model/refactor_test.struxml";
            var model = FemDesign.Model.DeserializeFromFilePath(inputFile);

            var a = 2;
            var b = 3;
            using(var connection = new FemDesignConnection(keepOpen: true))
            {
                connection.Open(model);
            }
        }
    }
}
