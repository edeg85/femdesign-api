using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace FemDesignGhTests
{
    [TestClass]
    public partial class AutoTest_Model
    {
        public string FilePath => GetFile("Model/Model.ghx");
        private TestContext testContextInstance;
        public TestContext TestContext { get => testContextInstance; set => testContextInstance = value; }
        public static string GetFile(string filename)
        {
            string baseDirectory = Directory.GetCurrentDirectory();
            return Path.Combine(baseDirectory, filename);
        }
        [TestMethod]
        public void ModelSerialize()
        {
            Tenrec.Runner.Initialize(TestContext);
            Tenrec.Runner.RunTenrecGroup(FilePath, new System.Guid("e3f9784c-3569-4b34-a36d-87776200314b"), TestContext);
        }
        [TestMethod]
        public void ReadModelFromFile()
        {
            Tenrec.Runner.Initialize(TestContext);
            Tenrec.Runner.RunTenrecGroup(FilePath, new System.Guid("01ec37cf-f864-4b23-89f0-3a530d7e5c70"), TestContext);
        }
    }

}
