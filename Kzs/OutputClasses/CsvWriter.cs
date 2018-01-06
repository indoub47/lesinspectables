using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Kzs.OutputClasses
{
    public class CsvWriter : IInspectableOutputter
    {
        public string GetExtensionFilter()
        {
            return "Comma Separated Values (*.csv)|*.csv";
        }

        public void Output(IEnumerable<Inspectable> insps, DateTime forDate, string fileName)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("Id").Append(";");
            sb.Append("Linija").Append(";");
            sb.Append("Kelias").Append(";");
            sb.Append("Km").Append(";");
            sb.Append("Pk").Append(";");
            sb.Append("M").Append(";");
            sb.Append("Siule").Append(";");
            sb.Append("Salyg. kodas").Append(";");
            sb.Append("Patikrinti iki").Append(";");
            sb.Append("Kelintas tikrinimas").AppendLine();

            foreach (var insp in insps)
            {
                sb.AppendFormat("{0};\"{1}\";{2};{3};{4};{5};{6};\"{7}\";\"{8}\";{9}",
                    insp.Id,
                    insp.Vkodas.Linija,
                    insp.Vkodas.Kelias,
                    insp.Vkodas.Km,
                    insp.Vkodas.Pk,
                    insp.Vkodas.M,
                    insp.Vkodas.Siule == null ? "" : insp.Vkodas.Siule.ToString(),
                    insp.Skodas,
                    forDate.AddDays(insp.Liko).ToShortDateString(),
                    (int)insp.Ktas);
                sb.AppendLine();
            }

            try
            {
                File.WriteAllText(fileName, sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
