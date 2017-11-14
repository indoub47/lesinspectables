using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    class Program
    {
        static void Main(string[] args)
        {
            IRecordFetcher rf = new RecordFetcher();
            IDangerCalculator dc = new DangerCalculator();
            string[] mapping = Properties.Settings.Default.Mapping;
            IVkodasFactory vfac = new VkodasFactory(mapping);
            IKoordCalculator koordCalc = new AccessDbKoordCalculator();
            koordCalc.Charge();
            InspectableFactory ifac = new InspectableFactory(DateTime.Now, vfac, koordCalc, mapping);
            Grouper gr = new Grouper();
            List<object[]> result = rf.Fetch(DateTime.Now);

            if (result == null)
            {
                Console.WriteLine("Klaida");
                return;
            }

            List<Inspectable> insps = new List<Inspectable>();

            foreach(var obj in result)
            {
                Inspectable insp;
                try
                {
                    insp = ifac.Make(obj);
                    insps.Add(insp);
                }
                catch
                {
                    continue;
                }
            }

            dc.BatchCalculate(insps);

            var data = gr.Group(insps);

            foreach (Grouper.Grouped grouped in data)
            {
                Console.WriteLine(grouped.Linija);
                foreach (Grouper.Grouped.KmInsps kmInsps in grouped.Kms)
                {
                    Console.WriteLine($"km: {kmInsps.Km}, count: {kmInsps.Insps.Count()}, danger: {kmInsps.KmDanger}");
                }
            }

            Console.ReadKey();
        }
    }
}
