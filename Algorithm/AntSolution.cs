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
    public class AntSolution
    {
        public List<Customer> Customers { get; set; }

        public List<Customer> NotExploredCustomers { get; set; }

        private SimulationExt simulation { get; set; }

        public AntSolution(SimulationExt simulation) 
        {
            this.simulation = simulation;
            this.NotExploredCustomers = new List<Customer>();
            this.NotExploredCustomers.AddRange(this.simulation.Customers);
        }

        public void ConductSolution()
        {
            var antRoute = new AntRoute(this.simulation, this.NotExploredCustomers);

            var currentCustomer = RandomManager.GetRandomInList(this.NotExploredCustomers);
            
            antRoute.MoveAntRoute(currentCustomer);

            while (antRoute.AvailableCustomers.Count > 0)
            {
                var nextCustomer = GetNextCustomer(antRoute.AvailableCustomers, currentCustomer);
                antRoute.MoveAntRoute(nextCustomer);
                currentCustomer = nextCustomer;
            }

        }

        private Customer GetNextCustomer(List<Customer> availableCustomers, Customer currentCustomer)
        {
            var influences = availableCustomers.Select(c => this.simulation.GetInfluence(currentCustomer.Id, c.Id)).ToList();

            return RandomManager.GetRandomFromInfluence(influences, availableCustomers);
        }
    }
}
