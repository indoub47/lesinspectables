using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using Kzs;
using Kzs.OutputClasses;
using System.Drawing.Drawing2D;
using Gui.Properties;
using System.IO;

namespace Gui
{
    public partial class MainForm : Form
    {
        Point mouseDown;
        Point mouseCurrent;

        int koefX0, koefY0, koefX1, koefY1, koefX2, koefY2;
        int koefMain, koefOverdue, koef064;

        Brush brushNoOverdued = new SolidBrush(Properties.Settings.Default.ColorNoOverdued);
        Brush brushSomeOverdued = new SolidBrush(Properties.Settings.Default.ColorSomeOverdued);
        Brush brushAllOverdued = new SolidBrush(Properties.Settings.Default.ColorAllOverdued);
        Brush brushSelected = new SolidBrush(Properties.Settings.Default.ColorSelected);

        bool recalculateDanger;
        DateTime date;
        string[] mapping;

        IRecordFetcher recordFetcher;
        InspectableFactory inspFactory;
        IDangerCalculator dangerCalculator;
        Grouper grouper;
        //IInspectableOutputter outputter;
        PicturePainter paramPainter;

        List<Inspectable> insps = new List<Inspectable>();
        List<Grouper.Grouped> unfilteredRecs;
        List<Grouper.Grouped> filteredRecs;
        List<string> linijosFilters;
        List<string> skodaiFilters;

