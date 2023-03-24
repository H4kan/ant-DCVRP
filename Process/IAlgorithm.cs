using antDCVRP.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Process
{
    public interface IAlgorithm
    {
        public ProductSolution ConductAlgorithm();

    }
}
