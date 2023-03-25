using antDCVRP.Algorithm;
using antDCVRP.Extensions;
using antDCVRP.Model;
using antDCVRP.RandomUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Greedy
{
    public class GRIteration
    {
        private SimulationExt simulation;

        private Customer startingCustomer;

        private double capacityLeft;

        private List<Customer> NotExploredCustomers { get; set; }

        public GRIteration(SimulationExt simulation, int startingCustomer)
        {
            this.simulation = simulation;
            this.startingCustomer = this.simulation.Customers[startingCustomer];

            this.NotExploredCustomers = new List<Customer>();
            this.NotExploredCustomers.AddRange(this.simulation.Customers);
            this.NotExploredCustomers.Remove(this.simulation.InitialCustomer);

            this.capacityLeft = this.simulation.Vehicle.Capacity;
        }

        public ProductSolution? ConductIteration()
        {
            var solution = new ProductSolution(simulation.distanceResolver);

            var currentCustomer = startingCustomer;

            while (this.NotExploredCustomers.Count > 0)
            {
                solution.AppendToSolution(this.simulation.InitialCustomer);
                this.capacityLeft = this.simulation.Vehicle.Capacity;

                while (this.NotExploredCustomers.Count > 0)
                {
                    if (currentCustomer.Demand > this.capacityLeft)
                    {
                        return null;
                    }
                    this.NotExploredCustomers.Remove(currentCustomer);
                    solution.AppendToSolution(currentCustomer);
                    this.capacityLeft -= currentCustomer.Demand;

                    var nextCustomer = this.NotExploredCustomers
                        .Where(nc => nc.Demand <= this.capacityLeft)
                        .OrderBy(nc =>
                        this.simulation.distanceResolver.GetDist(currentCustomer.Id, nc.Id))
                        .ThenByDescending(nc => nc.Demand).FirstOrDefault();

                    if (nextCustomer != null)
                    {
                        currentCustomer = nextCustomer;
                    }
                    else if (this.NotExploredCustomers.Count > 0)
                    {
                        currentCustomer = RandomManager.GetRandomInList(this.NotExploredCustomers);
                        break;
                    }
                    else break;
                }
            }

            solution.AppendToSolution(this.simulation.InitialCustomer);

            return solution;
        }
    }
}
