using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace WebApi
{
    public class DatabaseConnectionFactory
    {
        private readonly ConnectionStringSettings _settings;

        public DatabaseConnectionFactory(ConnectionStringSettings settings)
        {
            _settings = settings;
        }

        public IDbConnection Create()
        {
            var provider = DbProviderFactories.GetFactory(_settings.ProviderName);
            var connection = provider.CreateConnection();
            if (connection == null)
            {
                var message = $"Could not create a connection for {_settings.ProviderName}, from setting key ${_settings.Name}";
                throw new InvalidOperationException(message);
            }

            connection.ConnectionString = _settings.ConnectionString;
            connection.Open();

            return connection;
        }
    }
}