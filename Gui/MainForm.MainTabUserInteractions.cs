using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gui.Properties;
using System.IO;
using System.Drawing;
using Kzs.OutputClasses;
using Kzs;

namespace Gui
{
    // Interactions
    partial class MainForm
    {
        private void pb_MouseDown(object sender, MouseEventArgs e)
        {
            if (!ModifierKeys.HasFlag(Keys.Control) && !ModifierKeys.HasFlag(Keys.Shift))
            {
                collected.Clear();
                foreach (var lin in filteredRecs)
                {
                    foreach (var km in lin.Kms)
                    {
                        km.POptions.Selected = false;
                    }
                }
                writeCollectedStatus();
            }

            freshCollected = new List<Kzs.Grouper.Grouped.KmInsps>();

            mouseDown = mouseCurrent = e.Location;
            pb.Paint += paintRect;
            pb.MouseMove += pb_MouseMove;
        }
        
        private void pb_MouseMove(object sender, MouseEventArgs e)
        {
            mouseCurrent = e.Location;

            if (ModifierKeys.HasFlag(Keys.Shift))
            {
                foreach (var lin in filteredRecs)
                {
                    foreach (var km in lin.Kms)
                    {
                        bool intersect = rectIntersect(mouseDown, e.X, e.Y, km.POptions.X, km.POptions.Y0, km.POptions.Y1);
                        if (km.POptions.Selected && intersect)
                        {
                            //unselect
                            freshCollected.Add(km);
                            km.POptions.Selected = false;
                            foreach (var insp in km.Insps)
                            {
                                collected.RemoveAll(x => x.Id == insp.Id);
                            }
                        }
                        else if (!km.POptions.Selected && !intersect && freshCollected.Contains(km))
                        {
                            // select
                            freshCollected.Remove(km);
                            collected.AddRange(km.Insps);
                            km.POptions.Selected = true;
                        }
                    }
                }
            }
            else if (ModifierKeys.HasFlag(Keys.Control))
            {
                foreach (var lin in filteredRecs)
                {
                    foreach (var km in lin.Kms)
                    {
                        bool intersect = rectIntersect(mouseDown, e.X, e.Y, km.POptions.X, km.POptions.Y0, km.POptions.Y1);
                        if (!km.POptions.Selected && intersect)
                        {
                            collected.AddRange(km.Insps);
                            km.POptions.Selected = true;
                            freshCollected.Add(km);                            
                        }
                        else if (km.POptions.Selected && !intersect && freshCollected.Contains(km))
                        {
                            // unselect
                            freshCollected.Remove(km);
                            foreach (var insp in km.Insps)
                            {
                                collected.RemoveAll(x => x.Id == insp.Id);
                            }
                            km.POptions.Selected = false;
                        }
                    }
                }
            }
            else
            {
                foreach (var lin in filteredRecs)
                {
                    foreach (var km in lin.Kms)
                    {
                        bool intersect = rectIntersect(mouseDown, e.X, e.Y, km.POptions.X, km.POptions.Y0, km.POptions.Y1);
                        if (!km.POptions.Selected && intersect)
                        {
                            collected.AddRange(km.Insps);
                            km.POptions.Selected = true;
                        }
                        else if(km.POptions.Selected && !intersect)
                        {
                            // unselect
                            foreach (var insp in km.Insps)
                            {
                                collected.RemoveAll(x => x.Id == insp.Id);
                            }
                            km.POptions.Selected = false;
                        }
                    }
                }
            }
            writeCollectedStatus();
            pb.Invalidate();
        }

        private void pb_MouseUp(object sender, MouseEventArgs e)
        {
            pb.Paint -= paintRect;
            pb.MouseMove -= pb_MouseMove;
            freshCollected = null;

            pb.Invalidate();

            btnExportCollected.Enabled = collected.Count != 0;
        }

        private string selectFileName(string extensionFilter)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Title = "Išsaugoti failą";
            if (Settings.Default.OutputDir == null || !File.Exists(Settings.Default.OutputDir))
            {
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            else
            {
                sfd.InitialDirectory = Path.GetDirectoryName(Settings.Default.OutputDir);
            }
            sfd.Filter = extensionFilter;
            sfd.FilterIndex = 1;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return sfd.FileName;
            }
            else
            {
                return null;
            }
        }

