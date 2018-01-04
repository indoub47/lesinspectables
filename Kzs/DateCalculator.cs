using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    public class DateCalculator
    {
        private int daysFrom1, daysFrom2, daysFrom3, daysFrom4, daysTo1, daysTo2, daysTo3, daysTo4;

        public DateCalculator()
        {
            daysFrom1 = Properties.Settings.Default.DaysFrom1;
            daysFrom2 = Properties.Settings.Default.DaysFrom2;
            daysFrom3 = Properties.Settings.Default.DaysFrom3;
            daysFrom4 = Properties.Settings.Default.DaysFrom4;
            daysTo1 = Properties.Settings.Default.DaysTo1;
            daysTo2 = Properties.Settings.Default.DaysTo2;
            daysTo3 = Properties.Settings.Default.DaysTo3;
            daysTo4 = Properties.Settings.Default.DaysTo4;
        }

        public DateTime LastDateI(DateTime suvirDate)
        {
            return suvirDate.AddDays(daysTo1);
        }

        public DateTime LastDateII(DateTime suvirDate)
        {
            return suvirDate.AddDays(daysTo2);
        }

        public DateTime LastDateIII(DateTime suvirDate)
        {
            return suvirDate.AddDays(daysTo3);
        }

        public DateTime LastDateIV(DateTime suvirDate)
        {
            return suvirDate.AddDays(daysTo4);
        }

        public DateTime DateMinusMinI(DateTime date)
        {
            return date.AddDays(-daysFrom1);
        }

        public DateTime DateMinusMinII(DateTime date)
        {
            return date.AddDays(-daysFrom2);
        }

        public DateTime DateMinusMinIII(DateTime date)
        {
            return date.AddDays(-daysFrom3);
        }

        public DateTime DateMinusMinIV(DateTime date)
        {
            return date.AddDays(-daysFrom4);
        }
    }
}
