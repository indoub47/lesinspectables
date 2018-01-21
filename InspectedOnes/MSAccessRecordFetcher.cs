using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InspectedOnes.Properties;

namespace InspectedOnes
{
    public class MSAccessRecordFetcher : InspectedOnes.IRecordFetcher
    {
        string connectionString;
        string sql;
        int cols;

        public MSAccessRecordFetcher(string connectionString)
        {
            this.connectionString = connectionString;
            sql = Settings.Default.Sql;
            cols = Settings.Default.Cols;
        }

        public List<object[]> Fetch(DateTime fromDate, DateTime toDate)
        {
            List<object[]> recAccum = new List<object[]>();
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = string.Format(sql, fromDate, toDate);

            object[] meta = new object[cols];
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
                            reader.GetValues(meta);
                            recAccum.Add((object[])(meta.Clone()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                return recAccum;
            }
        }
    }
}
