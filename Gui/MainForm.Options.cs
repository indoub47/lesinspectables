using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gui.Properties;
using System.IO;

namespace Gui
{
    // Options
    partial class MainForm
    {
        private void populateSettings()
        {
            txbHelperDbPath.Text = Settings.Default.OptionsDbFileName;
            txbMainDbPath.Text = Settings.Default.MainDbFileName;
            txbOutputFolder.Text = Settings.Default.OutputDir;

            nudX0.Value = koefX0 = Settings.Default.koefX0;
            nudY0.Value = koefY0 = Settings.Default.koefY0;
            nudX1.Value = koefX1 = Settings.Default.koefX1;
            nudY1.Value = koefY1 = Settings.Default.koefY1;
            nudX2.Value = koefX2 = Settings.Default.koefX2;
            nudY2.Value = koefY2 = Settings.Default.koefY2;
            nudKoefMain.Value = koefMain = Settings.Default.CoefMain;
            nudKoefOverdue.Value = koefOverdue = Settings.Default.CoefOverdued;
            nudKoef064.Value = koef064 = Settings.Default.CoefThermit;

            dtpDatai.Value = date = DateTime.Now;
            skodaiFilters = Properties.Settings.Default.Skodai.ToList();
            skodaiFilters.ForEach(x => chlbSkodai.Items.Add(x));
            for (int i = 0; i < chlbSkodai.Items.Count; i++)
            {
                chlbSkodai.SetItemChecked(i, true);
            }

            linijosFilters = Properties.Settings.Default.Linijos.ToList();
            linijosFilters.ForEach(x => chlbLinijos.Items.Add(x));
            for (int i = 0; i < chlbLinijos.Items.Count; i++)
            {
                chlbLinijos.SetItemChecked(i, true);
            }

            chbNepagr.Checked = true;

            nudLiko.Value = nudLiko.Maximum;            
        }

        private void btnChangeMainDb_Click(object sender, EventArgs e)
        {
            setFile("MainDbFileName",
                txbMainDbPath,
                "Suvirinimų duomenų bazė",
                "Access (*.accdb;*.mdb)|*.accdb;*.mdb|All files (*.*)|*.*");
        }

        private void btnChangeHelpDb_Click(object sender, EventArgs e)
        {
            setFile("OptionsDbFileName",
                txbHelperDbPath,
                "Pagalbinė duomenų bazė",
                "Access (*.accdb;*.mdb)|*.accdb;*.mdb|All files (*.*)|*.*");

        }

        private void btnChangeOutputFolder_Click(object sender, EventArgs e)
        {
            setFolder("OutputDir", txbOutputFolder);
        }


        private void setFolder(string ofSetting, TextBox txb)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = true;
            if (Settings.Default[ofSetting] == null || !Directory.Exists((string)(Settings.Default[ofSetting])))
            {
                fbd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            else
            {
                fbd.SelectedPath = (string)(Settings.Default[ofSetting]);
            }
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Settings.Default[ofSetting] = fbd.SelectedPath;
                txb.Text = fbd.SelectedPath;
                Settings.Default.Save();
            }

            fbd.Dispose();
        }


        private void setFile(string pathSetting, TextBox txb, string title, string filter)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Title = title;
            if (Settings.Default[pathSetting] == null || !File.Exists((string)(Settings.Default[pathSetting])))
            {
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                ofd.InitialDirectory = Path.GetDirectoryName((string)(Settings.Default[pathSetting]));
            }
            ofd.Filter = filter;
            ofd.FilterIndex = 1;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Settings.Default[pathSetting] = ofd.FileName;
                txb.Text = ofd.FileName;
                Settings.Default.Save();
            }
            ofd.Dispose();
        }

    }
}
