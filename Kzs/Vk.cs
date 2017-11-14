using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    [Serializable()]
    public class Vk
    {
        public string Linija { get; set; }
        public int Kelias { get; set; }
        public int Km { get; set; }
        public int Pk { get; set; }
        public int M { get; set; }
        public int? Siule { get; set; }
        public int? Iesmas { get; }
        public int? Suvirinimas { get; }

        public bool Pagrindinis { get; }
        public bool OnIesmas { get; }

        public Vk() { }

        public Vk(long id, string linija, int kelias, int km, int pk, int m, int? siule)
        {
            if (kelias == 8 && (pk != 0 || m < 1 || siule != null))
            {
                throw new Exception($"Blogas vietos kodas: linija {linija}, kelias {kelias}, km {km}, pk {pk}, m {m}, siūlė {siule}");
            }

            if (kelias == 9 && (pk < 1 || m < 1 || siule != null))
            {
                throw new Exception($"Blogas vietos kodas: linija {linija}, kelias {kelias}, km {km}, pk {pk}, m {m}, siūlė {siule}");
            }

            if (kelias != 9 && kelias != 8 && (pk < 1 || m < 0 || siule == null))
            {
                throw new Exception($"Blogas vietos kodas: linija {linija}, kelias {kelias}, km {km}, pk {pk}, m {m}, siūlė {siule}");
            }

            Linija = linija;
            Kelias = kelias;
            Km = km;
            Pk = pk;
            M = m;
            Siule = siule;
            if (kelias == 8)
            {
                Iesmas = km;
                Suvirinimas = m;
            }
            else if (kelias == 9)
            {
                Iesmas = pk;
                Suvirinimas = m;
            }
            else
            {
                Iesmas = null;
                Suvirinimas = null;
            }

            OnIesmas = Iesmas != null;
            Pagrindinis = Kelias == 1 || Kelias == 2;
        }        

        public override string ToString() {

            string mainPart = string.Format("{0}.{1}{2:000}.{3:#00}.{4:00}", Linija, Kelias, Km, Pk, M);
            string siulePart = string.Empty;
            if (Siule != null)
            {
                siulePart = $".{Siule}";
            }
            return mainPart + siulePart;
        }
    }
}
