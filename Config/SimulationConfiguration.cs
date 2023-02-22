using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Config
{
    public class SimulationConfiguration
    {
        public double VehicleMaxDistance { get; private set; }

        public int NumberOfVehicles { get; private set; }

        public double Alpha { get; private set; }
        public double Beta { get; private set; }
        public double Gamma { get; private set; }

        public int AntsPerIteration { get; private set; }

        public SimulationConfiguration() 
        {
            this.VehicleMaxDistance = 100.0;
            this.NumberOfVehicles = 4;
            this.AntsPerIteration = 3;
            this.Alpha = 1.0;
            this.Beta = 1.0;
            this.Gamma = 1.0;

        }
    }
}
