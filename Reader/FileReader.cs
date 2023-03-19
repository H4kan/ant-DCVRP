using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using antDCVRP.Config;
using antDCVRP.Exceptions;
using antDCVRP.Extensions;
using antDCVRP.Model;
using antDCVRP.Output;
using Microsoft.Extensions.Configuration;

namespace antDCVRP.Reader
{
    public abstract class FileReader
    {
        protected Simulation _simulation = new Simulation();

        public SimulationConfiguration Configuration { get; private set; }

        public IOConfiguration IOConfiguration { get; private set; }

        public Simulation GetSimulation()
        {
            return _simulation.DeepClone();
        }

        public abstract void ReadMap();


        public void LoadConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var loadedConfig = config.GetRequiredSection("SimulationConfiguration").Get<SimulationConfiguration>();
            if (loadedConfig == null)
            {
                throw new InvalidConfigurationException();
            }
            this.Configuration = loadedConfig;
            
        }

        public void LoadOuputConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            var loadedConfig = config.GetRequiredSection("IOConfiguration").Get<IOConfiguration>();
            if (loadedConfig == null)
            {
                throw new InvalidConfigurationException();
            }
            this.IOConfiguration = loadedConfig;
            
        }
    }
}
