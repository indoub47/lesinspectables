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
            /*
             * šitie iškomentuoti, nes IF3 rašo iešmų kodą su siūle
            if (kelias == 8 && siule != null)
            {
                throw new Exception($"Įtartinas vietos kodas id {id} nebus įtrauktas: {linija}.{kelias}{km:000}.{pk:#00}.{m:#00}.{siule} - neturėtų būti siūlės");
            }

            if (kelias == 9 && siule != null)
            {
                throw new Exception($"Įtartinas vietos kodas id {id} nebus įtrauktas: {linija}.{kelias}{km:000}.{pk:#00}.{m:#00}.{siule} - neturėtų būti siūlės");
            }
            */

            /*
             * šitą iškomentavau, nes tokiu atveju reikėtų tikrinti viską, ne vien siūlės nebuvimą.
            if (kelias != 9 && kelias != 8 && siule == null)
            {
                throw new Exception($"Blogas vietos kodas id {id} nebus įtrauktas: {linija}.{kelias}{km:000}.{pk:#00}.{m:#00}.{siule} - nėra siūlės");
            }
            */

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
