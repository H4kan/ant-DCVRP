using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Model
{
    [Serializable]
    public class Customer
    {
        public int Id { get; set; }

        public PositionXY Position { get; set; }

        public decimal Demand { get; set; }

        public Customer() {
            this.Position = new PositionXY();
            this.Demand = 0;
        }

    }

    [Serializable]
    public class PositionXY
    {
        public decimal X { get; set; }

        public decimal Y { get; set; }
    }
}
