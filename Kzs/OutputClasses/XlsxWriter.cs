using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using InteropExcel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace Kzs.OutputClasses
{
    public class XlsxWriter: CommonWriter
    {
        readonly string templateFileName;
        readonly string worksheetName;
        readonly string topLeftCell;
        const int oUTPUT_COLUMN_COUNT = 13;

        public XlsxWriter(string template)
        {
            templateFileName = template;
            worksheetName = Properties.Settings.Default.WorksheetName;
            topLeftCell = Properties.Settings.Default.TopLeftCell;
        }

        override public void Output(IEnumerable<Inspectable> insps, DateTime forDate, string destFileName)
        {
            InteropExcel.Application app = null;
            InteropExcel.Workbook workbook = null;
            InteropExcel.Worksheet sheet = null;
            InteropExcel.Range range = null;

            List<Inspectable> orderedInsps = PreprocessRecords(insps);
            string[,] valueArray = inspsToStringArray(orderedInsps);

            try
            {
                app = new InteropExcel.Application
                {
                    Visible = false,
                    DisplayAlerts = false
                };
                File.Copy(templateFileName, destFileName, true);
                workbook = app.Workbooks.Open(destFileName);
                sheet = (InteropExcel.Worksheet)workbook.Sheets[worksheetName];
                range = sheet.Range[topLeftCell].Resize[orderedInsps.Count(), oUTPUT_COLUMN_COUNT];
                range.Value = valueArray;
                workbook.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("MS Excel output exception:");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Data);
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



        private string[,] inspsToStringArray(IEnumerable<Inspectable> insps)
        {
            string[,] sarr = new string[insps.Count(), oUTPUT_COLUMN_COUNT];

            List<Inspectable> inspList = insps.ToList();

            for (int i = 0; i < insps.Count(); i++)
            {
                Inspectable current = inspList[i];
                sarr[i, 0] = current.Id.ToString();
                sarr[i, 1] = current.Vkodas.Linija;
                sarr[i, 2] = current.Vkodas.Kelias.ToString();
                sarr[i, 3] = current.Vkodas.Km.ToString();
                sarr[i, 4] = current.Vkodas.Pk.ToString();
                sarr[i, 5] = current.Vkodas.M.ToString();
                sarr[i, 6] = current.Vkodas.Siule == null ? "" : current.Vkodas.Siule.ToString();
                sarr[i, 7] = current.Skodas;
                sarr[i, 8] = ((int)current.Ktas).ToString();
                sarr[i, 9] = current.DataNuo.ToShortDateString();
                sarr[i, 10] = current.DataIki.ToShortDateString();
                sarr[i, 11] = current.Koord.ToString();
                sarr[i, 12] = current.WeeksAway.ToString();
            }
            return sarr;
        }



        override public string GetExtensionFilter()
        {
            return "MS Excel (*.xlsx)|*.xlsx";
        }
    }
}
