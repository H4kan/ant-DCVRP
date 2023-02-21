using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Model
{
    [Serializable]
    public class VehicleInfo
    {
        public double Capacity { get; set; }

        public int StartId { get; set; }
    }
}
