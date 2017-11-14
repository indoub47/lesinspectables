using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    public interface IRecordFetcher
    {
        List<object[]> Fetch(DateTime forDate);
    }
}
