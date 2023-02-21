using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace antDCVRP.Extensions
{
        public static class ExtensionMethods
        {
            // Deep clone
            public static T DeepClone<T>(this T a)
            {
                if (a == null)
                {
                    throw new ArgumentNullException(nameof(a));
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                formatter.Serialize(stream, a);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
                stream.Position = 0;
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                return (T)formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            }
            }
        }
}
