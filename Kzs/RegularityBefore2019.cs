﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    public class RegularityBefore2019 : IRegularity
    {
        private const int
            monthsFrom1 = 0,
            daysFrom2 = 30,
            monthsFrom3 = 8,
            monthsFrom4 = 24,
            monthsTo1 = 0,
            daysTo2 = 40,
            monthsTo3 = 16,
            monthsTo4 = 30;
        

        private DateTime dateX_I(DateTime date)
        {
            return date.AddMonths(-monthsFrom1);
        }

        private DateTime firstDateI(DateTime suvirDate)
        {
            // suvirDate -  suvirinimo arba eismo atidarymo data
            return suvirDate.AddMonths(monthsFrom1);
        }

        private DateTime lastDateI(DateTime suvirDate)
        {
            // suvirDate -  suvirinimo arba eismo atidarymo data
            return suvirDate.AddMonths(monthsTo1);
        }




        private DateTime dateX_II(DateTime date)
        {
            return date.AddDays(-daysFrom2);
        }

        private DateTime firstDateII(DateTime date1)
        {
            // startDate - arba suvirinimo data,
            // arba eismo atidarymo data(gamykloje suvirintiems ek)
            return date1.AddMonths(daysFrom2);
        }

        private DateTime lastDateII(DateTime date1)
        {
            // startDate - arba suvirinimo data,
            // arba eismo atidarymo data(gamykloje suvirintiems ek)
            return date1.AddDays(daysTo2);
        }



        private DateTime dateX_III(DateTime date)
        {
            return date.AddMonths(-monthsFrom3);
        }

        private DateTime firstDateIII(DateTime date1)
        {
            return date1.AddMonths(monthsFrom3);
        }

        private DateTime lastDateIII(DateTime date1)
        {
            return date1.AddMonths(monthsTo3);
        }



        private DateTime dateX_IV(DateTime date)
        {
            return date.AddMonths(-monthsFrom4);
        }

        private DateTime firstDateIV(DateTime date1)
        {
            return date1.AddMonths(monthsFrom4);
        }

        private DateTime lastDateIV(DateTime date1)
        {
            return date1.AddMonths(monthsTo4);
        }


        public DateTime GetDateX(DateTime date, Kelintas whichToPerform)
        {
            switch (whichToPerform)
            {
                case Kelintas.II:
                    return dateX_II(date);
                case Kelintas.III:
                    return dateX_III(date);
                case Kelintas.IV:
                    return dateX_IV(date);
                case Kelintas.I:
                    return dateX_I(date);
                default:
                    throw new Exception("Nesuprantama, kelintas čia tikrinimas: " + whichToPerform.ToString());
            }
        }

        public int GetDaysLeft(DateTime baseDate, Kelintas whichToPerform, DateTime toDate)
        {
            switch (whichToPerform)
            {
                case Kelintas.II:
                    return (lastDateII(baseDate) - toDate).Days;
                case Kelintas.III:
                    return (lastDateIII(baseDate) - toDate).Days;
                case Kelintas.IV:
                    return (lastDateIV(baseDate) - toDate).Days;
                case Kelintas.I:
                    return (lastDateI(baseDate) - toDate).Days;
                default:
                    throw new Exception("Nesuprantama, kelintas čia tikrinimas: " + whichToPerform.ToString());
            }
        }

        public DateTime GetDateFrom(DateTime baseDate, Kelintas whichToPerform)
        {
            switch (whichToPerform)
            {
                case Kelintas.II:
                    return firstDateII(baseDate);
                case Kelintas.III:
                    return firstDateIII(baseDate);
                case Kelintas.IV:
                    return firstDateIV(baseDate);
                case Kelintas.I:
                    return firstDateI(baseDate);
                default:
                    throw new Exception("Nesuprantama, kelintas čia tikrinimas: " + whichToPerform.ToString());
            }
        }

        public DateTime GetDateTo(DateTime baseDate, Kelintas whichToPerform)
        {
            switch (whichToPerform)
            {
                case Kelintas.II:
                    return lastDateII(baseDate);
                case Kelintas.III:
                    return lastDateIII(baseDate);
                case Kelintas.IV:
                    return lastDateIV(baseDate);
                case Kelintas.I:
                    return lastDateI(baseDate);
                default:
                    throw new Exception("Nesuprantama, kelintas čia tikrinimas: " + whichToPerform.ToString());
            }
        }

        public string[] GetSQLTails()
        {
            return new string[] {
                ", Pak_suv_data AS atskaitosData, 1 AS kelintas FROM ssd WHERE ([saliginis kodas] IN (\"06.3\", \"06.4\")) AND (I_pat_data IS NULL) AND (Pak_suv_data <= #{0:yyyy-MM-dd}#)",
                ", I_pat_data AS atskaitosData, 2 AS kelintas FROM ssd WHERE ([saliginis kodas] IN(\"06.3\", \"06.4\")) AND (I_pat_data IS NOT NULL) AND (II_pat_data IS NULL) AND (I_pat_data <= #{0:yyyy-MM-dd}#)",
                ", I_pat_data AS atskaitosData, 3 AS kelintas FROM ssd WHERE ([saliginis kodas] IN(\"06.3\", \"06.4\")) AND (II_pat_data IS NOT NULL) AND (III_pat_data IS NULL) AND (I_pat_data <= #{0:yyyy-MM-dd}#)",
                ", I_pat_data AS atskaitosData, 4 AS kelintas FROM ssd WHERE ([saliginis kodas] IN(\"06.3\", \"06.4\")) AND (III_pat_data IS NOT NULL) AND (IV_pat_data IS NULL) AND (I_pat_data <= #{0:yyyy-MM-dd}#)"
            };
        }

        public int GetMaxTerm()
        {
            return 183;
        }
    }
}
