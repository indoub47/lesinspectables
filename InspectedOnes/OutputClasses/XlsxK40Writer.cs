using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using InteropExcel = Microsoft.Office.Interop.Excel;
using System.IO;
using InspectedOnes.Properties;

namespace InspectedOnes.OutputClasses
{
    public class XlsxK40Writer: IInspectedOutputter
    {
        string templateFileName;
        string topLeftCell;
        string[] mappingDb, mappingTemplate;
        int templateColumnCount;

        public XlsxK40Writer(string template)
        {
            templateFileName = template;
            topLeftCell = Settings.Default.TopLeftCellK40;
            mappingDb = Settings.Default.MappingDb;
            mappingTemplate = Settings.Default.MappingK40Template;
            templateColumnCount = mappingTemplate.Length;
        }

        public string GetDefaultFNFormat()
        {
            return Properties.Settings.Default.DefaultXlsxK40FNFormat;
        }

        public void Output(List<Object[]> records, string destFileName)
        {
            File.Copy(templateFileName, destFileName, true);
            // might throw an exception

            InteropExcel.Application app = new InteropExcel.Application();
            app.Visible = false;
            app.DisplayAlerts = false;
            InteropExcel.Workbook workbook = app.Workbooks.Open(destFileName);
            InteropExcel.Worksheet templateSheet = workbook.Worksheets[Settings.Default.TemplateSheetName];
            InteropExcel.Worksheet sheet = null;
            InteropExcel.Range range = null;

            int indxAparat = Array.IndexOf(mappingDb, "aparat");
            int indxPDate = Array.IndexOf(mappingDb, "pdata");
            int indxOperat = Array.IndexOf(mappingDb, "operat");
            int indxSKodas = Array.IndexOf(mappingDb, "skodas");
            int indxKelintas = Array.IndexOf(mappingDb, "kelintas");
            int indxLinija = Array.IndexOf(mappingDb, "linija");
            int indxKel = Array.IndexOf(mappingDb, "kel");
            int indxKm = Array.IndexOf(mappingDb, "km");
            int indxPk = Array.IndexOf(mappingDb, "pk");
            int indxM = Array.IndexOf(mappingDb, "m");
            int indxSiule = Array.IndexOf(mappingDb, "siule");

            var grouped = records
                .OrderBy(x => x[indxAparat])
                .ThenBy(x => x[indxPDate])
                .ThenBy(x => x[indxOperat])
                .ThenBy(x => x[indxSKodas])
                .ThenBy(x => x[indxKelintas])
                .ThenBy(x => x[indxLinija])
                .ThenBy(x => x[indxKel])
                .ThenBy(x => x[indxKm])
                .ThenBy(x => x[indxPk])
                .ThenBy(x => x[indxM])
                .ThenBy(x => x[indxSiule])
                .GroupBy(x => x[indxAparat], (key, group) => new
            {
                Aparat = key.ToString(),
                Inspected = group
            });

            try
            {
                foreach (var group in grouped)
                {
                    // templatesheet kopijuojamas į patį galą
                    templateSheet.Copy(Type.Missing, workbook.Worksheets[workbook.Worksheets.Count]);
                    // sheet imamas kaip einamasis
                    sheet = (InteropExcel.Worksheet)workbook.Worksheets[workbook.Worksheets.Count];
                    // sheet pervardinamas operatoriaus kodu
                    if (group.Aparat != null && group.Aparat.ToString().Trim() != "")
                    {
                        sheet.Name = group.Aparat;
                    }
                    // inspected paverčiami string array [,]
                    string[,] valueArray = recsToStringArray(group.Inspected);
                    // konstruojamas range objektas iš sheet eilučių ir stulpelių
                    range = sheet.Range[topLeftCell].Resize[group.Inspected.Count(), templateColumnCount];
                    // range priskiriamas value array
                    range.Value = valueArray;
                }

                //ištrinamas template sheet
                templateSheet.Delete();
                workbook.Save();
            }
            catch (Exception ex)
            {
                if (File.Exists(destFileName))
                {
                    try
                    {
                        File.Delete(destFileName);
                        throw new Exception("Gaminant failą, įvyko klaida. Failas panaikintas.", ex);
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
                Marshal.ReleaseComObject(templateSheet);
                if (workbook != null)
                {
                    workbook.Close();
                    Marshal.ReleaseComObject(workbook);
                }
            }
            app.Quit();
            Marshal.ReleaseComObject(app);
        }



        private string[,] recsToStringArray(IEnumerable<Object[]> recs)
        {
            string[] mappingTemplate = Settings.Default.MappingK40Template;
            string[,] sarr = new string[recs.Count(), templateColumnCount];

            int indxDate = Array.IndexOf(mappingDb, "pdata");

            for (int r = 0; r < recs.Count(); r++)
            {
                object[] row = recs.ElementAt(r);
                for (int c = 0; c < templateColumnCount; c++)
                {
                    string colName = mappingTemplate[c];
                    int cellIndex = Array.IndexOf(mappingDb, colName);
                    object value = row[cellIndex];

                    if (value == null)
                    {
                        sarr[r, c] = "";
                    }
                    else if (cellIndex == indxDate)
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

        //private string getDestFileName(string destFolder, DateTime dateFrom, DateTime dateTo)
        //{
        //    string fileNameFormat = Settings.Default.FileNameFormat;
        //    return Path.Combine(destFolder, string.Format(fileNameFormat, dateFrom, dateTo));
        //}

        public string GetExtensionFilter()
        {
            return "MS Excel (*.xlsx)|*.xlsx";
        }
    }
}
