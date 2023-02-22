using antDCVRP.Extensions;
using antDCVRP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Algorithm
{
    public class AntRoute
    {
        public Customer Current { get; set; }

        public double CapacityLeft { get; set; }

        public double DistanceLeft { get; set; }

        public List<Customer> AvailableCustomers { get; set; }

        private SimulationExt simulation { get; set; }
        
        public AntRoute(SimulationExt simulation, List<Customer> notExploredCustomers)
        {
            this.simulation = simulation;
            this.Current = simulation.InitialCustomer;
            this.CapacityLeft = simulation.Vehicle.Capacity;
            this.DistanceLeft = simulation.Configuration.VehicleMaxDistance;
            this.AvailableCustomers = new List<Customer>();
            this.AvailableCustomers.AddRange(notExploredCustomers);
            TrimAvailableCustomers();
        }

        public void MoveAntRoute(Customer targetCustomer)
        {
            this.CapacityLeft -= targetCustomer.Demand;
            this.DistanceLeft -= this.simulation.GetDist(this.Current.Id, targetCustomer.Id);
            this.Current = targetCustomer;
            TrimAvailableCustomers();
        }

        private void TrimAvailableCustomers()
        {
            this.AvailableCustomers = this.AvailableCustomers.Where(c => 
                this.CapacityLeft >= c.Demand && 
                this.DistanceLeft >= this.simulation.GetDist(this.Current.Id, c.Id) 
                    + this.simulation.GetDist(this.Current.Id, this.simulation.InitialCustomer.Id)
                && c.Id != this.Current.Id).ToList();
        }
    }
}
