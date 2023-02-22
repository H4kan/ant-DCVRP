using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Exceptions
{
    public class InvalidFeromonReferenceException : Exception
    {
        private const string message = "Attempted to change feromon value between same customer";

        public InvalidFeromonReferenceException(): base(message) { }
    }
}
