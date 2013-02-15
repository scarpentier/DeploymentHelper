using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using LinqToExcel;

namespace DeploymentHelper.Core
{
    public class DeploymentHelper
    {
        private static readonly Regex RegexObj = new Regex(@"\{([^{}]*)\}");

        public ExcelQueryFactory Excel { get; private set; }

        public Dictionary<string, string> Dictionary { get; private set; }

        public DeploymentHelper(string excelFilePath, string environment)
        {
            Dictionary = new Dictionary<string, string>();
            Excel = new ExcelQueryFactory(excelFilePath);
            this.FillDictionnary(environment);
        }

        private void FillDictionnary(string environment)
        {
            // Get first sheet name, first column name
            var sheetName = Excel.GetWorksheetNames().First();
            var keyColumnName = Excel.GetColumnNames(sheetName).First();

            // Make sure the environment exists
            if (!Excel.GetColumnNames(sheetName).Contains(environment))
            {
                Console.WriteLine("[ERROR] Environment \"{0}\" could not be found in the Excel file");
                return;
            }

            // Get the data
            var data =
                this.Excel.Worksheet(sheetName)
                    .Where(a => a[keyColumnName] != string.Empty)
                    .Select(a => new { Key = a[keyColumnName], Value = a[environment] });

            // Put the keys in a dictionnary
            foreach (var k in data)
            {
                Dictionary.Add(k.Key, k.Value);
            }
        }

        public void ParseFile(string inFilePath, string outFilePath)
        {
            // Open file
            var fileContent = File.ReadAllText(inFilePath);

            // Replace the content
            fileContent = this.Dictionary.Aggregate(fileContent, (current, kvpair) => current.Replace("{" + kvpair.Key + "}", kvpair.Value));

            // Make sure no vars are left
            Match matchResult = RegexObj.Match(fileContent);
            while (matchResult.Success)
            {
                fileContent = fileContent.Replace(matchResult.Value, string.Empty);
                Console.WriteLine("[WARNING] Key \"{0}\" defined in input file not found in Excel file. Replaced by empty string", matchResult.Groups[1].Value);
                matchResult = matchResult.NextMatch();
            }

            // Save output file
            File.WriteAllText(outFilePath, fileContent);
        }
    }
}
