using antDCVRP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Extensions
{
    public class EuclideanDistanceResolver
    {
        private double[][] distances = new double[0][];

        public void LoadDistances(List<Customer> customers)
        {
            distances = new double[customers.Count][];
            for (int i = 0; i < customers.Count; i++)
            {
                distances[i] = new double[customers.Count];
                for (int j = 0; j < customers.Count; j++)
                {
                    distances[i][j] = ResolveEuclidanDistance(customers[i].Position, customers[j].Position);
                }
            }
        }

        private double ResolveEuclidanDistance(PositionXY pos1, PositionXY pos2)
        {
            return Math.Sqrt((pos1.X - pos2.X) * (pos1.X - pos2.X) + (pos1.Y - pos2.Y) * (pos1.Y - pos2.Y));
        }

        public double GetDist(int i, int j)
        {
            return distances[i][j];
        }
    }
}
