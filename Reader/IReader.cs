using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using antDCVRP.Model;

namespace antDCVRP.Reader
{
    public interface IReader
    {
        public Simulation GetSimulation();

        public void Read(string path);

    }
}
