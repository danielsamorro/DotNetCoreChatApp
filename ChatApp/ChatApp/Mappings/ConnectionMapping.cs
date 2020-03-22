using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Mappings
{
    public static class ConnectionMapping<T>
    {
        private static Dictionary<T, HashSet<string>> _connections = new Dictionary<T, HashSet<string>>();

        public static int Count => _connections.Count;

        public static void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;

                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        internal static IEnumerable<T> GetAllUserNames()
        {
            return _connections.Keys;
        }

        public static IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;

            if (_connections.TryGetValue(key, out connections))
                return connections;

            return Enumerable.Empty<string>();
        }

        public static void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;

                if (!_connections.TryGetValue(key, out connections))
                    return;

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                        _connections.Remove(key);
                }
            }
        }
    }    
}

