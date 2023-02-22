using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.RandomUtils
{
    public static class RandomManager
    {
        public static Random Generator { get; set; }

        public static void SetupSeed(int seed)
        {
            Generator = new Random(seed);
        }

        public static T GetRandomInList<T>(List<T> list)
        {
            return list[Generator.Next(0, list.Count)];
        }

        public static T GetRandomFromInfluence<T>(List<double> distribution, List<T> list)
        {
            var summedDist = distribution.Sum(x => x);
            var draftRandom = Generator.NextDouble();

            var i = 0;
            var currentSum = 0.0;
            while (currentSum <= draftRandom)
            {
                currentSum += distribution[i] / summedDist;
                i++;
            }

            return list[i - 1];
        }
    }
}
