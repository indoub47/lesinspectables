using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectedOnes
{ 
    public interface IInspectedOutputter
    {
        void Output(List<Object[]> records, string destFileName);
        string GetExtensionFilter();
        string GetDefaultFNFormat();
    }
}
