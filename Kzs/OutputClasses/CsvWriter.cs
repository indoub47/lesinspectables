using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Kzs.OutputClasses
{
    public class CsvWriter : CommonWriter
    {
        override public string GetExtensionFilter()
        {
            return "Comma Separated Values (*.csv)|*.csv";
        }

        override public void Output(IEnumerable<Inspectable> insps, DateTime forDate, string fileName)
        {

            List<Inspectable> orderedInsps = PreprocessRecords(insps);
            StringBuilder csvSB = inspsToCsv(orderedInsps);

            try
            {
                File.WriteAllText(fileName, csvSB.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private StringBuilder inspsToCsv(List<Inspectable> insps)
        {
            StringBuilder sb = new StringBuilder();
            string delim = Properties.Settings.Default.CsvDelimiter;

            for (int i = 0; i < insps.Count(); i++)
            {
                Inspectable insp = insps[i];
                sb
                    .Append(insp.Id).Append(delim)
                    .Append(insp.Vkodas.Linija).Append(delim)
                    .Append(insp.Vkodas.Kelias).Append(delim)
                    .Append(insp.Vkodas.Km).Append(delim)
                    .Append(insp.Vkodas.Pk).Append(delim)
                    .Append(insp.Vkodas.M).Append(delim)
                    .Append(insp.Vkodas.Siule == null ? "" : insp.Vkodas.Siule.ToString()).Append(delim)
                    .Append(insp.Skodas).Append(delim)
                    .Append((int)insp.Ktas).Append(delim)
                    .Append(insp.DataNuo.ToShortDateString()).Append(delim)
                    .Append(insp.DataIki.ToShortDateString()).Append(delim)
                    .Append(insp.Koord).Append(delim)
                    .Append(insp.WeeksAway).AppendLine();
            }
            return sb;
        }
    }
}
