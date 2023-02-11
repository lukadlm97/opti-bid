using System.Collections.Concurrent;

namespace OptiBid.Microservices.Messaging.Receving.Models
{
    public class ConnectionManager
    {
        private readonly ConcurrentDictionary<string, List<string>> _connections;

        public ConnectionManager()
        {
            _connections = new ConcurrentDictionary<string, List<string>>();
        }

        public IEnumerable<string> GetConnections(string topic)
        {
            return _connections[topic];
        }

        public void AddConnection(string connectionId,string topic)
        {
            if (!_connections.Keys.Contains(topic))
            {
                _connections.TryAdd(topic,new List<string>(){connectionId});
            }
            else
            {
                _connections[topic].Add(connectionId);
            }
        }

        public void RemoveConnection(string connectionId, string topic)
        {
            if (_connections.ContainsKey(topic) && _connections[topic].Contains(connectionId))
            {
                _connections[topic].Remove(connectionId);
            }
        }

        public bool IsSubscribed(string connectionId, string topic)
        {
            if (_connections.ContainsKey(topic) && _connections[topic].Contains(connectionId))
            {
                return true;
            }

            return false;
        }
    }
}
