using antDCVRP.Algorithm;
using antDCVRP.Exceptions;
using antDCVRP.Extensions;
using antDCVRP.Output;
using antDCVRP.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Greedy
{
    public class GRAlgorithm : IAlgorithm
    {
        private SimulationExt simulation;

        private Logger logger;

        public GRAlgorithm(SimulationExt simulation, Logger logger) { 
            this.simulation = simulation;
            this.logger = logger;
        }

        public ProductSolution ConductAlgorithm()
        {
            ProductSolution bestSolution = new ProductSolution(this.simulation.distanceResolver);
            bestSolution.Sum = double.MaxValue; 

            for (int i = 0; i < this.simulation.Customers.Count; i++)
            {
                if (this.simulation.Customers[i] == this.simulation.InitialCustomer) continue;

                var iteration = new GRIteration(this.simulation, i);

                var currentSolution = iteration.ConductIteration();

                if (currentSolution == null)
                {
                    throw new NotFoundSolutionException();
                }
                if (currentSolution.Sum < bestSolution.Sum)
                {
                    bestSolution = currentSolution;
                    this.logger.LogIterativeSolution(bestSolution);
                }
            }

            return bestSolution;
        }
    }
}
