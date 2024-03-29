﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Model
{
    [Serializable]
    public class Simulation
    {
        public Simulation() {
            this.Vehicle = new VehicleInfo();
            this.Customers = new List<Customer>();
        }

        public Simulation(Simulation simulation)
        {
            this.Vehicle = simulation.Vehicle;
            this.Customers = simulation.Customers;
        }

        public List<Customer> Customers { get; set; }

        public VehicleInfo Vehicle { get; set; }
    }
}
