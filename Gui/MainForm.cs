using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kzs;
using Kzs.OutputClasses;
using System.Drawing.Drawing2D;

namespace Gui
{
    public partial class MainForm : Form
    {
        Point mouseDown;
        Point mouseCurrent;

        bool coefHasChanged;
        DateTime date;
        string[] mapping;

        IRecordFetcher recordFetcher;
        InspectableFactory inspFactory;
        IDangerCalculator dangerCalculator;
        Grouper grouper;

        List<Inspectable> insps = new List<Inspectable>();
        List<Grouper.Grouped> unfilteredRecs;
        List<Grouper.Grouped> filteredRecs;
        List<string> linijosFilters;
        List<string> skodaiFilters;

        List<Inspectable> collected = new List<Inspectable>();

        int maxUnfiltered;
        int countUnfiltered;
        int maxFiltered;
        int countFiltered;

        IInspectableOutputter outputter;

        public MainForm()
        {
            InitializeComponent();
            coefHasChanged = false;
            outputter = new CsvWriter(
                Properties.Settings.Default.OutputDir, 
                Properties.Settings.Default.OutputExelFName);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
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

            nudKoefMain.Value = (decimal)Properties.Settings.Default.CoefMain;
            nudKoefOverdue.Value = (decimal)Properties.Settings.Default.CoefOverdued;
            nudKoef064.Value = (decimal)Properties.Settings.Default.CoefThermit;


            chargeInspManager();
            getInspectables(date);
            dangerCalculator.SetParams(
                Properties.Settings.Default.CoefMain,
                Properties.Settings.Default.CoefOverdued,
                Properties.Settings.Default.CoefThermit);
            dangerCalculator.BatchCalculate(insps);
            grouper.ClearFilterMethods();
            unfilteredRecs = grouper.Group(insps).ToList();
            setFilters();
            filteredRecs = grouper.Group(insps).ToList();
            findMaxCount();
        }

        private void findMaxCount()
        {
            maxUnfiltered = unfilteredRecs.Max(x => x.Kms.Max(y => y.KmDanger));
            countUnfiltered = unfilteredRecs.Sum(x => x.Kms.Count());
            maxFiltered = filteredRecs.Max(x => x.Kms.Max(y => y.KmDanger));
            countFiltered = filteredRecs.Sum(x => x.Kms.Count());
        }

        private void setFilters()
        {
            grouper.ClearFilterMethods();

            for (int i = 0; i < linijosFilters.Count; i++)
            {
                if (!chlbLinijos.CheckedIndices.Contains(i))
                {
                    grouper.addFilterMethod(grouper.discardByLinija(linijosFilters[i]));
                }
            }

            for (int i = 0; i < skodaiFilters.Count; i++)
            {
                if (!chlbSkodai.CheckedIndices.Contains(i))
                {
                    grouper.addFilterMethod(grouper.discardBySkodas(skodaiFilters[i]));
                }
            }

            if (!chbNepagr.Checked)
            {
                grouper.addFilterMethod(grouper.discardNepagr());
            }

            int dienu = Convert.ToInt32(nudLiko.Value);
            grouper.addFilterMethod(grouper.filterByLikoMaziau(dienu));
        }

        private void getInspectables(DateTime date)
        {
            List<object[]> rawRecords = recordFetcher.Fetch(date);
            // check if records != null
            foreach (var obj in rawRecords)
            {
                Inspectable insp;
                try
                {
                    insp = inspFactory.Make(obj);
                    insps.Add(insp);
                }
                catch
                {
                    // inform somehow and skip
                    continue;
                }
            }
        }

        private void chargeInspManager()
        {
            //date = Convert.ToDateTime(Properties.Settings.Default.Date);
            mapping = Properties.Settings.Default.Mapping;
            recordFetcher = new RecordFetcher();
            dangerCalculator = new DangerCalculator();
            IVkodasFactory vkodasFactory = new VkodasFactory(mapping);
            IKoordCalculator koordCalculator = new AccessDbKoordCalculator();
            koordCalculator.Charge();
            grouper = new Grouper();
            inspFactory = new InspectableFactory(date, vkodasFactory, koordCalculator, mapping);
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

        private void pb_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            int imageWidth = pb.Size.Width;
            int imageHeight = pb.Size.Height;

            int chartHeight = imageHeight / 2;
            float unfilteredY0 = imageHeight;
            float filteredY0 = imageHeight / 2;

            Bitmap DrawArea = new Bitmap(pb.Size.Width, pb.Size.Height);
            pb.Image = DrawArea;
            Graphics g = Graphics.FromImage(DrawArea);
            float w = imageWidth * 0.75f / countUnfiltered;

            Pen greenPen = new Pen(Brushes.Green, w);
            Pen redPen = new Pen(Brushes.Red, w);
            Pen axisPen = new Pen(Brushes.Gray, 1);

            int axisHeight = 20;

            Font kmFont = new Font("Calibri", 9);
            SolidBrush kmBrush = new SolidBrush(Color.Black);
            StringFormat kmFormat = new StringFormat();
            kmFormat.LineAlignment = StringAlignment.Near;
            kmFormat.Alignment = StringAlignment.Center;

            Font linijaFont = new Font("Verdana", 14);
            SolidBrush linijaBrush = new SolidBrush(Color.DimGray);
            StringFormat linijaFormat = new StringFormat();
            linijaFormat.LineAlignment = StringAlignment.Near;
            linijaFormat.Alignment = StringAlignment.Near;

            drawChart(g, filteredRecs, redPen, greenPen, axisPen,
                imageWidth * 0.75f / countFiltered,
                imageWidth * 0.25f / countFiltered,
                filteredY0 - axisHeight,
                (chartHeight - axisHeight) * 1.0f / maxFiltered, imageWidth, chartHeight,
                axisHeight / 3, axisHeight / 2,
                kmFont, kmBrush, kmFormat,
                linijaFont, linijaBrush, linijaFormat);

            drawChart(g, unfilteredRecs, redPen, greenPen, axisPen,
                imageWidth * 0.75f / countUnfiltered,
                imageWidth * 0.25f / countUnfiltered,
                unfilteredY0 - axisHeight,
                (chartHeight - axisHeight) * 1.0f / maxUnfiltered, imageWidth, chartHeight,
                axisHeight / 3, axisHeight / 2,
                kmFont, kmBrush, kmFormat,
                linijaFont, linijaBrush, linijaFormat);

            axisPen.Dispose();
            greenPen.Dispose();
            redPen.Dispose();
            kmFont.Dispose();
            kmBrush.Dispose();
            kmFormat.Dispose();
            linijaFont.Dispose();
            linijaBrush.Dispose();
            linijaFormat.Dispose();
            g.Dispose();
        }

