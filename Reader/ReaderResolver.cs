using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Reader
{
    public static class ReaderResolver
    {
        public static FileReader ResolveStandardReader() { return new StandardReader(); }
    }
}
