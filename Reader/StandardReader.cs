using antDCVRP.Exceptions;
using antDCVRP.Extensions;
using antDCVRP.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace antDCVRP.Reader
{
    // CVRPrep convention format reader
    public class StandardReader : IReader
    {

        private Simulation _simulation = new Simulation();

        public void Read(string path)
        {
            if (!File.Exists(path))
            {
                throw new InputFileNotFoundException();
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            if (doc == null || doc.DocumentElement == null)
            {
                throw new InvalidCVRPrepFormatException();
            }
            var networkNode = doc.DocumentElement.SelectSingleNode("/instance/network/nodes");
            var fleetNode = doc.DocumentElement.SelectSingleNode("/instance/fleet/vehicle_profile");
            var requestsNode = doc.DocumentElement.SelectSingleNode("/instance/requests");
            if (networkNode == null || fleetNode == null || requestsNode == null) 
            {
                throw new InvalidCVRPrepFormatException();
            }
            this.ReadNetwork(networkNode);
            this.ReadFleet(fleetNode);
            this.ReadRequests(requestsNode);
        }

        private void ReadNetwork(XmlNode node)
        {
            if (!node.HasChildNodes)
            {
                throw new InvalidCVRPrepFormatException();
            }
            foreach(var childNode in node.ChildNodes)
            {
                var newCustomer = new Customer();
                var textId = ((System.Xml.XmlElement)childNode).Attributes.GetNamedItem("id")?.InnerText;
                if (textId == null)
                {
                    throw new InvalidCVRPrepFormatException();
                }
                newCustomer.Id = SafeIntParse(textId);


                var positionNodes = ((System.Xml.XmlNode)childNode).ChildNodes;

                if (positionNodes == null || positionNodes.Count != 2)
                {
                    throw new InvalidCVRPrepFormatException();
                }

                if (positionNodes[0]?.Name != "cx" || positionNodes[1]?.Name != "cy")
                {
                    throw new InvalidCVRPrepFormatException();
                }

                newCustomer.Position.X = SafeDoubleParse(positionNodes[0]?.InnerText);
                newCustomer.Position.Y = SafeDoubleParse(positionNodes[1]?.InnerText);

                this._simulation.Customers.Add(newCustomer);

            }
        }

        private void ReadFleet(XmlNode node)
        {
            if (node.ChildNodes.Count != 3)
            {
                throw new InvalidCVRPrepFormatException();
            }

            var departureNode = node.ChildNodes[0];
            if (departureNode?.Name != "departure_node")
            {
                throw new InvalidCVRPrepFormatException();
            }

            this._simulation.Vehicle.StartId = this.SafeIntParse(departureNode?.InnerText);

            var capacityNode = node.ChildNodes[2];
            if (capacityNode?.Name != "capacity")
            {
                throw new InvalidCVRPrepFormatException();
            }

            this._simulation.Vehicle.Capacity = this.SafeDoubleParse(capacityNode?.InnerText);
        }

        private void ReadRequests(XmlNode node)
        {
            if (!node.HasChildNodes)
            {
                throw new InvalidCVRPrepFormatException();
            }
            foreach (var childNode in node.ChildNodes)
            {
                var textId = ((System.Xml.XmlElement)childNode).Attributes.GetNamedItem("node")?.InnerText;
                if (textId == null)
                {
                    throw new InvalidCVRPrepFormatException();
                }

                var requestedCustomer = this._simulation.Customers.FirstOrDefault(c => c.Id == SafeIntParse(textId));
                if (requestedCustomer == null)
                {
                    throw new InvalidCVRPrepFormatException();
                }

                var quantityNode = ((System.Xml.XmlNode)childNode).FirstChild;

                if (quantityNode == null || quantityNode.Name != "quantity")
                {
                    throw new InvalidCVRPrepFormatException();
                }

                requestedCustomer.Demand = SafeDoubleParse(quantityNode?.InnerText);
                if (requestedCustomer.Demand > this._simulation.Vehicle.Capacity)
                {
                    throw new InvalidCVRPrepFormatException();
                }

            }
        }

        private int SafeIntParse(string? text)
        {
            if (text == null)
            {
                throw new InvalidCVRPrepFormatException();
            }
            try
            {
                return Int32.Parse(text);
            } catch
            {
                throw new InvalidCVRPrepFormatException();
            }
        }

        private double SafeDoubleParse(string? text)
        {
            if (text == null)
            {
                throw new InvalidCVRPrepFormatException();
            }
            try
            {
                return double.Parse(text, CultureInfo.InvariantCulture);
            }
            catch
            {
                throw new InvalidCVRPrepFormatException();
            }
        }

        public Simulation GetSimulation()
        {
            return _simulation.DeepClone();
        }
    }
}
