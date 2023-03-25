using antDCVRP.Algorithm;
using antDCVRP.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Output
{
    public class Logger
    {
        private const string firstLineOutputTemplate = "Best found solution has weight {0}.";
        private const string customerOutputTemplate = "{0}";
        private const string customerSeparatorTemplate = " -> ";
        private const string noSolutionTemplate = "Haven't found a solution. :(";

        private StreamWriter outputStream { get; set; }
        private IOConfiguration IOConfiguration { get; set; }

        public Logger(IOConfiguration configuration) {
            this.LoadConfiguration(configuration);
        }

        public void LoadConfiguration(IOConfiguration configuration)
        {
            this.IOConfiguration = configuration;
            if (this.IOConfiguration.OutputFile.Length == 0)
            {
                var sw = new StreamWriter(Console.OpenStandardOutput());
                sw.AutoFlush = true;
                Console.SetOut(sw);
                this.outputStream = sw;
            }
            else
            {
                Stream st = File.Open(this.IOConfiguration.OutputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                var sw = new StreamWriter(st);
                sw.AutoFlush = true;
                this.outputStream = sw;
            }
        }

        public void LogIterativeSolution(ProductSolution solution)
        {
            if (this.IOConfiguration.LogEachImprovement)
            {
                this.LogSolution(solution);
            }
        }

        public void LogSolution(ProductSolution solution)
        {
            var firstLineOutput = string.Format(firstLineOutputTemplate, solution.Sum);
            var secondLineOutput = "";
            
            for (int i = 0; i < solution.Customers.Count - 1; i++)
            {
                secondLineOutput += string.Format(customerOutputTemplate, solution.Customers[i].Id);
                secondLineOutput += customerSeparatorTemplate;
            }
            secondLineOutput += string.Format(customerOutputTemplate, solution.Customers[solution.Customers.Count - 1].Id);

            var concatenatedOutput = string.Format("{0}\n{1}\n", firstLineOutput, secondLineOutput);

            outputStream.Write(concatenatedOutput);
        }

        public void LogBestSolution(ProductSolution solution, TimeSpan elapsedTime)
        {
            outputStream.Write("--------------------------\n");
            outputStream.Write("Elapsed Time is {0:00}:{1:00}:{2:00}.{3}\n",
                 elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds);
            LogSolution(solution);
        }

        public void LogNoSolution()
        {
            outputStream.Write(noSolutionTemplate);
        }
    }
}
