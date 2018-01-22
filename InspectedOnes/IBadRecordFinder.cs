using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectedOnes
{
    public interface IBadRecordFinder
    {
        List<ulong> FindBadRecords(DateTime dateFrom, DateTime dateTo);
    }
}
