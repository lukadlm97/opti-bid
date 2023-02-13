using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OptiBid.Microservices.Shared.Caching.Configuration;
using StackExchange.Redis;

namespace OptiBid.Microservices.Shared.Caching.Factory
{
    public class RedisConnectionFactory : IDistributedCacheConnectionFactory
    {
        private readonly HybridCacheSettings _hybridCacheSettings;
        private Lazy<ConnectionMultiplexer> _connection;
        private IDatabase _database;

        public RedisConnectionFactory(IOptions<HybridCacheSettings> options)
        {
            this._hybridCacheSettings = options.Value;
            CreateConnection();
        }
        



        private void CreateConnection()
        {
            try
            {
                _connection = new Lazy<ConnectionMultiplexer>(
                        ConnectionMultiplexer.Connect(_hybridCacheSettings.DistributedCacheSettings.ServerName));

                _database = _connection.Value.GetDatabase(_hybridCacheSettings.DistributedCacheSettings.DbNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }


        private bool ConnectionExists()
        {
            if (_connection != null && _connection.Value.IsConnected)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }


        public IDatabase GetConnection()
        {
            if (ConnectionExists())
            {
                return _database;
            }

            throw new InvalidOperationException();
        }
    }
}
