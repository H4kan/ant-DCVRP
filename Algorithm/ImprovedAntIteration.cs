using antDCVRP.Extensions;
using antDCVRP.Model;
using antDCVRP.RandomUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Algorithm
{
    public class ImprovedAntIteration : AntIteration
    {
       
        public ImprovedAntIteration(SimulationExt simulation): base(simulation) { }

        protected override double GetEvaporateFactor()
        {
            return this.simulation.Configuration.PersistenceTrail 
                + this.simulation.Configuration.Theta / this.GetAverageDistance();
        }

        private double GetAverageDistance()
        {
            return this.productSolutions.Average(s => s.Sum);
        }
    }
}
