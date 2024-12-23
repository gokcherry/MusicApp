using Npgsql;
using System;
using System.Data;

namespace MusicApp
{
    public class DatabaseHelper
    {
        private static readonly string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=Gokces;Database=postgres";
        public static NpgsqlConnection GetConnection()
        {
            try
            {
                var connection = new NpgsqlConnection(ConnectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Error connecting to the database. Details: " + ex.Message);
            }
        }
        public static DataTable ExecuteQuery(string query)
        {
            try
            {
                Console.WriteLine("Executing Query: " + query); // Log the query
                using (var connection = GetConnection())
                using (var command = new NpgsqlCommand(query, connection))
                {
                    var dataTable = new DataTable();
                    using (var adapter = new NpgsqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                    return dataTable;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing query. Details: " + ex.Message);
            }
        }

        public static void ExecuteNonQuery(string query)
        {
            try
            {
                using (var connection = GetConnection())
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing non-query. Details: " + ex.Message);
            }
        }
    }
}
