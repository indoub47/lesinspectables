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
    // Interactions
    partial class MainForm
    {
        private void pb_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = mouseCurrent = e.Location;
            pb.Paint += paintLine;
            pb.MouseMove += pb_MouseMove;
        }
        
        private void pb_MouseMove(object sender, MouseEventArgs e)
        {
            mouseCurrent = e.Location;
            pb.Invalidate();
        }

        private void pb_MouseUp(object sender, MouseEventArgs e)
        {
            pb.Paint -= paintLine;
            pb.MouseMove -= pb_MouseMove;

            pb.Invalidate();
            if (!ModifierKeys.HasFlag(Keys.Control))
            {
                collected.Clear();
                foreach (var lin in filteredRecs)
                {
                    foreach (var km in lin.Kms)
                    {
                        km.POptions.Selected = false;
                    }
                }
            }

            collectIntersected(e.X, e.Y);

            writeCollectedStatus();

            btnExportCollected.Enabled = collected.Count != 0;
        }

        private void btnExportCollected_Click(object sender, EventArgs e)
        {
            pb.Paint -= pb_Paint;
            try
            {
                outputter.Output(collected, date);
                MessageBox.Show($"Išsaugota {outputter.GetFileName()}");
                collected.Clear();
                btnExportCollected.Enabled = false;
                slblCollected.Text += $". Išsaugota {outputter.GetFileName()}";
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
                    (int)nudKoef064.Value, (int)nudKoefMain.Value, (int)nudKoefOverdue.Value);
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
    }
}
