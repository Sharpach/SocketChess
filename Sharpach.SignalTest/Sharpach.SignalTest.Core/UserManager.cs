using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Sharpach.SignalTest.Entities;

namespace Sharpach.SignalTest.Core
{
    public class UserManager
    {
        private readonly List<User> _users;
        private readonly Dictionary<string, User> _connections;

        public UserManager()
        {
            _users = new List<User>();
            _connections = new Dictionary<string, User>();
        }

        public List<string> GetConnections(string username)
        {
            return _connections
                .Where(kv => kv.Value.Username == username)
                .Select(kv => kv.Key)
                .ToList();
        }

        public void AddConnection(string connectionId, string username)
        {
            var user = _users.SingleOrDefault(u => u.Username == username);
            if (user == null)
            {
                user = new User { Username = username };
                _users.Add(user);
            }

            if (!_connections.TryAdd(connectionId, user))
            {
                throw new Exception($"Connection {connectionId} with user {user} already exists");
            }
        }

        public void RemoveConnection(string connectionId)
        {
            if (_connections.ContainsKey(connectionId))
            {
                var user = _connections[connectionId];

                _connections.Remove(connectionId);

                if (_connections.All(kv => kv.Key != connectionId))
                {
                    _users.Remove(user);
                }
            }
            else
            {
                throw new Exception($"No connections {connectionId} exist");
            }
        }

        public User GetUser(string connectionId)
        {
            if (_connections.TryGetValue(connectionId, out var user))
            {
                return user;
            }
            else
            {
                throw new Exception($"There are no users for this connection {connectionId}");
            }
        }
    }
}