        private void drawChart(
            Graphics g, IEnumerable<Grouper.Grouped> grouped,
            Pen redPen, Pen greenPen, Pen axisPen,
            float strokeWidth, float gapWidth,
            float chartY0, float scale, int imgWidth, int chartHeight,
            int smallTickLength, int bigTickLength,
            Font kmFont, SolidBrush kmBrush, StringFormat kmFormat,
            Font linijaFont, SolidBrush linijaBrush, StringFormat linijaFormat)
        {

            g.DrawLine(axisPen, 0, chartY0, imgWidth, chartY0);
            Pen currentPen;
            float x = 0;
            foreach (var lin in grouped)
            {
                g.DrawLine(axisPen, x, chartY0, x, chartY0 - chartHeight + 20);
                g.DrawString(lin.Linija, linijaFont, linijaBrush, x, chartY0 - chartHeight + 30, linijaFormat);
                foreach (var km in lin.Kms)
                {
                    currentPen = km.ContainsOverdued ? redPen : greenPen;
                    km.Y1 = chartY0 - km.KmDanger * scale;
                    km.X = x;
                    km.Y0 = chartY0;

                    g.DrawLine(currentPen, x, km.Y0, x, km.Y1);

                    if (km.Km % 10 == 0)
                    {
                        g.DrawLine(axisPen, x, chartY0, x, chartY0 + bigTickLength);
                    }
                    else if (km.Km % 5 == 0)
                    {
                        g.DrawLine(axisPen, x, chartY0, x, chartY0 + smallTickLength);
                    }

                    if (km.Km % 20 == 0)
                    {
                        g.DrawString(km.Km.ToString(), kmFont, kmBrush, x, chartY0 + smallTickLength, kmFormat);
                    }
                    x = x + strokeWidth + gapWidth;
                }

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
                dangerCalculator.SetParams((float)nudKoef064.Value, (float)nudKoefMain.Value, (float)nudKoefOverdue.Value);
                dangerCalculator.BatchCalculate(insps);
                coefHasChanged = false;
            }

            setFilters();
            filteredRecs = grouper.Group(insps).ToList();
            findMaxCount();
            pb.Invalidate();
        }

        private void writeCollectedStatus()
        {
            int count063 = collected.Where(x => x.Skodas == "06.3").Count();
            int count064 = collected.Where(x => x.Skodas == "06.4").Count();
            slblCollected.Text = $"Pririnkta {collected.Count} vnt.: 06.3 - {count063}, 06.4 - {count064}";
            statusStrip1.Refresh();
        }

        private bool LinesIntersect(PointF mDown, float upX, float upY, float x, float y0, float y1)
        {
            PointF CmP = new PointF(x - mDown.X, y0 - mDown.Y);
            PointF r = new PointF(upX - mDown.X, upY - mDown.Y);
            PointF s = new PointF(0, y1 - y0);

            float CmPxr = CmP.X * r.Y - CmP.Y * r.X;
            float CmPxs = CmP.X * s.Y;
            float rxs = r.X * s.Y;

            if (CmPxr == 0f)
            {
                // Lines are collinear, and so intersect if they have any overlap

                return ((x - mDown.X < 0f) != (x - upX < 0f))
                    || ((y0 - mDown.Y < 0f) != (y0 - upY < 0f));
            }

            if (rxs == 0f)
                return false; // Lines are parallel.

            float rxsr = 1f / rxs;
            float t = CmPxs * rxsr;
            float u = CmPxr * rxsr;

            return (t >= 0f) && (t <= 1f) && (u >= 0f) && (u <= 1f);
        }

        public void collectIntersected(float upX, float upY, bool onlyOverdued)
        {
            foreach (var lin in filteredRecs)
            {
                foreach (var km in lin.Kms)
                {
                    if (LinesIntersect(mouseDown, upX, upY, km.X, km.Y0, km.Y1))
                    {
                        if (onlyOverdued)
                        {
                            collected.AddRange(km.Insps.Where(x => x.Liko < 0));
                        }
                        else
                        {
                            collected.AddRange(km.Insps);
                        }
                        km.Selected = true;
                    }
                }
            }

            collected = collected.Distinct().ToList();
        }


        private void paintLine(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Brushes.Black))
            {
                pen.DashStyle = DashStyle.Dot;
                e.Graphics.DrawLine(pen, mouseDown, mouseCurrent);
            }
        }

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
            }

            if (mouseDown.X > e.X)
            {
                collectIntersected(e.X, e.Y, true);
            }
            else
            {
                collectIntersected(e.X, e.Y, false);
            }
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                pb.Paint += pb_Paint;
            }
        }
    }
}
