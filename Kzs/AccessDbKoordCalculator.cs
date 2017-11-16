using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Kzs
{
    public class AccessDbKoordCalculator : IKoordCalculator
    {
        private class Insp
        {
            internal ulong id;
            internal string linija;
            internal int km;
            internal int pk;
            internal int m;
        }

        private class Stotis
        {
            internal string stotis;
            internal string linija;
            internal int km;
            internal int pk;
            internal int m;
        }

        private class Iesmas
        {
            internal int nr;
            internal string stotis;
            internal string linija;
            internal int km;
            internal int pk;
            internal int m;
        }

        private List<Insp> inspai;
        private List<Stotis> stotys;
        private List<Iesmas> iesmai;

        private string connectionString;

        public AccessDbKoordCalculator(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Charge()
        {
            inspai = new List<Insp>();
            stotys = new List<Stotis>();
            iesmai = new List<Iesmas>();
            // connect to db;
            // fill containers;
            // close db;

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string stmStotys = Properties.Settings.Default.StmStotys;
                string stmIesmai = Properties.Settings.Default.StmIesmai;
                string stmInspai = Properties.Settings.Default.StmInsps;
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                conn.Open();

                cmd.CommandText = stmInspai;
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        inspai.Add(readerToInsp(reader));
                    }
                }

                cmd.CommandText = stmStotys;
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stotys.Add(readerToStotys(reader));
                    }
                }

                cmd.CommandText = stmIesmai;
                using (OleDbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        iesmai.Add(readerToIesmai(reader));
                    }
                }
            }
        }

        private Iesmas readerToIesmai(OleDbDataReader reader)
        {
            // SELECT nr, stotis, linija, km, pk, m FROM Iesmai
            return new Iesmas
            {
                nr = Convert.ToInt32(reader["nr"]),
                stotis = reader["stotis"] == null ? string.Empty : reader["stotis"].ToString(),
                linija = reader["linija"].ToString(),
                km = Convert.ToInt32(reader["km"]),
                pk = Convert.ToInt32(reader["pk"]),
                m = Convert.ToInt32(reader["m"])
            };
        }

        private Stotis readerToStotys(OleDbDataReader reader)
        {
            // SELECT stotis, linija, km, pk, m FROM Stotys
            return new Stotis
            {
                stotis = reader["stotis"].ToString(),
                linija = reader["linija"].ToString(),
                km = Convert.ToInt32(reader["km"]),
                pk = Convert.ToInt32(reader["pk"]),
                m = Convert.ToInt32(reader["m"])
            };
        }

        private Insp readerToInsp(OleDbDataReader reader)
        {
            // SELECT id, linija, km, pk, m FROM Insps
            return new Insp
            {
                id = Convert.ToUInt64(reader["id"]),
                linija = reader["linija"].ToString(),
                km = Convert.ToInt32(reader["km"]),
                pk = Convert.ToInt32(reader["pk"]),
                m = Convert.ToInt32(reader["m"])
            };
        }

        public Tuple<string, int> Calculate(ulong id, Vk vk)
        {
            Tuple<string, int> result = new Tuple<string, int>("", -1);

            // lookup inspai
            result = lookupInsps(id, vk);
            if (result.Item2 >= 0) return result;

            // lookup iesmai
            if (vk.OnIesmas)
            {
                result = lookupIesmai(id, vk);
                if (result.Item2 >= 0) return result;
            }

            // lookup stotys
            result = lookupStotys(id, vk);
                if (result.Item2 >= 0) return result;

            result = getPagrindinisKoord(id, vk);
            return result;
        }

        private Tuple<string, int> getPagrindinisKoord(ulong id, Vk vk)
        {
            return new Tuple<string, int>(
                vk.Linija,
                coord(vk.Km, vk.Pk, vk.M));
        }

        private Tuple<string, int> lookupStotys(ulong id, Vk vk)
        {
            Stotis stotis = stotys.Find(x => x.stotis == vk.Linija);
            if (stotis == null)
            {
                return new Tuple<string, int>("", -1);
            }

            return new Tuple<string, int>(
                stotis.linija, 
                coord(stotis.km, stotis.pk, stotis.m));
        }

        private Tuple<string, int> lookupIesmai(ulong id, Vk vk)
        {
            // ieško didelėje stotyje, kur nurodytas stoties kodas
            Iesmas iesm = iesmai.Find(x => x.nr == vk.Iesmas && x.stotis == vk.Linija);

            // jeigu didelėje stotyje nerasta,
            if (iesm == null)
            {
                // iesko mazoje stotyje, kur nurodyta linija ir km
                iesm = iesmai.Find(x => x.stotis == string.Empty && x.nr == vk.Iesmas && x.linija == vk.Linija && x.km == vk.Km);
            }

            if (iesm == null)
            {
                return new Tuple<string, int>("", -1);
            }

            return new Tuple<string, int> (
                iesm.linija, 
                coord(iesm.km, iesm.pk, iesm.m));
        }

        private Tuple<string, int> lookupInsps(ulong id, Vk vk)
        {
            Insp insp = inspai.Find(x => x.id == id);
            if (insp == null)
            {
                return new Tuple<string, int>("", -1);
            }

            return new Tuple<string, int>(
                insp.linija, 
                coord(insp.km, insp.pk, insp.m));
        }

        private int coord(int km, int pk, int m)
        {
            return km * 1000 + (pk - 1) * 100 + m;
        }
    }

    
}
