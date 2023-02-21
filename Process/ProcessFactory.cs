using antDCVRP.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Process
{
    public static class ProcessFactory
    {
        public static void ConductSimulation()
        {

            var reader = ReaderResolver.ResolveStandardReader();

            reader.Read("C:\\Users\\Szymon\\Desktop\\msi-projects\\ant-DCVRP\\benchmark\\googledev\\full.xml");

            var simulation = reader.GetSimulation();

            Console.WriteLine(simulation);
        }
    }
}
