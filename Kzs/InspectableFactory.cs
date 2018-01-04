using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    public class InspectableFactory
    {
        private string[] mapping;
        private IVkodasFactory vkodasFactory;
        private IKoordCalculator koordCalculator;
        private DateTime toDate;
        private DateCalculator dateCalc;

        public InspectableFactory(DateTime toDate, IVkodasFactory vkodasFactory, IKoordCalculator koordCalculator, string[] mapping)
        {
            this.mapping = mapping;
            this.vkodasFactory = vkodasFactory;
            this.koordCalculator = koordCalculator;
            this.dateCalc = new DateCalculator();

            this.toDate = toDate;
        }

        public Inspectable Make(object[] rec)
        {
            ulong id = Convert.ToUInt64(rec[Array.IndexOf(mapping, "id")]);
            Vk vk = vkodasFactory.Make(rec);
            Tuple<string, int> linKoord = koordCalculator.Calculate(id, vk);
            if (linKoord.Item2 < 0)
            {
                return null;
            }
            string skodas = Convert.ToString(rec[Array.IndexOf(mapping, "skodas")]);
            DateTime suvirData = Convert.ToDateTime(rec[Array.IndexOf(mapping, "suvdata")]);
            Kelintas kelintasNeatliktas = (Kelintas)rec[9];
            int likoDienu = getLikoDienu(suvirData, kelintasNeatliktas);
            Inspectable insp = new Inspectable(id, linKoord.Item1, vk, linKoord.Item2, skodas, kelintasNeatliktas, likoDienu);
            return insp;
        }

        public void ChangeDate(DateTime date)
        {
            toDate = date;
        }

        private int getLikoDienu(DateTime suvirData, Kelintas neatliktas)
        {
            switch(neatliktas)
            {
                case Kelintas.II:
                    return (dateCalc.LastDateII(suvirData) - toDate).Days;
                case Kelintas.III:
                    return (dateCalc.LastDateIII(suvirData) - toDate).Days;
                case Kelintas.IV:
                    return (dateCalc.LastDateIV(suvirData) - toDate).Days;
                case Kelintas.I:
                    return (dateCalc.LastDateI(suvirData) - toDate).Days;
                default:
                    return 0;
            }
        }
    }
}
