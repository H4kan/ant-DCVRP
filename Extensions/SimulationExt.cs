using antDCVRP.Config;
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
        private IDistanceResolver distanceResolver { get; set; }
        private FeromonManager feromonManager { get; set; }

        public SimulationConfiguration Configuration = new SimulationConfiguration();

        public Customer InitialCustomer { get; private set; }

        public SimulationExt(Simulation simulation) : base(simulation)
        {
            distanceResolver = new EuclideanDistanceResolver();
            feromonManager = new FeromonManager(distanceResolver, Configuration);

            distanceResolver.LoadDistances(Customers);
            feromonManager.LoadFeromons(Customers, this.Vehicle.StartId);
            InitialCustomer = Customers.First(c => c.Id == this.Vehicle.StartId);
        }

        public double GetDist(int i, int j)
        {
            return distanceResolver.GetDist(i, j);
        }

        public void IncreaseFeromon(int i, int j, double newValue)
        {
            feromonManager.IncreaseFeromon(i, j, newValue);
        }

        public double GetInfluence(int i, int j)
        {
            return feromonManager.GetInfluence(i, j);
        }
    }
}
