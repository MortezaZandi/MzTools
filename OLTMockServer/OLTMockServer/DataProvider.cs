using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer
{
    public class DataProvider
    {
        private static DataProvider instance;
        private readonly string connectionString;
        private const int DefaultCommandTimeout = 30;

        public DataProvider(string connectionString)
        {
            this.connectionString = connectionString;
            instance = this;
        }

        public static DataProvider Instance
        {
            get
            {
                return instance;
            }
        }

        public bool TestConnection(out string message)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                try
                {
                    connection.Open();
                    message = null;
                    return true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    return false;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                    {
                        connection.Clone();
                    }
                }
            }
        }

        public int GetCount(string query, int timeout = DefaultCommandTimeout)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = timeout;

                    try
                    {
                        connection.Open();

                        return (int)command.ExecuteScalar();
                    }
                    finally
                    {
                        if (connection.State != ConnectionState.Closed)
                        {
                            connection.Clone();
                        }
                    }
                }
            }
        }

        public void Execute(string query)
        {
            ExecuteReader(query);
        }

        public SqlDataReader ExecuteReader(string query, int timeout = DefaultCommandTimeout)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = timeout;

                    try
                    {
                        connection.Open();

                        return command.ExecuteReader();
                    }
                    finally
                    {
                        if (connection.State != ConnectionState.Closed)
                        {
                            connection.Clone();
                        }
                    }
                }
            }
        }

        public DataTable ReadData(string query)
        {
            return ReadData(query, DefaultCommandTimeout).Tables[0];
        }

        public DataSet ReadData(string query, int timeout = DefaultCommandTimeout)
        {
            using (SqlConnection connection = new SqlConnection(this.connectionString))
            {
                var dataset = new DataSet();
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.CommandTimeout = timeout;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {

                        try
                        {
                            connection.Open();

                            adapter.Fill(dataset);

                            return dataset;
                        }
                        finally
                        {
                            if (connection.State != ConnectionState.Closed)
                            {
                                connection.Clone();
                            }
                        }
                    }
                }
            }
        }
    }
}
