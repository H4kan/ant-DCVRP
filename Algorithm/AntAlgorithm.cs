using antDCVRP.Extensions;
using antDCVRP.Output;
using antDCVRP.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Algorithm
{
    public class AntAlgorithm : IAlgorithm
    {
        private SimulationExt simulation { get; set; }

        private Logger logger { get; set; }

        public AntAlgorithm(SimulationExt simulation, Logger logger) 
        {
            this.simulation = simulation;
            this.logger = logger;
        }

        public ProductSolution ConductAlgorithm()
        {
            var antIterator = new ImprovedAntIteration(this.simulation);
            for (int i = 0; i <  this.simulation.Configuration.MaximumIteration; i++)
            {
                antIterator.ConductIteration();
                if (antIterator.NoImprovementsCount >= this.simulation.Configuration.NoImprovementsStop)
                {
                    break;
                } else if (antIterator.NoImprovementsCount == 0) {
                    this.logger.LogIterativeSolution(antIterator.BestSolution);
                }
            }
            return antIterator.BestSolution;
        }
    }
}
