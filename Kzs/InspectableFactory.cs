using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    public class InspectableFactory
    {
        private readonly string[] mapping;
        private IVkodasFactory vkodasFactory;
        private IKoordCalculator koordCalculator;
        private DateTime toDate;
        private IRegularity regularity;

        public InspectableFactory(DateTime toDate, IVkodasFactory vkodasFactory, IKoordCalculator koordCalculator, string[] mapping, IRegularity regularity)
        {
            this.mapping = mapping;
            this.vkodasFactory = vkodasFactory;
            this.koordCalculator = koordCalculator;
            this.regularity = regularity;

            this.toDate = toDate;
        }

        public void SetRegularity(IRegularity regularity)
        {
            this.regularity = regularity;
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
            DateTime baseDate = Convert.ToDateTime(rec[Array.IndexOf(mapping, "atskaitosData")]);
            Kelintas whichToPerform = (Kelintas)rec[Array.IndexOf(mapping, "kelintas")];
            int likoDienu = regularity.GetDaysLeft(baseDate, whichToPerform, toDate);
            DateTime dataNuo = regularity.GetDateFrom(baseDate, whichToPerform);
            DateTime dataIki = regularity.GetDateTo(baseDate, whichToPerform);
            int weeksAway = WeekCalculator.GetWeeksAwayCount(toDate, dataIki);
            Inspectable insp = new Inspectable(id, linKoord.Item1, vk, linKoord.Item2, skodas, whichToPerform, likoDienu, dataNuo, dataIki, weeksAway);
            return insp;
        }

        public void ChangeDate(DateTime date)
        {
            toDate = date;
        }
    }
}
