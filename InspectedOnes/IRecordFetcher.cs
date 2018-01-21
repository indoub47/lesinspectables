using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectedOnes
{
    public interface IRecordFetcher
    {
        List<Object[]> Fetch(DateTime fromDate, DateTime toDate);
    }
}
