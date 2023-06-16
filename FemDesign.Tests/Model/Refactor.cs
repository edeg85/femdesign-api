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
            string inputFile = "Model/refactor_test2.struxml";
            var model = FemDesign.Model.DeserializeFromFilePath(inputFile);

            using(var connection = new FemDesignConnection(keepOpen: true))
            {
                connection.Open(model);
                connection.Save(System.IO.Path.GetFullPath("Model/refactor_test2_after.struxml"));
            }
        }
    }
}
