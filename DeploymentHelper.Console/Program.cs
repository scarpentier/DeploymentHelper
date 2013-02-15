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
            if (args.Length != 3 && args.Length != 4)
            {
                System.Console.WriteLine("DeploymentHelper. "); // TODO: Add a description
                System.Console.WriteLine("Usage: DeploymentHelper.exe ExcelFile Environment InputFile OutputFile");
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
            
            var deploy = new Core.DeploymentHelper(excelFile, environment);
            deploy.ParseFile(inputFile, outputFile);
        }
    }
}
