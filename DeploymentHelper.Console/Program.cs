using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploymentHelper.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("DeploymentHelper. Replaces strings in your app configuration file(s) with data read from an Excel file.");
                System.Console.WriteLine("Usage: DeploymentHelper.exe ExcelFile Environment InputFile OutputFile");
                System.Console.WriteLine();
                return;
            }

            // Arg 1 is the excel file
            var excelFile = args[0];

            // Arg 2 is the environment
            var environment = args[1];

            // Arg 3 is the input file
            var inputFile = args[2];

            // Arg 4 is the output file
            var outputFile = args[3];

            try
            {
                var deploy = new Core.DeploymentHelper(excelFile, environment);
                deploy.ParseFile(inputFile, outputFile);
            }
            catch (InvalidOperationException ex)
            {
                if (!ex.Message.Contains("Microsoft.ACE.OLEDB")) throw;

                System.Console.WriteLine("The Microsoft Database Engine is missing from this machine. You can download it from the Microsoft Download Center: http://www.microsoft.com/download/details.aspx?id=13255");
            }
        }
    }
}
