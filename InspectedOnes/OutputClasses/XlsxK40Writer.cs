using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using InteropExcel = Microsoft.Office.Interop.Excel;
using System.IO;
using InspectedOnes.Properties;

namespace InspectedOnes.OutputClasses
{
    public class XlsxK40Writer: CommonK40Writer
    {
        readonly string templateFileName;
        readonly string topLeftCell;
        readonly string[] dbMapping;
        readonly string[] templateMapping;

        public XlsxK40Writer(string template)
        {
            templateFileName = template;
            topLeftCell = Settings.Default.TopLeftCellK40;
            dbMapping = Settings.Default.MappingDb;
            templateMapping = Settings.Default.MappingK40Template;
        }

        override public void Output(List<Object[]> rawRecords, string destFileName)
        {
            InteropExcel.Application app = null;
            InteropExcel.Workbook workbook = null;
            InteropExcel.Worksheet sheet = null;
            InteropExcel.Range range = null;

            List<object[]> records = PreprocessRecords(rawRecords, dbMapping);
            string[,] valueArray = recordsToStringArray(records, dbMapping, templateMapping, new[] {"pdata"});

            try
            {
                app = new InteropExcel.Application
                {
                    Visible = false,
                    DisplayAlerts = false
                };
                File.Copy(templateFileName, destFileName, true);
                workbook = app.Workbooks.Open(destFileName);
                sheet = (InteropExcel.Worksheet)workbook.Sheets[Settings.Default.SheetName];
                range = sheet.Range[topLeftCell].Resize[records.Count(), templateMapping.Length];
                range.Value = valueArray;
                workbook.Save();
            }
            catch (Exception ex)
            {
                if (File.Exists(destFileName))
                {
                    try
                    {
                        File.Delete(destFileName);
                    }
                    catch
                    {
                        throw new Exception(
                            "Gaminant failą, įvyko klaida. Todėl buvo mėginama failą ištrinti, bet tai nepavyko taip pat. Failas gali būti su klaidingais duomenimis.", ex);
                    }
                }

            }
            finally
            {
                if (range != null)
                {
                    Marshal.ReleaseComObject(range);
                }

                if (sheet != null)
                {
                    Marshal.ReleaseComObject(sheet);
                }

                if (workbook != null)
                {
                    workbook.Close();
                    Marshal.ReleaseComObject(workbook);
                }

                if (app != null)
                {
                    app.Quit();
                    Marshal.ReleaseComObject(app);
                }
            }            
        }        

        override public string GetExtensionFilter()
        {
            return "MS Excel (*.xlsx)|*.xlsx";
        }

        private string[,] recordsToStringArray(IEnumerable<Object[]> records, string[] dbMap, string[] templateMap, string[] dates)
        {
            int templateColumnCount = templateMap.Count();
            int recordsCount = records.Count();
            string[,] sarr = new string[recordsCount, templateColumnCount];

            for (int r = 0; r < recordsCount; r++)
            {
                object[] row = records.ElementAt(r);
                for (int c = 0; c < templateColumnCount; c++)
                {
                    string templateColName = templateMap[c];
                    int cellIndex = Array.IndexOf(dbMap, templateColName);
                    object value = row[cellIndex];

                    if (value == null)
                    {
                        sarr[r, c] = "";
                    }
                    else if (dates.Any(templateColName.Equals))
                    {
                        sarr[r, c] = Convert.ToDateTime(value).ToShortDateString();
                    }
                    else
                    {
                        sarr[r, c] = value.ToString();
                    }
                }
            }
            return sarr;
        }
    }
}
