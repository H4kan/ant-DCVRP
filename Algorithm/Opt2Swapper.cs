using antDCVRP.Model;
using antDCVRP.RandomUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Algorithm
{
    public static class Opt2Swapper
    {

        public static ProductSolution Retrieve2OptimizedSolution(ProductSolution solution)
        {
            // each cycle is represented by ints numbering edges before certain vertex
            var cycles = new List<List<Customer>>();

            var initialCustomerId = solution.Customers.First().Id;

            int currentCycle = -1;

            for (int i = 0; i < solution.Customers.Count; i++)
            {
                if (solution.Customers[i].Id == initialCustomerId)
                {
                    cycles.Add(new List<Customer>());
                    cycles[++currentCycle].Add(solution.Customers[initialCustomerId]);
                }
                else
                {
                    cycles[currentCycle].Add(solution.Customers[i]);
                }
            }

            bool anyChange = true;

            while (anyChange)
            {
                anyChange = false;
                for (int i = 0; i < cycles.Count; i++)
                {

                    // for two edges swap dont work
                    // in euclidean space 3 doesnt change anything either
                    if (cycles[i].Count <= 3)
                    {
                        continue;
                    }

                    for (int j = 2; j < cycles[i].Count; j++)
                    {
                        for (int k = 0; k < j - 1; k++)
                        {
                            //if (cycles[i][j].Id == 5 && cycles[i][k].Id == 6)
                            //{
                            //    Console.WriteLine("xd");
                            //}
                            if (TrySwap(j, k, cycles[i], solution, initialCustomerId))
                            {
                                anyChange= true;
                            }   
                        }
                    }
                }
            }

            var newSolution = new ProductSolution(solution.distanceResolver);
            for (int i = 0; i < cycles.Count; i++)
            {
                for (int j = 0; j < cycles[i].Count; j++)
                {
                    newSolution.AppendToSolution(cycles[i][j]);
                }
            }
            newSolution.AppendToSolution(solution.Customers.First());
            return solution;
        }

        private static bool TrySwap(int j, int k, List<Customer> cycle, ProductSolution solution, int initialCustomerId)
        {
            var currentSum = solution.distanceResolver
                                .GetDist(cycle[k].Id, cycle[
                                    k > 0 ? k - 1 : cycle.Count - 1].Id)
                                + solution.distanceResolver
                                .GetDist(cycle[j].Id, cycle[j - 1].Id);

            var changedSum = solution.distanceResolver
                .GetDist(cycle[k].Id, cycle[j].Id)
                + solution.distanceResolver
                .GetDist(cycle[
                    k > 0 ? k - 1 : cycle.Count - 1].Id,
                    cycle[j - 1].Id);

            if (changedSum >= currentSum)
            {
                return false;
            }


            // revert from min to max - 1
            for (int p = 0; p < (j - k) / 2; p++)
            {
                var handler = cycle[k + p];
                cycle[k + p] = cycle[j - 1 - p];
                cycle[j - 1 - p] = handler;
            }


            // get back 0 on start
            if (cycle[0].Id != initialCustomerId)
            {
                var initialIdx = cycle.FindIndex(c => c.Id == initialCustomerId);
                var previousSeq = cycle.Take(initialIdx);
                var proceedingSeq = cycle.Skip(initialIdx);
                var newCycle = proceedingSeq.Concat(previousSeq).ToList();

                cycle.Clear();
                cycle.AddRange(newCycle);
            }

            return true;
        }

    }
}
