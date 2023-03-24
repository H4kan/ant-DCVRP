using antDCVRP.Config;
using antDCVRP.Exceptions;
using antDCVRP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Extensions
{
    public class FeromonManager
    {
        private Dictionary<int, Dictionary<int, double>> feromons = new Dictionary<int, Dictionary<int, double>>();
        private Dictionary<int, Dictionary<int, double>> influence = new Dictionary<int, Dictionary<int, double>>();



        private IDistanceResolver DistanceResolver { get; set; }
        private SimulationConfiguration Configuration { get; set; }

        private List<Customer> Customers { get; set; }

        private Customer InitialCustomer { get; set; }

        public FeromonManager(IDistanceResolver distanceResolver, SimulationConfiguration configuration)
        {
            this.DistanceResolver = distanceResolver;
            this.Configuration = configuration;
        }

        public void LoadFeromons(List<Customer> customers, Customer initialCustomer)
        {
            this.Customers = customers;
            this.InitialCustomer = initialCustomer;
            for (int i = 0; i < customers.Count; i++)
            {
                var feromonsDict = new Dictionary<int, double>();
                var probabilitesDict = new Dictionary<int, double>();
                feromons.Add(customers[i].Id, feromonsDict);
                influence.Add(customers[i].Id, probabilitesDict);
                for (int j = 0; j < customers.Count; j++)
                {
                    if (i != j)
                    {
                        feromons[customers[i].Id][customers[j].Id] = GetInitialFeromonValue();
                    }
                }
            }
            this.RecalculateInfluence();
        }

        public void RecalculateInfluence()
        {
            if (this.Configuration.HeuristicDistribution)
            {
                this.RecalculateHeuristicInfluence();
            }
            else
            {
                this.RecalculateSimpleInfluence();
            }

        }

        private void RecalculateSimpleInfluence()
        {
            for (int i = 0; i < this.Customers.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    var pheromonPart = this.GetFeromon(this.Customers[i].Id, this.Customers[j].Id);

                    influence[this.Customers[i].Id][this.Customers[j].Id] = pheromonPart;
                    influence[this.Customers[j].Id][this.Customers[i].Id] = pheromonPart;
                }
            }
        }

        private void RecalculateHeuristicInfluence()
        {
            for (int i = 0; i < this.Customers.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    var pheromonPart = Math.Pow(this.GetFeromon(this.Customers[i].Id, this.Customers[j].Id),
                        this.Configuration.Alpha);

                    var heuristicPart = 1 / Math.Pow(this.DistanceResolver.GetDist(this.Customers[i].Id, this.Customers[j].Id),
                        this.Configuration.Beta);

                    var savingsPart = Math.Pow(
                        this.DistanceResolver.GetDist(this.Customers[i].Id, this.InitialCustomer.Id)
                        + this.DistanceResolver.GetDist(this.Customers[j].Id, this.InitialCustomer.Id)
                        - this.DistanceResolver.GetDist(this.Customers[i].Id, this.Customers[j].Id),
                        this.Configuration.Gamma);
                    var productParts = heuristicPart * pheromonPart * savingsPart;

                    influence[this.Customers[i].Id][this.Customers[j].Id] = productParts;
                    influence[this.Customers[j].Id][this.Customers[i].Id] = productParts;
                }
            }
        }

        private double GetInitialFeromonValue()
        {
            return Configuration.InitialFeromon;
        }

        public double GetInfluence(int customerId1, int customerId2)
        {
            if (customerId1== customerId2)
            {
                throw new InvalidFeromonReferenceException();
            }
            return influence[customerId1][customerId2];
        }

        public double GetFeromon(int customerId1, int customerId2)
        {
            if (customerId1 == customerId2)
            {
                throw new InvalidFeromonReferenceException();
            }
            return feromons[customerId1][customerId2];
        }

        public void IncreaseFeromon(int customerId1, int customerId2, double newValue)
        {
            if (customerId1 == customerId2)
            {
                throw new InvalidFeromonReferenceException();
            }
            feromons[customerId1][customerId2] += newValue;
        }

        public void EvaportateFeromon(double evaporateFactor)
        { 
            for (int i = 0; i < this.Customers.Count; i++)
            {
                for (int j = 0; j < this.Customers.Count; j++)
                {
                    if (i != j)
                    {
                        this.feromons[this.Customers[i].Id][this.Customers[j].Id] *= evaporateFactor;
                    }
                }
            }
        }
    }
}
