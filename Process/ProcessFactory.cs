using antDCVRP.Algorithm;
using antDCVRP.Extensions;
using antDCVRP.Greedy;
using antDCVRP.Output;
using antDCVRP.RandomUtils;
using antDCVRP.Reader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Process
{
    public static class ProcessFactory
    {
        public static void ConductSimulation()
        {



            var reader = ReaderResolver.ResolveStandardReader();

            reader.LoadConfiguration();
            reader.LoadOuputConfiguration();

            reader.ReadMap();

            var simulation = new SimulationExt(reader.GetSimulation(), reader.Configuration);

            RandomManager.SetupSeed(simulation.Configuration.RandomSeed);

            var logger = new Logger(reader.IOConfiguration);

            IAlgorithm algorithm = GetAlgorithm(simulation, logger);

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var bestSolution = algorithm.ConductAlgorithm();
            stopwatch.Stop();

            logger.LogBestSolution(bestSolution, stopwatch.Elapsed);
            

            if (reader.IOConfiguration.OutputFile.Length == 0)
            {
                Console.Read();
            }

        }

        private static IAlgorithm GetAlgorithm(SimulationExt simulation, Logger logger)
        {
            if (simulation.Configuration.GreedyAlgorithm)
            {
                return new GRAlgorithm(simulation, logger);
            }
            else
            {
                return new AntAlgorithm(simulation, logger);
            }
        }
    }
}