        List<Inspectable> collected = new List<Inspectable>();
        List<Grouper.Grouped.KmInsps> freshCollected;

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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {            
            // Settings - to controls
            txbHelperDbPath.Text = Settings.Default.OptionsDbFileName;
            txbMainDbPath.Text = Settings.Default.MainDbFileName;

            nudX0.Value = koefX0 = Settings.Default.koefX0;
            nudY0.Value = koefY0 = Settings.Default.koefY0;
            nudX1.Value = koefX1 = Settings.Default.koefX1;
            nudY1.Value = koefY1 = Settings.Default.koefY1;
            nudX2.Value = koefX2 = Settings.Default.koefX2;
            nudY2.Value = koefY2 = Settings.Default.koefY2;
            nudKoefMain.Value = koefMain = Settings.Default.CoefMain;
            nudKoefOverdue.Value = koefOverdue = Settings.Default.CoefOverdued;
            nudKoef064.Value = koef064 = Settings.Default.CoefThermit;

            if (Settings.Default.ExportFormat == "xlsx")
            {
                rbExportXlsx.Checked = true;
            }
            // other formats go here: else if (Settings.Default.ExportFormat == "other") {rbExportOther.Checked = true;}
            else
            {
                rbExportCsv.Checked = true;
            }

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

            // Create other components

            paramPainter = new PicturePainter(
                new PointF(koefX0, koefY0), 
                new PointF(koefX1, koefY1), 
                new PointF(koefX2, koefY2));
            // end of Create other components

            // Form private fields
            recalculateDanger = false;
            // end of Form private fields

            // Parameters for main chart painting
            strokePart = Settings.Default.StrokePart;            
            unfilteredChartPart = Settings.Default.UnfilteredPart;
            filteredChartPart = 1 - unfilteredChartPart;
            maxStrokeWidth = Settings.Default.MaxStrokeWidht;
            xAxisHeight = Settings.Default.AxisHeight;
            // end of Parameters for main chart painting  
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (!File.Exists(Properties.Settings.Default.MainDbFileName))
            {
                MessageBox.Show($"Suvirinimų duomenų bazės failas \"{Settings.Default.MainDbFileName}\" neegzistuoja.\nNurodykite suvirinimų duomenų bazės failą ir paleiskite programą iš naujo.",
                    "Crucial file not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!File.Exists(Properties.Settings.Default.OptionsDbFileName))
            {
                MessageBox.Show($"Pagalbinės duomenų bazės failas \"{Settings.Default.OptionsDbFileName}\" neegzistuoja.\nNurodykite pagalbinės duomenų bazės failą ir paleiskite programą iš naujo.",
                    "Crucial file not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Application.UseWaitCursor = true;
            splitContainer1.Panel1.Enabled = false;

            // Load data
            try
            {
                getInspectables(date); //Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data fetching error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }            
            dangerCalculator.BatchCalculate(insps);// Thread.Sleep(1000);            
            grouper.ClearFilterMethods(); //Thread.Sleep(1000);
            try
            {
                unfilteredRecs = grouper.Group(insps);// Thread.Sleep(1000);
                // gali būti neteisingai įvestos linijos, tai išmes exception,
                // kad susitvarkyti db
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Data processing error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            setFilters(); //Thread.Sleep(1000);
            filteredRecs = grouper.Group(insps);// Thread.Sleep(1000);
            findMaxCount();// Thread.Sleep(1000);
            // end of Load data

            nudY0.ValueChanged += new EventHandler(nudY0_ValueChanged);
            nudX1.ValueChanged += new EventHandler(nudX1_ValueChanged);
            nudY1.ValueChanged += new EventHandler(nudY1_ValueChanged);
            nudY2.ValueChanged += new EventHandler(nudY2_ValueChanged);

            rbExportCsv.CheckedChanged += new EventHandler(rbExportCsv_CheckedChanged);
            rbExportXlsx.CheckedChanged += new EventHandler(rbExportXlsx_CheckedChanged);

            btnAllOverduedColor.BackColor = Settings.Default.ColorAllOverdued;
            btnNoOverduedColor.BackColor = Settings.Default.ColorNoOverdued;
            btnSomeOverduedColor.BackColor = Settings.Default.ColorSomeOverdued;
            btnSelectedColor.BackColor = Settings.Default.ColorSelected;

            pb.MouseDown += pb_MouseDown;
            pb.MouseUp += pb_MouseUp;
            pb.Paint += pb_Paint;
            pbxDangerParameters.Paint += paramPainter.picBox_Paint;
            pb.Invalidate();

            splitContainer1.Panel1.Enabled = true;
            Application.UseWaitCursor = false;
        }

        private void findMaxCount()
        {
            countUnfiltered = unfilteredRecs.Sum(x => x.Kms.Count());
            if (countUnfiltered > 0)
            {
                maxUnfiltered = unfilteredRecs.Max(x => x.Kms.Max(y => y.KmDanger));
            }
            else
            {
                maxUnfiltered = -1;
            }

            countFiltered = filteredRecs.Sum(x => x.Kms.Count());
            if (countFiltered > 0)
            {
                maxFiltered = filteredRecs.Max(x => x.Kms.Max(y => y.KmDanger));
            }
            else
            {
                maxFiltered = -1;
            }
            
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
            insps.Clear();
            List<object[]> rawRecords = recordFetcher.Fetch(date);
            StringBuilder sbErrors = new StringBuilder();
            // check if records != null
            foreach (var obj in rawRecords)
            {
                Inspectable insp;
                try
                {
                    insp = inspFactory.Make(obj);
                    insps.Add(insp);
                }
                catch (Exception ex)
                {
                    // inform somehow and skip
                    sbErrors.AppendLine(ex.Message);
                    continue;
                }
            }
            if (sbErrors.Length > 0)
            {
                MessageBox.Show(sbErrors.ToString());
            }
        }

        private void pb_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            float pbWidth = pb.Width;
            float fChartHeight = e.ClipRectangle.Height * filteredChartPart - xAxisHeight;
            float unfChartHeight = e.ClipRectangle.Height * unfilteredChartPart - xAxisHeight;            

            Pen penNoOverdued = new Pen(brushNoOverdued);
            Pen penAllOverdued = new Pen(brushAllOverdued);
            Pen penSomeOverdued = new Pen(brushSomeOverdued);
            Pen penSelected = new Pen(brushSelected);

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
            
            if (maxFiltered != -1)
            {
                float fKmWidth = pbWidth / countFiltered;
                penAllOverdued.Width = penNoOverdued.Width = penSomeOverdued.Width = penSelected.Width = Math.Min(fKmWidth * strokePart, maxStrokeWidth);
                drawChart(filteredRecs,
                    pbWidth / countFiltered,
                    fChartHeight,
                    fChartHeight / maxFiltered,
                    fChartHeight,
                    xAxisHeight / 5, xAxisHeight / 3, xAxisHeight / 2);
            }
            
            if (maxUnfiltered != -1)
            {
                float unfKmWidth = pbWidth / countUnfiltered;
                penAllOverdued.Width = penNoOverdued.Width = Math.Min(unfKmWidth * strokePart, maxStrokeWidth);
                drawChart(unfilteredRecs,
                    pbWidth / countUnfiltered,
                    e.ClipRectangle.Height - xAxisHeight,
                    unfChartHeight / maxUnfiltered,
                    unfChartHeight,
                    xAxisHeight / 5, xAxisHeight / 3, xAxisHeight / 2);
            }

            axisPen.Dispose();
            penNoOverdued.Dispose();
            penAllOverdued.Dispose();
            penSelected.Dispose();
            penSomeOverdued.Dispose();
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
                    e.Graphics.DrawLine(axisPen, x, chartY0 + bigTickLength, x, chartY0 - chartHeight + 20);
                    e.Graphics.DrawString(lin.Linija, linijaFont, linijaBrush, x, chartY0 - chartHeight + 30, linijaFormat);
                    foreach (var km in lin.Kms)
                    {
                        if (km.POptions.Selected)
                        {
                            currentPen = penSelected;
                        }
                        else if (km.POptions.Overdued == 1)
                        {
                            currentPen = penAllOverdued;
                        }
                        else if (km.POptions.Overdued == 0)
                        {
                            currentPen = penSomeOverdued;
                        }
                        else
                        {
                            currentPen = penNoOverdued;
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
            int count063over = collected.Where(x => x.Skodas == "06.3" && x.Liko < 0).Count();
            int count064 = collected.Where(x => x.Skodas == "06.4").Count();
            int count064over = collected.Where(x => x.Skodas == "06.4" && x.Liko < 0).Count();
            slblCollected.Text = $"Pririnkta {collected.Count} vnt.: 06.3 - {count063} ({count063over}), 06.4 - {count064} ({count064over})";
            statusStrip1.Refresh();
        }

        private bool rectIntersect(PointF mDown, float mUpX, float mUpY, float x, float y0, float y1)
        {
            float mLeftX, mRightX, mTopY, mBottomY;
            if (mUpX > mDown.X)
            {
                mLeftX = mDown.X;
                mRightX = mUpX;
            }
            else
            {
                mLeftX = mUpX;
                mRightX = mDown.X;
            }

            if (mUpY > mDown.Y)
            {
                mBottomY = mUpY;
                mTopY = mDown.Y;
            }
            else
            {
                mBottomY = mDown.Y;
                mTopY = mUpY;
            }

            return (mLeftX <= x && mRightX >= x) && (mTopY <= y0 && mBottomY >= y1);
        }

        private void paintRect(object sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Brushes.Black))
            {
                pen.DashStyle = DashStyle.Dash;
                float x, y, rectWidth, rectHeight;
                if (mouseCurrent.X >= mouseDown.X)
                {
                    x = mouseDown.X;
                    rectWidth = mouseCurrent.X - mouseDown.X;
                }
                else
                {
                    x = mouseCurrent.X;
                    rectWidth = mouseDown.X - mouseCurrent.X;
                }

                if (mouseCurrent.Y >= mouseDown.Y)
                {
                    y = mouseDown.Y;
                    rectHeight = mouseCurrent.Y - mouseDown.Y;
                }
                else
                {
                    y = mouseCurrent.Y;
                    rectHeight = mouseDown.Y - mouseCurrent.Y;
                }

                e.Graphics.DrawRectangle(pen, x, y, rectWidth, rectHeight);
            }
        }
    }
}
