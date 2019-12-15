using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectedOnes.OutputClasses
{
    abstract public class CommonK40Writer:IInspectedOutputter
    {
        public abstract void Output(List<Object[]> records, string destFileName);
        public abstract string GetExtensionFilter();

        protected List<Object[]> PreprocessRecords(List<Object[]> records, string[] dbMapping)
        {
            return records
                .OrderBy(x => x[Array.IndexOf(dbMapping, "aparat")])
                .ThenBy(x => x[Array.IndexOf(dbMapping, "pdata")])
                .ThenBy(x => x[Array.IndexOf(dbMapping, "skodas")])
                .ThenBy(x => x[Array.IndexOf(dbMapping, "kelintas")])
                .ThenBy(x => x[Array.IndexOf(dbMapping, "operat")])
                .ThenBy(x => x[Array.IndexOf(dbMapping, "linija")])
                .ThenBy(x => x[Array.IndexOf(dbMapping, "kel")])
                .ThenBy(x => x[Array.IndexOf(dbMapping, "km")])
                .ThenBy(x => x[Array.IndexOf(dbMapping, "pk")])
                .ThenBy(x => x[Array.IndexOf(dbMapping, "m")])
                .ThenBy(x => x[Array.IndexOf(dbMapping, "siule")])
                .ThenBy(x => x[Array.IndexOf(dbMapping, "id")])
                .ToList();
        }

        public string CreateFileName(DateTime from, DateTime to)
        {
            return string.Format(Properties.Settings.Default.DefaultK40FNFormat, from, to);
        }

    }
}
