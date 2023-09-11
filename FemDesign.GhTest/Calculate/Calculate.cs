using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace FemDesignGhTests
{
    [TestClass]
    public partial class AutoTest_Calculate
    {
        public string FilePath => GetFile("Calculate/Calculate.ghx");
        private TestContext testContextInstance;
        public TestContext TestContext { get => testContextInstance; set => testContextInstance = value; }
        public static string GetFile(string filename)
        {
            string baseDirectory = Directory.GetCurrentDirectory();
            return Path.Combine(baseDirectory, filename);
        }
        [TestMethod]
        public void ModelConstruct()
        {
            Tenrec.Runner.Initialize(TestContext);
            Tenrec.Runner.RunTenrecGroup(FilePath, new System.Guid("38a59629-b6f8-4b46-992e-91858aacfdd0"), TestContext);
        }
        [TestMethod]
        public void LoadCaseAnalysisDefine()
        {
            Tenrec.Runner.Initialize(TestContext);
            Tenrec.Runner.RunTenrecGroup(FilePath, new System.Guid("05870304-d3f2-4557-b011-d6da4056ec5a"), TestContext);
        }
        [TestMethod]
        public void LoadCombAnalysisDefine()
        {
            Tenrec.Runner.Initialize(TestContext);
            Tenrec.Runner.RunTenrecGroup(FilePath, new System.Guid("4393264e-aea1-4f2a-bcb5-70df7f879608"), TestContext);
        }
        [TestMethod]
        public void StabilityAnalysisDefine()
        {
            Tenrec.Runner.Initialize(TestContext);
            Tenrec.Runner.RunTenrecGroup(FilePath, new System.Guid("aa6de7e7-c132-45f3-9802-210cf24fc958"), TestContext);
        }
        [TestMethod]
        public void EigenFrequencyAnalysisDefine()
        {
            Tenrec.Runner.Initialize(TestContext);
            Tenrec.Runner.RunTenrecGroup(FilePath, new System.Guid("51451e78-6e2b-42d8-9e69-fe9070d652d3"), TestContext);
        }
    }

}
