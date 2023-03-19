using antDCVRP.Algorithm;
using antDCVRP.Extensions;
using antDCVRP.Output;
using antDCVRP.RandomUtils;
using antDCVRP.Reader;
using System;
using System.Collections.Generic;
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

            RandomManager.SetupSeed(1);

            var reader = ReaderResolver.ResolveStandardReader();

            reader.LoadConfiguration();
            reader.LoadOuputConfiguration();

            reader.ReadMap();

            var simulation = new SimulationExt(reader.GetSimulation(), reader.Configuration);

            var logger = new Logger(reader.IOConfiguration);

            var algorithm = new AntAlgorithm(simulation, logger);

            var bestSolution = algorithm.ConductAlgorithm();

            logger.LogBestSolution(bestSolution);

            if (reader.IOConfiguration.OutputFile.Length == 0)
            {
                Console.Read();
            }

        }
    }
}
