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
    public class ProductSolution
    {
        public List<Customer> Customers { get; private set; }

        public double Sum { get; set; }

        public IDistanceResolver distanceResolver;

        public ProductSolution(IDistanceResolver distanceResolver) 
        {
            this.Customers = new List<Customer>();
            this.Sum = 0;
            this.distanceResolver = distanceResolver;
        }

        public void AppendToSolution(Customer nextCustomer)
        {
            var prevCustomer = this.Customers.LastOrDefault();
            if (prevCustomer == null)
            {
                prevCustomer = nextCustomer;
            }
            this.Sum += this.distanceResolver.GetDist(prevCustomer.Id, nextCustomer.Id);
            this.Customers.Add(nextCustomer);

        }
    }
}
