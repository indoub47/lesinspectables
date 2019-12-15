using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs.OutputClasses
{
    abstract public class CommonWriter:IInspectableOutputter
    {
        public abstract void Output(IEnumerable<Inspectable> insps, DateTime forDate, string fileName);
        public abstract string GetExtensionFilter();

        protected List<Inspectable> PreprocessRecords(IEnumerable<Inspectable> insps)
        {
            return insps
                .OrderBy(x => x.Vkodas.Linija)
                .ThenBy(x => x.Vkodas.Km)
                .ThenBy(x => x.Vkodas.Pk)
                .ThenBy(x => x.Vkodas.M)
                .ThenBy(x => x.Vkodas.Kelias)
                .ThenBy(x => x.Vkodas.Siule)
                .ToList();
        }
    }
}
