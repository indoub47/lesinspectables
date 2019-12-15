using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace InspectedOnes
{
    public class MSAccessBadRecordFinder : IBadRecordFinder
    {
        readonly string connectionString;
        public MSAccessBadRecordFinder(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<ulong> FindBadRecords(DateTime dateFrom, DateTime dateTo)
        {
            OleDbCommand cmd = new OleDbCommand
            {
                CommandText = string.Format(Properties.Settings.Default.BadDataSql, dateFrom, dateTo)
            };

            List<ulong> result = new List<ulong>();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                cmd.Connection = conn;
                conn.Open();
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(Convert.ToUInt64(reader[0]));
                    }
                }
            }
            return result;
        }
    }
}
