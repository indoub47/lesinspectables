using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kzs.Properties;

namespace Kzs
{
    public class RecordFetcher: IRecordFetcher
    {
        private readonly string connectionString;
        private readonly int cols;
        private IRegularity regularity;

        public RecordFetcher(string connectionString, IRegularity regularity)
        {
            this.connectionString = connectionString;
            cols = Settings.Default.Cols;
            this.regularity = regularity;
        }

        public void SetRegularity(IRegularity regularity)
        {
            this.regularity = regularity;
        }

        public List<object[]> Fetch(DateTime forDate)
        {
            string[] sqlTails = regularity.GetSQLTails();
            string sqlHead = Settings.Default.FetchInspSqlHead;

            string[] sqls = new string[]
            {
                String.Format(sqlHead + sqlTails[0], regularity.GetDateX(forDate, Kelintas.I)),
                String.Format(sqlHead + sqlTails[1], regularity.GetDateX(forDate, Kelintas.II)),
                String.Format(sqlHead + sqlTails[2], regularity.GetDateX(forDate, Kelintas.III)),
                String.Format(sqlHead + sqlTails[3], regularity.GetDateX(forDate, Kelintas.IV))
            };

            List<object[]> recAccum = new List<object[]>();
            OleDbCommand cmd = new OleDbCommand();

            object[] meta = new object[cols];
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                cmd.Connection = conn;
                try
                {
                    conn.Open();
                    foreach (var sql in sqls)
                    {
                        cmd.CommandText = sql;
                        // Console.WriteLine(sql);
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reader.GetValues(meta);
                                recAccum.Add((object[])(meta.Clone()));
                            }
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
