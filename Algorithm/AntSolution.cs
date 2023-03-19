using antDCVRP.Extensions;
using antDCVRP.Model;
using antDCVRP.RandomUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Algorithm
{
    public class AntSolution
    {
        public List<Customer> NotExploredCustomers { get; set; }

        private SimulationExt simulation { get; set; }

        public ProductSolution Solution { get; private set; }

        public AntSolution(SimulationExt simulation) 
        {
            this.simulation = simulation;
            this.NotExploredCustomers = new List<Customer>();
            this.NotExploredCustomers.AddRange(this.simulation.Customers);
            this.NotExploredCustomers.Remove(this.simulation.InitialCustomer);
            this.Solution = new ProductSolution(this.simulation.distanceResolver);
        }

        public bool ConductSolution()
        {
            bool solutionExists = true;
            while (this.NotExploredCustomers.Count > 0 && solutionExists)
            {
                solutionExists = this.ConductCycle();
            }
            this.Solution.AppendToSolution(this.simulation.InitialCustomer);
            return this.NotExploredCustomers.Count == 0;
        }

        private bool ConductCycle()
        {
            this.Solution.AppendToSolution(this.simulation.InitialCustomer);

            var antRoute = new AntRoute(this.simulation, this.NotExploredCustomers);

            if (antRoute.AvailableCustomers.Count == 0)
            {
                return false;
            }

            var currentCustomer = RandomManager.GetRandomInList(antRoute.AvailableCustomers);
            
            antRoute.MoveAntRoute(currentCustomer);
            this.NotExploredCustomers.Remove(currentCustomer);
            this.Solution.AppendToSolution(currentCustomer);

            while (antRoute.AvailableCustomers.Count > 0)
            {
                var nextCustomer = GetNextCustomer(
                    antRoute.AvailableCustomers
                    .Take(this.simulation.FeasibleNeighbourhoodCount).ToList(), currentCustomer);
                antRoute.MoveAntRoute(nextCustomer);
                this.NotExploredCustomers.Remove(nextCustomer);
                this.Solution.AppendToSolution(nextCustomer);
                currentCustomer = nextCustomer;
            }

            return true;
        }

        private Customer GetNextCustomer(List<Customer> availableCustomers, Customer currentCustomer)
        {
            if (availableCustomers.Count < this.simulation.FeasibleNeighbourhoodCount)
            {
                return availableCustomers.First();
            }

            var influences = availableCustomers.Select(c => this.simulation.feromonManager.GetInfluence(currentCustomer.Id, c.Id)).ToList();

            return RandomManager.GetRandomFromInfluence(influences, availableCustomers);
        }
    }
}
