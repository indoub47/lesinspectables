using DefReview.Properties;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using InteropExcel = Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;
using System.ComponentModel;

i DONT NEED THIS ANYMORE

I'm going to delete this



private static void writeExcel(BackgroundWorker worker, DoWorkEventArgs e, ref int progressCounter, DateTime from, DateTime to)
        {
                string outputFolder = Settings.Default.SummaryOutputFolder;
                string outputFileName = string.Format(Settings.Default.Summary1OutputFileNameTemplate, from, to);
                string fullDestFileName = Path.Combine(outputFolder, outputFileName);
                string templateFileName = Settings.Default.Summary1TemplatePath;
                InteropExcel.Application app = new InteropExcel.Application();
                app.Visible = false;
                File.Copy(templateFileName, fullDestFileName, true);

                InteropExcel.Workbook workbook = app.Workbooks.Open(fullDestFileName);
                InteropExcel.Worksheet sheet = (InteropExcel.Worksheet)workbook.Sheets[Settings.Default.Summary1WorksheetName];
                InteropExcel.Range range = null;

            try
            {

                range = sheet.Range[Settings.Default.Summary1HeaderAddress];
                range.Value = string.Format(range.Value, from, to);                

                foreach (AddressTable atable in dictTables.Values)
                {
                    range = sheet.Range[atable.Address].Resize[atable.IntArray.GetLength(0), atable.IntArray.GetLength(1)];
                    range.Value = atable.IntArray;
                }

                workbook.Save();
                app.Quit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                range = null;
                sheet = null;
                workbook = null;
                app = null;
                Marshal.ReleaseComObject(range);
                Marshal.ReleaseComObject(sheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(app);
            }
        }
		
		private static void writeExcel(BackgroundWorker worker, DoWorkEventArgs e, ref int progressCounter, DateTime from, DateTime to)
        {
            string outputFolder = Settings.Default.SummaryOutputFolder;
            string outputFileName = string.Format(Settings.Default.Summary2OutputFileNameTemplate, from, to);
            string fullDestFileName = Path.Combine(outputFolder, outputFileName);
            string templateFileName = Settings.Default.Summary2TemplatePath;
            InteropExcel.Application app = new InteropExcel.Application();
            app.Visible = false;
            File.Copy(templateFileName, fullDestFileName, true);

            InteropExcel.Workbook workbook = app.Workbooks.Open(fullDestFileName);
            InteropExcel.Worksheet sheet = (InteropExcel.Worksheet)workbook.Sheets[Settings.Default.Summary2WorksheetName];
            InteropExcel.Range range = null;
            range = sheet.Range[Settings.Default.Summary2HeaderAddress];
            range.Value = string.Format(range.Value, from, to);

            foreach (AddressTable atable in dictTables.Values)
            {
                range = sheet.Range[atable.Address].Resize[atable.IntArray.GetLength(0), atable.IntArray.GetLength(1)];
                range.Value = atable.IntArray;
            }

            workbook.Save();
            app.Quit();
        }

        internal static void CreateSummary2(BackgroundWorker worker, DoWorkEventArgs e)
        {

            Object[] argument = e.Argument as Object[];
            DateTime dateFrom = Convert.ToDateTime(argument[0]);
            DateTime dateTo = Convert.ToDateTime(argument[1]);
            DateTime dateBottom = Convert.ToDateTime(argument[2]);
            string defectConnString = argument[3].ToString();
            string optionsConnString = argument[4].ToString();   

            try
            {
                loadRanges(optionsConnString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading ranges error. " + ex.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                createMappings(optionsConnString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Create mappings error. " + ex.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DataChecker.checkData(dateBottom, dateFrom, dateTo, defectConnString, mappMeistrijos, mappPavojingumai);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Checking data error. " + ex.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show("Creating tables error. " + ex.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                writeExcel(worker, e, ref pc, dateFrom, dateTo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Writting excel error. " + ex.Message, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Done");

            e.Result = "Success. Enjoy your gorgeous summary.";
        }

    }