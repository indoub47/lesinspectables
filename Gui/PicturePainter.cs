using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gui
{
    public class PicturePainter
    {
        PointF[] points;
        float minX, minY, maxY;
        bool yAxis = false;

        public PicturePainter(params PointF[] pointfs)
        {
            points = pointfs;
            SetPoints(points);
        }

        private void initialize()
        {
            Array.Sort(points, new Comparison<PointF>((a, b) => a.X.CompareTo(b.X)));

            minX = points.First().X;
            minY = points.Min(p => p.Y);
            maxY = points.Max(p => p.Y);
            yAxis = (points.First().X <= 0 && points.Last().X >= 0);
        }

        public void SetPoints(params PointF[] points)
        {
            this.points = points;
            initialize();
        }

        public void picBox_Paint(object sender, PaintEventArgs e)
        {
            //base.OnPaint(e);
            float xScale = e.ClipRectangle.Size.Width / (points.Last().X - minX);
            float yScale = e.ClipRectangle.Size.Height / (points.Max(p => p.Y) - minY);

            Pen axisPen = new Pen(Color.Black, 1);
            Pen thePen = new Pen(Color.DarkBlue, 2);

            if (yAxis)
            {
                e.Graphics.DrawLine(axisPen, translate(new PointF(0, minY)), translate(new PointF(0, maxY)));
            }

            PointF firstPt = translate(points.First());
            for (int i = 0; i < points.Length-1; i++)
            {
                PointF nextPt = translate(points[i + 1]);
                e.Graphics.DrawLine(thePen, firstPt, nextPt);
                firstPt = nextPt;
            }

            thePen.Dispose();
            axisPen.Dispose();

            PointF translate(PointF point)
            {
                return new PointF(
                    xScale * (point.X - minX),
                    e.ClipRectangle.Size.Height - (point.Y - minY) * yScale);
            }
        }
    }
}
