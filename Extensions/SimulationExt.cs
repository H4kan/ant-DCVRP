using antDCVRP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Extensions
{
    public class SimulationExt : Simulation
    {
        private EuclideanDistanceResolver euclideanDistanceResolver = new EuclideanDistanceResolver();

        public SimulationExt(Simulation simulation) : base(simulation)
        {
            euclideanDistanceResolver.LoadDistances(Customers);
        }

        public double GetDist(int i, int j)
        {
            return euclideanDistanceResolver.GetDist(i, j);
        }
    }
}
