﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    public class Grouper
    {
        public class Grouped
        {
            public class KmInsps
            {
                public class PaintOptions
                {
                    public int Overdued { get; set; } // reikalingas, kad pakeisti spalvą, kai pieš
                    public float X { get; set; } // stulpelio koordinatė ekrane, suteiks; kai pieš
                    public float Y0 { get; set; } // stulpelio apačios koordinatė ekrane, suteiks kai pieš
                    public float Y1 { get; set; } // stulpelio viršaus koordinatė ekrane; suteiks, kai pieš
                    public bool Selected { get; set; } // ar stulpelis yra parinktas; suteiks, kai parinks
                }
                public int Km { get; set; }
                public IEnumerable<Inspectable> Insps { get; set; }
                public int KmDanger { get; set; }
                public PaintOptions POptions { get; set; }

                // grąžina tuščia kilometrą 
                public static KmInsps GetEmpty(int km)
                {
                    return new KmInsps
                    {
                        Km = km,
                        Insps = new List<Inspectable>(),
                        KmDanger = 0,
                        POptions = new PaintOptions()
                    };
                }
            }

            public string Linija { get; set; }
            public IEnumerable<KmInsps> Kms { get; set; }

            // Papildo liniją trūkstamais tuščiais kilometrais
            public void Complement(int kmFrom, int kmTo)
            {
                List<KmInsps> kmsList = Kms.ToList();
                for (int km = kmFrom; km <= kmTo; km++)
                {
                    if (!kmsList.Any(x => x.Km == km))
                    {
                        kmsList.Add(KmInsps.GetEmpty(km));
                    }
                }               
                Kms = kmsList.OrderBy(x => x.Km);
            }
        }

        // kiekvienos linijos pradinis ir pabaigos kilometrai:
        // lineKms["01"] = Tuple<231, 377>
        // lineKms["17"] = Tuple<53, 148>
        // ...
        private Dictionary<string, Tuple<int, int>> lineKms;
        private List<Predicate<Inspectable>> filterMethods;

        public Grouper()
        {
            setLineKms();
            filterMethods = new List<Predicate<Inspectable>>();
        }

        public void ClearFilterMethods()
        {
            filterMethods.Clear();
        }


        // Ima iš Settings LineKms string, išskaido į dalis ir suformuoja
        // Dictionary<string, Tuple<int, int>>
        private void setLineKms()
        {
            lineKms = new Dictionary<string, Tuple<int, int>>();
            string strLineKms = Properties.Settings.Default.LineKms;
            string[] lines = strLineKms.Split(new string[] { ";;" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                string[] values = line.Split(';');
                lineKms[values[0]] = new Tuple<int, int>(Convert.ToInt32(values[1]), Convert.ToInt32(values[2]));
            }
        }

        public List<Grouped> Group(IEnumerable<Inspectable> insps)
        {
            // sugrupuoja inspectables į kilometrus
            // sugrupuoja kilometrus į linijas
            var grouped = insps.Where(And(filterMethods)).OrderBy(x => x.Linija).GroupBy(x1 => x1.Linija, (key, group) => new Grouped
            {
                Linija = key,
                Kms = group.GroupBy(x2 => (int)x2.Koord / 1000, (key1, group1) => new Grouped.KmInsps
                {
                    Km = key1,
                    Insps = group1.OrderBy(x3 => x3.Koord),
                    KmDanger = group1.Sum(x4 => x4.Danger),
                    POptions = new Grouped.KmInsps.PaintOptions
                    {
                        X = 0,
                        Y0 = 0,
                        Y1 = 0,
                        Selected = false,
                        Overdued = group1.All(x5 => x5.Liko < 0) ? 1 : (group1.Any(x5 => x5.Liko < 0) ? 0 : -1)
                    }
                }).OrderBy(x6 => x6.Km)
            });

            List<Grouped>groupedx = grouped.ToList();

            // kiekvieną liniją papildo trūkstamais tuščiais kilometrais
            foreach (var lin in groupedx)
            {
                if (!lineKms.ContainsKey(lin.Linija))
                {
                    // TODO: kažką daryti, jeigu nėra tokios linijos linijų sąraše
                    throw new Exception($"Yra suvirinimų su nesuprantama linija {lin.Linija}");
                }

                lin.Complement(lineKms[lin.Linija].Item1, lineKms[lin.Linija].Item2);
            }

            return groupedx;
        }

        public Func<Inspectable, bool> And(List<Predicate<Inspectable>> predicates)
        {
            return delegate (Inspectable insp)
            {
                if (predicates == null || predicates.Count() == 0)
                {
                    return true;
                }

                foreach (Predicate<Inspectable> predicate in predicates)
                {
                    if (!predicate(insp))
                    {
                        return false;
                    }
                }
                return true;
            };
        }

        public Func<Inspectable, bool> Or(List<Predicate<Inspectable>> predicates)
        {
            return delegate (Inspectable insp)
            {
                if (predicates == null || predicates.Count() == 0)
                {
                    return true;
                }

                foreach (Predicate<Inspectable> predicate in predicates)
                {
                    if (predicate(insp))
                    {
                        return true;
                    }
                }
                return false;
            };
        }

        private bool getAll(Inspectable insp)
        {
            return true;
        }

        public Predicate<Inspectable> FilterByLikoMaziau(int dienu)
        {
            return insp => insp.Liko < dienu;
        }

        public Predicate<Inspectable> DiscardBySkodas(string skodas)
        {
            return insp => insp.Skodas != skodas;
        }

        public Predicate<Inspectable> DiscardByLinija(string linija)
        {
            return insp => insp.Linija != linija;
        }

        public Predicate<Inspectable> DiscardNepagr()
        {
            return insp => insp.Vkodas.Pagrindinis;
        }

        public void AddFilterMethod(Predicate<Inspectable> method)
        {
            filterMethods.Add(method);
        }
    }
}
