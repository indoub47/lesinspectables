using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InspectedOnes;
using System.Windows.Forms;
using System.IO;
using Gui.Properties;

namespace Gui
{
    partial class MainForm
    {

        private List<ulong> findBadIds(DateTime dateFrom, DateTime dateTo)
        {
            IBadRecordFinder badRecordFinder;
            badRecordFinder = new MSAccessBadRecordFinder(
                string.Format(
                        Settings.Default.ConnectionString,
                        Settings.Default.MainDbFileName)
                );

            // ieško nepilnų įrašų - kur nurodyta pakeitimo data, bet nenurodytas 
            // aparatas arba operatorius

            return badRecordFinder.FindBadRecords(dateFrom, dateTo);
        }


        private void btnGenerateK40_Click(object sender, EventArgs e)
        {
            DateTime dateFrom = dtpInspectedFrom.Value;
            DateTime dateTo = dtpInspectedTo.Value;
            List<ulong> badIds;
            try
            {
                badIds = findBadIds(dateFrom, dateTo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace,
                    "Klaida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // jeigu randa blogų - informuoja ir išeina
            if (badIds.Count > 0)
            {
                string badRecordIds = string.Join(", ", badIds.Select(x => x.ToString()));
                MessageBox.Show("Nepilni įrašai DB: " + badRecordIds);
                return;
            }

            // jeigu blogų nerado, tada dirba toliau
            IInspectedOutputter outputter;
            string outputformat = Settings.Default.ExportFormat;
            switch (outputformat)
            {
                case "xlsx":
                    outputter = new InspectedOnes.OutputClasses.XlsxK40Writer(
                        Settings.Default.XlsxK40TemplateInspected);
                    break;
                default:
                    outputter = new InspectedOnes.OutputClasses.CsvK40Writer();
                    break;
            }
            string fileName = string.Format(outputter.GetDefaultFNFormat(), dateFrom, dateTo);
            string fullFileName = getFileNameFromSFD(outputter.GetExtensionFilter(), fileName);

            if (fullFileName == "") return; // reiškia, paspaudė Cancel

            try
            {
                InspectedOnes.IRecordFetcher rFetcher = new MSAccessRecordFetcher(
                    string.Format(
                        Settings.Default.ConnectionString,
                        Settings.Default.MainDbFileName)
                    );
                List<object[]> records = rFetcher.Fetch(dateFrom, dateTo);
                if (records.Count == 0)
                {
                    MessageBox.Show(
                        string.Format("{0:yyyy-MM-dd} - {1:yyyy-MM-dd} nebuvo tikrinta suvirinimų", dateFrom, dateTo),
                        "Info",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
                outputter.Output(records, fullFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show($"Išsaugota faile \"{fullFileName}\"", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGenerateInspected_Click(object sender, EventArgs e)
        {
            DateTime dateFrom = dtpInspectedFrom.Value;
            DateTime dateTo = dtpInspectedTo.Value;
            List<ulong> badIds;
            try
            {
                badIds = findBadIds(dateFrom, dateTo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace,
                    "Klaida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // jeigu randa blogų - informuoja ir išeina
            if (badIds.Count > 0)
            {
                string badRecordIds = string.Join(", ", badIds.Select(x => x.ToString()));
                MessageBox.Show("Nepilni įrašai DB: " + badRecordIds);
                return;
            }

            // jeigu blogų nerado, tada dirba toliau
            IInspectedOutputter outputter;
            string outputformat = Settings.Default.ExportFormat;
            OperatorManager opManager = new OperatorManager(
                string.Format(
                    Settings.Default.ConnectionString,
                    Settings.Default.OptionsDbFileName
                ));
            switch (outputformat)
            {
                case "xlsx":
                    outputter = new InspectedOnes.OutputClasses.XlsxWriter(
                        Settings.Default.XlsxTemplateInspected,
                        opManager.GetOperators());
                    break;
                default:
                    outputter = new InspectedOnes.OutputClasses.CsvWriter();
                    break;
            }
            string fileName = string.Format(outputter.GetDefaultFNFormat(), dateFrom, dateTo);
            string fullFileName = getFileNameFromSFD(outputter.GetExtensionFilter(), fileName);

            if (fullFileName == "") return; // reiškia, paspaudė Cancel

            try
            {
                InspectedOnes.IRecordFetcher rFetcher = new MSAccessRecordFetcher(
                    string.Format(
                        Settings.Default.ConnectionString,
                        Settings.Default.MainDbFileName)
                    );
                List<object[]> records = rFetcher.Fetch(dateFrom, dateTo);
                if (records.Count == 0)
                {
                    MessageBox.Show(
                        string.Format("{0:yyyy-MM-dd} - {1:yyyy-MM-dd} nebuvo tikrinta suvirinimų", dateFrom, dateTo), 
                        "Info", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Information);
                    return;
                }
                outputter.Output(records, fullFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show($"Išsaugota faile \"{fullFileName}\"", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string getFileNameFromSFD(string filter, string fileName)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Išsaugoti failą";
            sfd.FileName = fileName;
            sfd.Filter = filter;
            string lastDir = Properties.Settings.Default.InspectedOutputDir;

            if (Directory.Exists(lastDir))
            {
                sfd.InitialDirectory = lastDir;
            }
            else
            {
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.InspectedOutputDir = Path.GetDirectoryName(sfd.FileName);
                Properties.Settings.Default.Save();
                return sfd.FileName;
            }
            else
            {
                return "";
            }
        }
    }
}
