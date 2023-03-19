using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Config
{
    public sealed class IOConfiguration
    {
        public required string InputFile { get; set; }

        public required string OutputFile { get; set; }

        public required bool LogEachImprovement { get; set; }
    }
}
