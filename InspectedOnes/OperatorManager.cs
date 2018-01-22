using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectedOnes
{
    public class OperatorManager
    {
        string connectionString;

        public OperatorManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public Dictionary<string, string> GetOperators()
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = Properties.Settings.Default.GetOperatorsSql;
            Dictionary<string, string> operators = new Dictionary<string, string>();
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                cmd.Connection = conn;
                try
                {
                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            operators[reader["id"].ToString()] = reader["vardas"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                return operators;
            }
        }
    }
}
