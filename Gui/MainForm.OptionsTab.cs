﻿using System;
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

        private void nudY0_ValueChanged(object sender, EventArgs e)
        {
            koefY0 = (int)nudY0.Value;
            paramPainter.SetPoints(
                new PointF(koefX0, koefY0),
                new PointF(koefX1, koefY1),
                new PointF(koefX2, koefY2));
            //dangerCalculator.SetParams(koefX0, koefY0, koefX1, koefY1, koefX2, koefY2, koefMain, koefOverdue, koef064);
            //dangerCalculator.BatchCalculate(insps);
            Settings.Default.KoefY0 = koefY0;
            Settings.Default.Save();
            recalculateDanger = true;
            pbxDangerParameters.Invalidate();
        }

        private void nudX1_ValueChanged(object sender, EventArgs e)
        {
            koefX1 = (int)nudX1.Value;
            paramPainter.SetPoints(
                new PointF(koefX0, koefY0),
                new PointF(koefX1, koefY1),
                new PointF(koefX2, koefY2));
            //dangerCalculator.SetParams(koefX0, koefY0, koefX1, koefY1, koefX2, koefY2, koefMain, koefOverdue, koef064);
            //dangerCalculator.BatchCalculate(insps);
            Settings.Default.KoefX1 = koefX1;
            Settings.Default.Save();
            recalculateDanger = true;
            pbxDangerParameters.Invalidate();
        }

        private void nudY1_ValueChanged(object sender, EventArgs e)
        {
            koefY1 = (int)nudY1.Value;
            paramPainter.SetPoints(
                new PointF(koefX0, koefY0),
                new PointF(koefX1, koefY1),
                new PointF(koefX2, koefY2));
            //dangerCalculator.SetParams(koefX0, koefY0, koefX1, koefY1, koefX2, koefY2, koefMain, koefOverdue, koef064);
            //dangerCalculator.BatchCalculate(insps);
            Settings.Default.KoefY1 = koefY1;
            Settings.Default.Save();
            recalculateDanger = true;
            pbxDangerParameters.Invalidate();
        }

        private void nudY2_ValueChanged(object sender, EventArgs e)
        {
            koefY2 = (int)nudY2.Value;
            paramPainter.SetPoints(
                new PointF(koefX0, koefY0),
                new PointF(koefX1, koefY1),
                new PointF(koefX2, koefY2));
            //dangerCalculator.SetParams(koefX0, koefY0, koefX1, koefY1, koefX2, koefY2, koefMain, koefOverdue, koef064);
            //dangerCalculator.BatchCalculate(insps);
            Settings.Default.KoefY2 = koefY2;
            Settings.Default.Save();
            recalculateDanger = true;
            pbxDangerParameters.Invalidate();
        }
        
        private void setFile(string pathSetting, TextBox txb, string title, string filter)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = title,
                Filter = filter,
                FilterIndex = 1
            };

            if (Settings.Default[pathSetting] == null || !File.Exists((string)(Settings.Default[pathSetting])))
            {
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                ofd.InitialDirectory = Path.GetDirectoryName((string)(Settings.Default[pathSetting]));
            }

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Settings.Default[pathSetting] = ofd.FileName;
                txb.Text = ofd.FileName;
                Settings.Default.Save();
                informReload();
            }
            ofd.Dispose();
        }

        private void rbExportXlsx_CheckedChanged(object sender, EventArgs e)
        {
            exportFormatChanged();
        }

        private void rbExportCsv_CheckedChanged(object sender, EventArgs e)
        {
            exportFormatChanged();
        }

        private void rbExportJson_CheckedChanged(object sender, EventArgs e)
        {
            exportFormatChanged();
        }

        private void exportFormatChanged()
        {
            if (rbExportXlsx.Checked)
            {
                Settings.Default.ExportFormat = "xlsx";
            }
            else if (rbExportCsv.Checked)
            {
                Settings.Default.ExportFormat = "csv";
            }
            else if (rbExportJson.Checked)
            {
                Settings.Default.ExportFormat = "json";
            }
            else
            {
                // default
                Settings.Default.ExportFormat = "csv";
            }
            Settings.Default.Save();
        }

        private void informReload()
        {
            MessageBox.Show("Kad pakeitimai įsigaliotų, reikia paleisti programą iš naujo.");
        }
    }
}
