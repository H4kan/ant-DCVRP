using antDCVRP.Config;
using antDCVRP.Exceptions;
using antDCVRP.Model;
using antDCVRP.Output;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Extensions
{
    public class SimulationExt : Simulation
    {
        public IDistanceResolver distanceResolver { get; set; }
        public FeromonManager feromonManager { get; set; }

        public SimulationConfiguration Configuration { get; private set; }

        public int FeasibleNeighbourhoodCount { get; private set; }

        public Customer InitialCustomer { get; private set; }

        public SimulationExt(Simulation simulation, SimulationConfiguration configuration) : base(simulation)
        {
            this.LoadConfiguration(configuration);

            distanceResolver = new EuclideanDistanceResolver();
            feromonManager = new FeromonManager(distanceResolver, Configuration);

            distanceResolver.LoadDistances(Customers);
            InitialCustomer = Customers.First(c => c.Id == this.Vehicle.StartId);


            feromonManager.LoadFeromons(Customers, this.InitialCustomer);
            

            FeasibleNeighbourhoodCount = (int)Math.Ceiling((double)(Customers.Count / this.Configuration.FeasibleNeighbourhoodFactor));
        }

        public void LoadConfiguration(SimulationConfiguration configuration)
        {
            this.Configuration = configuration;
            if (this.Configuration.AntsPerIteration == 0)
            {
                this.Configuration.AntsPerIteration = this.Customers.Count;
            }
        }

        public double GetDist(int i, int j)
        {
            return distanceResolver.GetDist(i, j);
        }
    }
}
