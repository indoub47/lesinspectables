﻿using System;
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
using Gui.Properties;

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
        IInspectableOutputter outputter;
        PicturePainter paramPainter;

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

        float strokePart;
        float filteredChartPart;
        float unfilteredChartPart;
        float maxStrokeWidth;
        float xAxisHeight;
        

        public MainForm()
        {
            InitializeComponent();
            coefHasChanged = false;
            outputter = new CsvWriter(
                Settings.Default.OutputDir, 
                Settings.Default.OutputExelFName);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Settings - to controls
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
            skodaiFilters = Settings.Default.Skodai.ToList();
            skodaiFilters.ForEach(x => chlbSkodai.Items.Add(x));
            for (int i = 0; i < chlbSkodai.Items.Count; i++)
            {
                chlbSkodai.SetItemChecked(i, true);
            }

            linijosFilters = Settings.Default.Linijos.ToList();
            linijosFilters.ForEach(x => chlbLinijos.Items.Add(x));
            for (int i = 0; i < chlbLinijos.Items.Count; i++)
            {
                chlbLinijos.SetItemChecked(i, true);
            }

            chbNepagr.Checked = true;

            nudLiko.Value = nudLiko.Maximum;
            // end of Settings to controls


            // setup Inspectables Factory
            mapping = Settings.Default.Mapping;

            recordFetcher = new RecordFetcher(
                string.Format(
                    Settings.Default.ConnectionString,
                    Settings.Default.MainDbFileName));

            dangerCalculator = new DangerCalculator();
            dangerCalculator.SetParams(koefX0, koefY0, koefX1, koefY1, koefX2, koefY2, koefMain, koefOverdue, koef064);

            IVkodasFactory vkodasFactory = new VkodasFactory(mapping);

            IKoordCalculator koordCalculator = new AccessDbKoordCalculator(
                string.Format(
                    Settings.Default.ConnectionString,
                    Settings.Default.OptionsDbFileName));
            koordCalculator.Charge();

            grouper = new Grouper();

            inspFactory = new InspectableFactory(date, vkodasFactory, koordCalculator, mapping);
            // end of Setup Inspectable Factory


            // Parameters for main chart painting
            strokePart = Settings.Default.StrokePart;            
            unfilteredChartPart = Settings.Default.UnfilteredPart;
            filteredChartPart = 1 - unfilteredChartPart;
            maxStrokeWidth = Settings.Default.MaxStrokeWidht;
            xAxisHeight = Settings.Default.AxisHeight;
            // end of Parameters for main chart painting

            // Liko-picture painter
            paramPainter = new PicturePainter(
                new PointF(koefX0, koefY0), 
                new PointF(koefX1, koefY1), 
                new PointF(koefX2, koefY2));
            // end of Liko-picture painter            

            // Load groupped Inspectable lists
            getInspectables(date);
            dangerCalculator.BatchCalculate(insps);
            grouper.ClearFilterMethods();
            unfilteredRecs = grouper.Group(insps).ToList();
            setFilters();
            filteredRecs = grouper.Group(insps).ToList();
            findMaxCount();
            // end of Load groupped Inspectable lists


            nudX1.ValueChanged += new EventHandler(nudX1_ValueChanged);
            nudY1.ValueChanged += new EventHandler(nudY1_ValueChanged);
            nudY2.ValueChanged += new EventHandler(nudY2_ValueChanged);
            pb.MouseDown += pb_MouseDown;
            pb.MouseMove += pb_MouseMove;
            pb.MouseUp += pb_MouseUp;
            pb.Paint += pb_Paint;
            pbxDangerParameters.Paint += paramPainter.picBox_Paint;

            // paint
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

        private void pb_Paint(object sender, PaintEventArgs e)
        {
            float pbWidth = pb.Width;
            base.OnPaint(e);
            float fChartHeight = e.ClipRectangle.Height * filteredChartPart - xAxisHeight;
            float unfChartHeight = e.ClipRectangle.Height * unfilteredChartPart - xAxisHeight;

            float fKmWidth = pbWidth / countFiltered;
            float unfKmWidth = pbWidth / countUnfiltered;

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


            redPen.Width = greenPen.Width = orangePen.Width = Math.Min(fKmWidth * strokePart, maxStrokeWidth);
            drawChart(filteredRecs,
                pbWidth / countFiltered,
                fChartHeight,
                fChartHeight / maxFiltered,
                fChartHeight, 
                xAxisHeight / 5, xAxisHeight / 3, xAxisHeight / 2);

            redPen.Width = greenPen.Width = Math.Min(unfKmWidth * strokePart, maxStrokeWidth);
            drawChart(unfilteredRecs,
                pbWidth / countUnfiltered,
                e.ClipRectangle.Height - xAxisHeight,
                unfChartHeight / maxUnfiltered,
                unfChartHeight,
                xAxisHeight / 5, xAxisHeight / 3, xAxisHeight / 2);

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

            void drawChart(
            IEnumerable<Grouper.Grouped> grouped, 
            float kmWidth,
            float chartY0, 
            float scale, 
            float chartHeight,
            float smallestTickLength, float smallTickLength, float bigTickLength)
            {
                e.Graphics.DrawLine(axisPen, 0, chartY0, pbWidth, chartY0);
                Pen currentPen;
                float x = 0;
                foreach (var lin in grouped)
                {
                    e.Graphics.DrawLine(axisPen, x, chartY0, x, chartY0 - chartHeight + 20);
                    e.Graphics.DrawString(lin.Linija, linijaFont, linijaBrush, x, chartY0 - chartHeight + 30, linijaFormat);
                    foreach (var km in lin.Kms)
                    {
                        if (km.POptions.Selected)
                        {
                            currentPen = orangePen;
                        }
                        else if (km.POptions.ContainsOverdued)
                        {
                            currentPen = redPen;
                        }
                        else
                        {
                            currentPen = greenPen;
                        }
                        
                        km.POptions.Y1 = chartY0 - km.KmDanger * scale;
                        km.POptions.X = x;
                        km.POptions.Y0 = chartY0;

                        e.Graphics.DrawLine(currentPen, x, km.POptions.Y0, x, km.POptions.Y1);

                        if (km.Km % 10 == 0)
                        {
                            e.Graphics.DrawLine(axisPen, x, chartY0, x, chartY0 + bigTickLength);
                        }
                        else if (km.Km % 5 == 0)
                        {
                            e.Graphics.DrawLine(axisPen, x, chartY0, x, chartY0 + smallTickLength);
                        }
                        else
                        {
                            e.Graphics.DrawLine(axisPen, x, chartY0, x, chartY0 + smallestTickLength);
                        }

                        if (km.Km % 20 == 0)
                        {
                            e.Graphics.DrawString(km.Km.ToString(), kmFont, kmBrush, x, chartY0 + smallTickLength, kmFormat);
                        }
                        x = x + kmWidth;
                    }

                }
            }

        }        

        private void writeCollectedStatus()
        {
            int count063 = collected.Where(x => x.Skodas == "06.3").Count();
            int count064 = collected.Where(x => x.Skodas == "06.4").Count();
            slblCollected.Text = $"Pririnkta {collected.Count} vnt.: 06.3 - {count063}, 06.4 - {count064}";
            statusStrip1.Refresh();
        }

        private bool linesIntersect(PointF mDown, float upX, float upY, float x, float y0, float y1)
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

        public void collectIntersected(float upX, float upY)
        {
            foreach (var lin in filteredRecs)
            {
                foreach (var km in lin.Kms)
                {
                    if (linesIntersect(mouseDown, upX, upY, km.POptions.X, km.POptions.Y0, km.POptions.Y1))
                    {
                        collected.AddRange(km.Insps);
                        km.POptions.Selected = true;
                    }
                }
            }

            collected = collected.Distinct().ToList();
        }

        private void paintLine(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Brushes.Black))
            {
                pen.DashStyle = DashStyle.Dash;
                e.Graphics.DrawLine(pen, mouseDown, mouseCurrent);
            }
        }        
    }
}
