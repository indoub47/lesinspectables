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

        int koefX0, koefY0, koefX1, koefY1, koefX2, koefY2;
        int koefMain, koefOverdue, koef064;

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

        float strokePart;
        float gapPart;
        float filteredChartPart;
        float unfilteredChartPart;
        float maxStrokeWidth;
        float xAxisHeight;

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
            populateSettings();

            strokePart = Properties.Settings.Default.StrokePart;
            gapPart = 1 - strokePart;
            unfilteredChartPart = Properties.Settings.Default.UnfilteredPart;
            filteredChartPart = 1 - unfilteredChartPart;
            maxStrokeWidth = Properties.Settings.Default.MaxStrokeWidht;
            xAxisHeight = Properties.Settings.Default.AxisHeight;


            chargeInspManager();
            getInspectables(date);
            dangerCalculator.SetParams(koefX0, koefY0, koefX1, koefY1, koefX2, koefY2, koefMain, koefOverdue, koef064);
            dangerCalculator.BatchCalculate(insps);
            grouper.ClearFilterMethods();
            unfilteredRecs = grouper.Group(insps).ToList();
            setFilters();
            filteredRecs = grouper.Group(insps).ToList();
            findMaxCount();

            pb.MouseDown += pb_MouseDown;
            pb.MouseMove += pb_MouseMove;
            pb.MouseUp += pb_MouseUp;

            pb.Paint += pb_Paint;
            pb.Invalidate();
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
            string mainDbConnString = string.Format(
                Properties.Settings.Default.ConnectionString,
                Properties.Settings.Default.MainDbFileName);
            recordFetcher = new RecordFetcher(mainDbConnString);
            dangerCalculator = new DangerCalculator();
            IVkodasFactory vkodasFactory = new VkodasFactory(mapping);
            string optionsDbConnString = string.Format(
                Properties.Settings.Default.ConnectionString,
                Properties.Settings.Default.OptionsDbFileName
                );
            IKoordCalculator koordCalculator = new AccessDbKoordCalculator(optionsDbConnString);
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
            float imageWidth = pb.Size.Width * 1.0f;
            float imageHeight = pb.Size.Height * 1.0f;


            float fChartHeight = imageHeight * filteredChartPart - xAxisHeight;
            float unfChartHeight = imageHeight * unfilteredChartPart - xAxisHeight;
            float fY0 = fChartHeight;
            float unfY0 = imageHeight - xAxisHeight;

            float fKmWidth = imageWidth / countFiltered;
            float fStrokeW = Math.Min(fKmWidth * strokePart, maxStrokeWidth);

            float unfKmWidth = imageWidth * 1.0f / countUnfiltered;
            float unfStrokeW = Math.Min(unfKmWidth * strokePart, maxStrokeWidth);


            Bitmap DrawArea = new Bitmap(pb.Size.Width, pb.Size.Height);
            pb.Image = DrawArea;
            Graphics g = Graphics.FromImage(DrawArea);

            Pen greenPen = new Pen(Brushes.Green);
            Pen redPen = new Pen(Brushes.Red);
            Pen orangePen = new Pen(Brushes.Orange);

            Pen axisPen = new Pen(Brushes.Gray, 1.0f);

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


            redPen.Width = greenPen.Width = fStrokeW;
            drawChart(g, filteredRecs, redPen, greenPen, axisPen, fKmWidth,
                fY0,
                fChartHeight / maxFiltered, imageWidth, 
                fChartHeight, 
                xAxisHeight / 5, xAxisHeight / 3, xAxisHeight / 2,
                kmFont, kmBrush, kmFormat,
                linijaFont, linijaBrush, linijaFormat);

            redPen.Width = greenPen.Width = unfStrokeW;
            drawChart(g, unfilteredRecs, redPen, greenPen, axisPen, unfKmWidth,
                unfY0,
                unfChartHeight / maxUnfiltered, imageWidth, 
                unfChartHeight,
                xAxisHeight / 5, xAxisHeight / 3, xAxisHeight / 2,
                kmFont, kmBrush, kmFormat,
                linijaFont, linijaBrush, linijaFormat);

            axisPen.Dispose();
            greenPen.Dispose();
            redPen.Dispose();
            orangePen.Dispose();
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
            Pen redPen, Pen greenPen, Pen axisPen, float kmWidth,
            float chartY0, float scale, float imgWidth, float chartHeight,
            float smallestTickLength, float smallTickLength, float bigTickLength,
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
                    else
                    {
                        g.DrawLine(axisPen, x, chartY0, x, chartY0 + smallestTickLength);
                    }

                    if (km.Km % 20 == 0)
                    {
                        g.DrawString(km.Km.ToString(), kmFont, kmBrush, x, chartY0 + smallTickLength, kmFormat);
                    }
                    x = x + kmWidth;
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
                dangerCalculator.SetParams(
                    nudX0.Value, nudY0.Value,
                    nudX1.Value, nudY1.Value,
                    nudX2.Value, nudY2.Value,
                    nudKoef064.Value, nudKoefMain.Value, nudKoefOverdue.Value);
                dangerCalculator.BatchCalculate(insps);
                coefHasChanged = false;
                unfilteredRecs = grouper.Group(insps).ToList();
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
