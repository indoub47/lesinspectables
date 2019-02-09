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
        // turi partempti Inpsectables:
        // 1. įrašai, kurių skodas yra 06.3 arba 06.4
        // 2. įrašai, kuriems neatliktas tikrinimas
        // 3. įrašai, kuriuos galima tikrinti


        private string connectionString;
        private List<string> sqls;
        private int cols;
        private DateCalculator dateCalc;

        public RecordFetcher(string connectionString)
        {
            this.connectionString = connectionString;
            cols = Settings.Default.Cols;
            dateCalc = new DateCalculator();
        }

        public List<object[]> Fetch(DateTime forDate)
        {
            sqls = new List<string>
            {
                Settings.Default.Stm1,
                Settings.Default.Stm2,
                Settings.Default.Stm3,
                Settings.Default.Stm4,
            };

            sqls[0] = String.Format(sqls[0], dateCalc.DateMinusMinI(forDate));
            sqls[1] = String.Format(sqls[1], dateCalc.DateMinusMinII(forDate));
            sqls[2] = String.Format(sqls[2], dateCalc.DateMinusMinIII(forDate));
            sqls[3] = String.Format(sqls[3], dateCalc.DateMinusMinIV(forDate));

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
                        //Console.WriteLine(sql);
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
