using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Config
{
    public sealed class SimulationConfiguration
    {
        public required  double VehicleMaxDistance { get; set; }

        public required int NumberOfVehicles { get; set; }

        public required double Alpha { get; set; }
        public required double Beta { get; set; }
        public required double Gamma { get; set; }

        public required int AntsPerIteration { get; set; }
        public required int MaximumIteration { get; set; }
        public required int NoImprovementsStop { get; set; }

        public required double PersistenceTrail { get; set; }

        public required double Theta { get; set; }

        public required int Sigma { get; set; }

        public required int FeasibleNeighbourhoodFactor { get; set;}

        public required bool Opt2Optimization { get; set; } 
    }
}
