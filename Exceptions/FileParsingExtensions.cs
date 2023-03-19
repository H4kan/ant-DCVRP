using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Exceptions
{
    public class InvalidCVRPrepFormatException : Exception
    {
        private const string message = "Provided file does not meet CVRPrep format convention";

        public InvalidCVRPrepFormatException(): base(message) { }
    }

    public class InputFileNotFoundException : Exception
    {
        private const string message = "File not found";

        public InputFileNotFoundException() : base(message) { }
    }

    public class InvalidConfigurationException : Exception
    {
        private const string message = "Configuration file is invalid";

        public InvalidConfigurationException() : base(message) { }
    }
}
