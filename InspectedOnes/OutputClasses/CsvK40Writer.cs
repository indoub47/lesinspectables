using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace InspectedOnes.OutputClasses
{
    public class CsvK40Writer : CommonK40Writer
    {
        private readonly string[] dbMapping;
        private readonly string[] templateMapping;
        private readonly string[] dates;
        private readonly string delim;

        public CsvK40Writer()
        {
            dbMapping = Properties.Settings.Default.MappingDb;
            templateMapping = Properties.Settings.Default.MappingK40Template;
            delim = Properties.Settings.Default.CsvDelimiter;
            dates = Properties.Settings.Default.DateColumnNames;
        }

        override public string GetExtensionFilter()
        {
            return "Comma Separated Values (*.csv)|*.csv";
        }

        override public void Output(List<object[]> rawRecords, string destFileName)
        {

            List<object[]> records = PreprocessRecords(rawRecords, dbMapping);
            StringBuilder csvSB = recordsToCsv(records, dbMapping, templateMapping, dates);

            try
            {
                File.WriteAllText(destFileName, csvSB.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }     
        }


        private StringBuilder recordsToCsv(List<object[]> records, string[] dbMap, string[] templateMap, string[] dates)
        { 
            int recordCount = records.Count();
            int lastRecIndx = recordCount - 1;
            int columnCount = templateMap.Length;
            int lastColIndx = columnCount - 1;
            StringBuilder sb = new StringBuilder();

            for(int r = 0; r < recordCount; r++)
            {
                for (int c = 0; c < columnCount; c++)
                {
                    string colName = templateMap[c];
                    int cellIndex = Array.IndexOf(dbMap, colName);
                    object value = records[r][cellIndex];
                    if (value == null)
                    { /* do nothing */ }
                    else if (dates.Any(colName.Equals))
                    {
                        sb.Append(Convert.ToDateTime(value).ToShortDateString());
                    }
                    else
                    {
                        sb.Append(value);
                    }
                    if (c < lastColIndx) sb.Append(delim);
                }
                if (r < lastRecIndx) sb.AppendLine();
            }
            return sb;
        }
    }

   
}
