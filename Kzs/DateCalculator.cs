using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    public class DateCalculator
    {
        private const int 
            monthsFrom1 = 0, 
            monthsFrom2 = 1, 
            monthsFrom3 = 8, 
            monthsFrom4 = 24, 
            monthsTo1 = 0, 
            monthsTo2 = 2, 
            monthsTo3 = 16, 
            monthsTo4 = 36;

        private DateTime dateMinusMinI(DateTime date)
        {
            return date.AddMonths(-monthsFrom1); ;
        }

        private DateTime dateMinusMinII(DateTime date)
        {
            return date.AddMonths(-monthsFrom2);
        }

        private DateTime dateMinusMinIII(DateTime date)
        {
            return date.AddMonths(-monthsFrom3);
        }

        private DateTime dateMinusMinIV(DateTime date)
        {
            return date.AddMonths(-monthsFrom4);
        }
    
        /*
         * suvirDate suvirinimo arba eismo atidarymo data
         */
        private DateTime lastDateI(DateTime suvirDate)
        {
            return suvirDate.AddMonths(monthsTo1);
        }

        /*
         * startDate - arba suvirinimo data,
         * arba eismo atidarymo data (gamykloje suvirintiems ek)
         */
        private DateTime lastDateII(DateTime startDate)
        {
            return startDate.AddMonths(monthsTo2);
        }

        private DateTime lastDateIII(DateTime date2)
        {
            return date2.AddMonths(monthsTo3);
        }

        private DateTime lastDateIV(DateTime date2)
        {
            return date2.AddMonths(monthsTo4);
        }

        /*
         * suvirDate - suvirinimo arba eismo atidarymo data
         */
        private DateTime firstDateI(DateTime suvirDate)
        {
            return suvirDate.AddMonths(monthsFrom1);
        }

        /*
         * startDate - arba suvirinimo data,
         * arba eismo atidarymo data (gamykloje suvirintiems ek)
         */
        private DateTime firstDateII(DateTime startDate)
        {
            return startDate.AddMonths(monthsFrom2);
        }

        private DateTime firstDateIII(DateTime date2)
        {
            return date2.AddMonths(monthsFrom3);
        }

        private DateTime firstDateIV(DateTime date2)
        {
            return date2.AddMonths(monthsFrom4);
        }


        public DateTime DateMinusMin(DateTime date, Kelintas kelintas)
        {
            switch (kelintas)
            {
                case Kelintas.II:
                    return dateMinusMinII(date);
                case Kelintas.III:
                    return dateMinusMinIII(date);
                case Kelintas.IV:
                    return dateMinusMinIV(date);
                case Kelintas.I:
                    return dateMinusMinI(date);
                default:
                    throw new Exception("Nesuprantama, kelintas čia tikrinimas: " + kelintas.ToString());
            }
        }
               
        public int GetLikoDienu(DateTime atskaitosData, Kelintas kelintasNeatliktas, DateTime toDate)
        {
            switch (kelintasNeatliktas)
            {
                case Kelintas.II:
                    return (lastDateII(atskaitosData) - toDate).Days;
                case Kelintas.III:
                    return (lastDateIII(atskaitosData) - toDate).Days;
                case Kelintas.IV:
                    return (lastDateIV(atskaitosData) - toDate).Days;
                case Kelintas.I:
                    return (lastDateI(atskaitosData) - toDate).Days;
                default:
                    throw new Exception("Nesuprantama, kelintas čia tikrinimas: " + kelintasNeatliktas.ToString());
            }
        }

        public DateTime GetDataNuo(DateTime atskaitosData, Kelintas kelintasNeatliktas)
        {
            switch (kelintasNeatliktas)
            {
                case Kelintas.II:
                    return firstDateII(atskaitosData);
                case Kelintas.III:
                    return firstDateIII(atskaitosData);
                case Kelintas.IV:
                    return firstDateIV(atskaitosData);
                case Kelintas.I:
                    return firstDateI(atskaitosData);
                default:
                    throw new Exception("Nesuprantama, kelintas čia tikrinimas: " + kelintasNeatliktas.ToString());
            }
        }

        public DateTime GetDataIki(DateTime atskaitosData, Kelintas kelintasNeatliktas)
        {
            switch (kelintasNeatliktas)
            {
                case Kelintas.II:
                    return lastDateII(atskaitosData);
                case Kelintas.III:
                    return lastDateIII(atskaitosData);
                case Kelintas.IV:
                    return lastDateIV(atskaitosData);
                case Kelintas.I:
                    return lastDateI(atskaitosData);
                default:
                    throw new Exception("Nesuprantama, kelintas čia tikrinimas: " + kelintasNeatliktas.ToString());
            }
        }

    }
}
