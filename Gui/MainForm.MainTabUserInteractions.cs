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
            if (dtpDatai.Value.Date != date.Date)
            {
                coefHasChanged = true; // meluojama, kad priversti perskaičiuoti danger
                // perkonstruoti viską iš naujo
                date = dtpDatai.Value;
                inspFactory.ChangeDate(date);
                getInspectables(date);
            }

            if (coefHasChanged)
            {
                dangerCalculator.SetParams(
                    (int)nudX0.Value, (int)nudY0.Value,
                    (int)nudX1.Value, (int)nudY1.Value,
                    (int)nudX2.Value, (int)nudY2.Value,
                    (int)nudKoef064.Value, (int)nudKoefMain.Value, (int)nudKoefOverdue.Value);
                dangerCalculator.BatchCalculate(insps);
                coefHasChanged = false;
                unfilteredRecs = grouper.Group(insps).ToList();
            }

            setFilters();
            filteredRecs = grouper.Group(insps).ToList();
            findMaxCount();
            pb.Invalidate();
        }
                
        private void nudKoef064_ValueChanged(object sender, EventArgs e)
        {
            coefHasChanged = true;
        }

        private void nudKoefMain_ValueChanged(object sender, EventArgs e)
        {
            coefHasChanged = true;
        }

        private void nudKoefOverdue_ValueChanged(object sender, EventArgs e)
        {
            coefHasChanged = true;
        }
    }
}
