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
    public class XlsxWriter: IInspectableOutputter
    {
        string templateFileName;
        const int COLUMN_COUNT = 10;
        string worksheetName;
        string topLeftCell;

        public XlsxWriter(string template)
        {
            templateFileName = template;
            worksheetName = Properties.Settings.Default.WorksheetName;
            topLeftCell = Properties.Settings.Default.TopLeftCell;
        }

        public void Output(IEnumerable<Inspectable> insps, DateTime forDate, string destFileName)
        {
            InteropExcel.Application app = new InteropExcel.Application();
            app.Visible = false;
            InteropExcel.Workbook workbook = null;
            InteropExcel.Worksheet sheet = null;
            InteropExcel.Range range = null;

            try
            {
                File.Copy(templateFileName, destFileName, true);
                workbook = app.Workbooks.Open(destFileName);
                string[,] valueArray = inspsToStringArray(insps, forDate);
                sheet = (InteropExcel.Worksheet)workbook.Sheets[worksheetName];
                range = sheet.Range[topLeftCell].Resize[insps.Count(), COLUMN_COUNT];
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
                        throw new Exception ("Gaminant failą, įvyko klaida. Failas ištrintas.", ex);
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
                Marshal.ReleaseComObject(range);
                Marshal.ReleaseComObject(sheet);
                if (workbook != null)
                {
                    workbook.Close();
                    Marshal.ReleaseComObject(workbook);
                }
            }
            app.Quit();
            Marshal.ReleaseComObject(app);
        }



        private string[,] inspsToStringArray(IEnumerable<Inspectable> insps, DateTime forDate)
        {
            string[,] sarr = new string[insps.Count(), COLUMN_COUNT];

            List<Inspectable> inspList = insps.ToList();

            for(int i = 0; i<insps.Count(); i++)
            {
                sarr[i, 0] = inspList[i].Id.ToString();
                sarr[i, 1] = inspList[i].Vkodas.Linija;
                sarr[i, 2] = inspList[i].Vkodas.Kelias.ToString();
                sarr[i, 3] = inspList[i].Vkodas.Km.ToString();
                sarr[i, 4] = inspList[i].Vkodas.Pk.ToString();
                sarr[i, 5] = inspList[i].Vkodas.M.ToString();
                sarr[i, 6] = inspList[i].Vkodas.Siule == null ? "" : inspList[i].Vkodas.Siule.ToString();
                sarr[i, 7] = inspList[i].Skodas;
                sarr[i, 8] = forDate.AddDays(inspList[i].Liko).ToShortDateString();
                sarr[i, 9] = ((int)inspList[i].Ktas).ToString();
            }
            return sarr;
        }
    }
}
