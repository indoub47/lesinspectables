using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    public static class WeekCalculator
    {
        private static DateTime getNextWeekday(DateTime start, DayOfWeek day)
        {
            //int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            //return start.AddDays(daysToAdd);
            switch(start.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return start.AddDays(5);
                case DayOfWeek.Tuesday:
                    return start.AddDays(4);
                case DayOfWeek.Wednesday:
                    return start.AddDays(3);
                case DayOfWeek.Thursday:
                    return start.AddDays(2);
                case DayOfWeek.Friday:
                    return start.AddDays(1);
                case DayOfWeek.Saturday:
                    return start.AddDays(7);
                case DayOfWeek.Sunday:
                    return start.AddDays(6);
                default:
                    return start;
            }
        }

        public static int GetWeeksAwayCount(DateTime startDate, DateTime deadline)
        {
            if (deadline < startDate) return -1;
            DateTime nextSaturday = getNextWeekday(startDate, DayOfWeek.Saturday);
            if (deadline < nextSaturday) return 0;
            return (deadline - nextSaturday).Days / 7 + 1;
        }
    }
}
