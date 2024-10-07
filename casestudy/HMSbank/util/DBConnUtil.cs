using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration; // Requires Microsoft.Extensions.Configuration.Json NuGet package

namespace BankApp.util
{
    public class DBConnUtil
    {
        // Static method to get the connection string from appsettings.json
        public static string GetConnectionString(string configFileName)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Get the current directory
                .AddJsonFile(configFileName) // Add the JSON file (appsettings.json)
                .Build();

            // Fetch the connection string from the configuration
            return configuration.GetConnectionString("LocalConnectionString");
        }

        // Static method to establish a connection to the database
        public static SqlConnection GetConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connected to the database successfully!");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Database connection failed: " + e.Message);
            }

            return connection;
        }
    }
}
