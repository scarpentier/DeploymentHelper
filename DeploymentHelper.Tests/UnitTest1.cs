using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeploymentHelper.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private const string InputFile = "Input.xml";

        private const string OutputFile = "Output.xml";

        private const string ExcelFile = "Deployment.xlsx";

        [TestMethod]
        [DeploymentItem(InputFile)]
        [DeploymentItem(ExcelFile)]
        public void TestMethod1()
        {
            // Init
            var deploy = new Core.DeploymentHelper(ExcelFile, "Dev");

            // Test actual operation
            deploy.ParseFile(InputFile, OutputFile);

            // Test output file
            var fileContent = File.ReadAllText(OutputFile);

            Assert.IsFalse(fileContent.Contains("{{Pi.Server}}"));
            Assert.IsTrue(fileContent.Contains("Data Source=pidev;"));
            Assert.IsTrue(fileContent.Contains("value=\"http://\""));
        }
    }
}
