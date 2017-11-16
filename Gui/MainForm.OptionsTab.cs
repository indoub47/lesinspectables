using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gui.Properties;
using System.IO;
using System.Drawing;

namespace Gui
{
    // Options tab
    partial class MainForm
    {
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

        private void nudX1_ValueChanged(object sender, EventArgs e)
        {
            koefX1 = (int)nudX1.Value;
            paramPainter.SetPoints(
                new PointF(koefX0, koefY0),
                new PointF(koefX1, koefY1),
                new PointF(koefX2, koefY2));
            dangerCalculator.SetParams(koefX0, koefY0, koefX1, koefY1, koefX2, koefY2, koefMain, koefOverdue, koef064);
            dangerCalculator.BatchCalculate(insps);
            Properties.Settings.Default.koefX1 = koefX1;
            Properties.Settings.Default.Save();
            pbxDangerParameters.Invalidate();
        }

        private void nudY1_ValueChanged(object sender, EventArgs e)
        {
            koefY1 = (int)nudY1.Value;
            paramPainter.SetPoints(
                new PointF(koefX0, koefY0),
                new PointF(koefX1, koefY1),
                new PointF(koefX2, koefY2));
            dangerCalculator.SetParams(koefX0, koefY0, koefX1, koefY1, koefX2, koefY2, koefMain, koefOverdue, koef064);
            dangerCalculator.BatchCalculate(insps);
            Properties.Settings.Default.koefY1 = koefY1;
            Properties.Settings.Default.Save();
            pbxDangerParameters.Invalidate();
        }

        private void nudY2_ValueChanged(object sender, EventArgs e)
        {
            koefY2 = (int)nudY2.Value;
            paramPainter.SetPoints(
                new PointF(koefX0, koefY0),
                new PointF(koefX1, koefY1),
                new PointF(koefX2, koefY2));
            dangerCalculator.SetParams(koefX0, koefY0, koefX1, koefY1, koefX2, koefY2, koefMain, koefOverdue, koef064);
            dangerCalculator.BatchCalculate(insps);
            Properties.Settings.Default.koefY2 = koefY2;
            Properties.Settings.Default.Save();
            pbxDangerParameters.Invalidate();
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
                outputter.SetOutputDir(fbd.SelectedPath);
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
                MessageBox.Show("Kad DB pakeitimai įsigaliotų, reikia paleisti programą iš naujo.");
            }
            ofd.Dispose();
        }
    }
}
