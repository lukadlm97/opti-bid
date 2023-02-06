using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptiBid.Microservices.Messaging.Receving.Models
{
    public class ConnectionManager
    {
        private readonly Dictionary<string, List<string>> _connections;

        public ConnectionManager()
        {
            _connections = new Dictionary<string, List<string>>();
        }

        public IEnumerable<string> GetConnections(string topic)
        {
            return _connections[topic];
        }

        public void AddConnection(string connectionId,string topic)
        {
            if (_connections[topic] == null)
            {
                _connections.Add(topic,new List<string>(){connectionId});
            }
            else
            {
                _connections[topic].Add(connectionId);
            }
        }

        public void RemoveConnection(string connectionId, string topic)
        {
            if (_connections.ContainsKey(topic))
            {
                _connections[topic].Remove(connectionId);
            }
        }
    }
}
