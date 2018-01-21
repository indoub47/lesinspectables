using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectedOnes.OutputClasses
{
    public class CsvWriter : IInspectedOutputter
    {
        string[] dbMapping;
        string[] templateMapping;
        string delim;
        string[] columnTitles;
        int templateColCount;
        int indxDate;

        public CsvWriter()
        {
            dbMapping = Properties.Settings.Default.MappingDb;
            templateMapping = Properties.Settings.Default.MappingTemplate;
            delim = Properties.Settings.Default.CsvDelimiter;
            columnTitles = Properties.Settings.Default.CsvColumnTitles;
            templateColCount = templateMapping.Length;
            indxDate = Array.IndexOf(dbMapping, "pdata");
        }

        string IInspectedOutputter.GetExtensionFilter()
        {
            return "Comma Separated Values (*.csv)|*.csv";
        }

        public string GetDefaultFNFormat()
        {
            return Properties.Settings.Default.DefaultCsvFNFormat;
        }

        void IInspectedOutputter.Output(List<object[]> records, string destFileName)
        {
         
            int indxOperator = Array.IndexOf(dbMapping, "operat");

            var grouped = records.GroupBy(x => x[indxOperator], (key, group) => new
            {
                Operator = key.ToString(),
                Inspected = group
            });
            
            string header = String.Join(delim, columnTitles);

            foreach (var group in grouped)
            {
                StringBuilder sb = new StringBuilder(header).AppendLine();
                foreach (object[] line in records)
                {
                    sb.AppendLine(constructLine(line));
                }

                string fileName = constructFileName(destFileName, group.Operator);

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

        private string constructLine(object[] row)
        {
            StringBuilder sb = new StringBuilder();
            for (int c = 0; c < templateColCount; c++)
            {
                string colName = templateMapping[c];
                int cellIndex = Array.IndexOf(dbMapping, colName);
                object value = row[cellIndex];

                if (value == null)
                {
                    // do nothing
                }
                else if (cellIndex == indxDate)
                {
                    sb.Append(Convert.ToDateTime(value).ToShortDateString());
                }
                else
                {
                    sb.Append(value.ToString());
                }

                if (c + 1 < templateColCount)
                {
                    sb.Append(delim);
                }
            }
            return sb.ToString();
        }

        private string constructFileName(string fileName, string operatorId)
        {
            string directory = Path.GetDirectoryName(fileName);
            string file = Path.GetFileNameWithoutExtension(fileName) + " " + operatorId;
            string extension = Path.GetExtension(fileName);

            return Path.Combine(directory, file + extension);
        }
    }
}
