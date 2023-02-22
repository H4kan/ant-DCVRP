using antDCVRP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Extensions
{
    public interface IDistanceResolver
    {
        public void LoadDistances(List<Customer> customers);

        public double GetDist(int customerId1, int customerId2);
    }
}
