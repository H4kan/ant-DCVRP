using antDCVRP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Extensions
{
    public class EuclideanDistanceResolver : IDistanceResolver
    {
        private Dictionary<int, Dictionary<int, double>> distances = new Dictionary<int, Dictionary<int, double>>();

        public void LoadDistances(List<Customer> customers)
        {
            for (int i = 0; i < customers.Count; i++)
            {
                var customerDict = new Dictionary<int, double>();
                distances.Add(customers[i].Id, customerDict);
                for (int j = 0; j < customers.Count; j++)
                {
                    distances[customers[i].Id][customers[j].Id] = ResolveEuclidanDistance(customers[i].Position, customers[j].Position);
                }
            }
        }

        private double ResolveEuclidanDistance(PositionXY pos1, PositionXY pos2)
        {
            return Math.Sqrt((pos1.X - pos2.X) * (pos1.X - pos2.X) + (pos1.Y - pos2.Y) * (pos1.Y - pos2.Y));
        }

        public double GetDist(int customerId1, int customerId2)
        {
            return distances[customerId1][customerId2];
        }
    }
}
