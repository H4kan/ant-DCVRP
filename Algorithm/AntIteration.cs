using antDCVRP.Exceptions;
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
    public class AntIteration
    {
        public List<Customer> Customers { get; set; }

        protected SimulationExt simulation { get; set; }

        protected List<ProductSolution> productSolutions { get; set; }

        public ProductSolution BestSolution { get; set; }

        public int NoImprovementsCount { get; set; }

        public AntIteration(SimulationExt simulation) 
        {
            this.simulation = simulation;
            this.Customers = new List<Customer>();
            this.Customers.AddRange(this.simulation.Customers);
            this.productSolutions = new List<ProductSolution>();
        }

        public void ConductIteration()
        {
            this.productSolutions.Clear();
            for (int i = 0; i < this.simulation.Configuration.AntsPerIteration; i++)
            {
                var newSolution = new AntSolution(this.simulation);
                if (!newSolution.ConductSolution())
                {
                    throw new NotFoundSolutionException();
                }

                var processedSolution = ProcessLinearHeuristics(newSolution.Solution);

                this.productSolutions.Add(processedSolution);
            }

            var elitistTake = this.simulation.Configuration.EliteAntsSelection ? 
                this.simulation.Configuration.Sigma - 1 : this.productSolutions.Count();
            var elitistSolutions = this.productSolutions.OrderBy(s => s.Sum)
                .Take(elitistTake).ToList();

            var bestInIteration = elitistSolutions.First();
            if (this.BestSolution == null || bestInIteration.Sum < this.BestSolution.Sum)
            {
                this.BestSolution = bestInIteration;
                this.NoImprovementsCount = 0;
            }
            else
            {
                this.NoImprovementsCount++;
            }

            this.UpdateFeromons(elitistSolutions);
        }

        public ProductSolution ProcessLinearHeuristics(ProductSolution solution) {
            if (!this.simulation.Configuration.Opt2Optimization)
            {
                return solution;
            }
            return Opt2Swapper.Retrieve2OptimizedSolution(solution);
        }

        private void UpdateFeromons(List<ProductSolution> elitistSolutions)
        {
            this.EvaporatePheromons();

            for (int lambda = 1; lambda < elitistSolutions.Count; lambda++)
            {
                this.DepositLambdaPheromons(elitistSolutions[lambda - 1], lambda, elitistSolutions.Count);
            }
            this.DepositBestPheromons(this.BestSolution, elitistSolutions.Count);
            this.simulation.feromonManager.RecalculateInfluence();
        }

        private void EvaporatePheromons()
        {
            this.simulation.feromonManager.EvaportateFeromon(this.GetEvaporateFactor());
        }

        private void DepositLambdaPheromons(ProductSolution solution, int lambda, int maxLambda)
        {
            var increase = (maxLambda - lambda) / solution.Sum;
            for (int i = 0; i < solution.Customers.Count - 1; i++)
            {
                this.simulation.feromonManager.IncreaseFeromon(solution.Customers[i].Id, solution.Customers[i + 1].Id, increase);
            }
        }

        private void DepositBestPheromons(ProductSolution solution, int maxLambda)
        {
            var increase = maxLambda/ solution.Sum;
            for (int i = 0; i < solution.Customers.Count - 1; i++)
            {
                this.simulation.feromonManager.IncreaseFeromon(solution.Customers[i].Id, solution.Customers[i + 1].Id, increase);
            }
        }

        protected virtual double GetEvaporateFactor()
        {
            return simulation.Configuration.PersistenceTrail;
        }
    }
}