        private void btnExportCollected_Click(object sender, EventArgs e)
        {
            pb.Paint -= pb_Paint;
            IInspectableOutputter outputter;
            string outputformat = Settings.Default.ExportFormat;
            switch (outputformat)
            {
                case "xlsx":
                    outputter = new XlsxWriter(Settings.Default.XlsxTemplateInspectables);
                    break;
                default:
                    outputter = new Kzs.OutputClasses.CsvWriter();
                    break;
            }

            try
            {
                string fileName = selectFileName(outputter.GetExtensionFilter());
                if (fileName == null)
                {
                    return;
                }
                Settings.Default.OutputDir = Path.GetDirectoryName(fileName);
                Settings.Default.Save();
                outputter.Output(collected, date, fileName);
                MessageBox.Show($"Išsaugota {fileName}");
                collected.Clear();
                btnExportCollected.Enabled = false;
                slblCollected.Text += $". Išsaugota {fileName}";
                foreach(var lin in filteredRecs)
                {
                    foreach(var km in lin.Kms)
                    {
                        km.POptions.Selected = false;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                pb.Paint += pb_Paint;
                pb.Invalidate();
            }
        }
        
        private void btnRepaint_Click(object sender, EventArgs e)
        {
            // Čia reikėtų padaryti atskirą metodą, paduoti jam backgroundworker
            // ir nukelti viską į bgWorker
            // Bet dabar tingiu, ir kol duomenų bazė lokali, užteks wait coursor

            Application.UseWaitCursor = true;
            splitContainer1.Panel1.Enabled = false;

            if (dtpDatai.Value.Date != date.Date)
            {
                date = dtpDatai.Value;
                inspFactory.ChangeDate(date);
                getInspectables(date);
                recalculateDanger = true;
            }

            if (recalculateDanger)
            {
                dangerCalculator.SetParams(
                    (int)nudX0.Value, (int)nudY0.Value,
                    (int)nudX1.Value, (int)nudY1.Value,
                    (int)nudX2.Value, (int)nudY2.Value, 
                    (int)nudKoefMain.Value, (int)nudKoefOverdue.Value, (int)nudKoef064.Value);
                dangerCalculator.BatchCalculate(insps);
                recalculateDanger = false;
                unfilteredRecs = grouper.Group(insps).ToList();
            }

            setFilters();
            filteredRecs = grouper.Group(insps).ToList();
            findMaxCount();

            Application.UseWaitCursor = false;
            splitContainer1.Panel1.Enabled = true;

            pb.Invalidate();
        }
                
        private void nudKoef064_ValueChanged(object sender, EventArgs e)
        {
            recalculateDanger = true;
        }

        private void nudKoefMain_ValueChanged(object sender, EventArgs e)
        {
            recalculateDanger = true;
        }

        private void nudKoefOverdue_ValueChanged(object sender, EventArgs e)
        {
            recalculateDanger = true;
        }



        private void btnNoOverduedColor_Click(object sender, EventArgs e)
        {
            Color color = selectColor(Settings.Default.ColorNoOverdued);
            Settings.Default.ColorNoOverdued = btnNoOverduedColor.BackColor = color;
            brushNoOverdued = new SolidBrush(color);
            Settings.Default.Save();
        }

        private void btnSomeOverduedColor_Click(object sender, EventArgs e)
        {
            Color color = selectColor(Settings.Default.ColorSomeOverdued);
            Settings.Default.ColorSomeOverdued = btnSomeOverduedColor.BackColor = color;
            brushSomeOverdued = new SolidBrush(color);
            Settings.Default.Save();
        }

        private void btnAllOverduedColor_Click(object sender, EventArgs e)
        {
            Color color = selectColor(Settings.Default.ColorAllOverdued);
            Settings.Default.ColorAllOverdued = btnAllOverduedColor.BackColor = color;
            brushAllOverdued = new SolidBrush(color);
            Settings.Default.Save();
        }

        private void btnSelectedColor_Click(object sender, EventArgs e)
        {
            Color color = selectColor(Settings.Default.ColorSelected);
            Settings.Default.ColorSelected = btnSelectedColor.BackColor = color;
            brushSelected = new SolidBrush(color);
            Settings.Default.Save();
        }

        private Color selectColor(Color currentColor)
        {
            // Keeps the user from selecting a custom color.
            colorDialog1.AllowFullOpen = true;
            // Sets the initial color select to the current color.
            colorDialog1.Color = currentColor;

            // Update color if the user clicks OK 
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                return colorDialog1.Color;
            else
                return currentColor;
        }
    }
}
