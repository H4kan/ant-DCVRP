using antDCVRP.Algorithm;
using antDCVRP.Extensions;
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
        public GRAlgorithm(SimulationExt simulation) { 
            this.simulation = simulation;
        }

        public ProductSolution ConductAlgorithm()
        {
            throw new NotImplementedException();
        }
    }
}
