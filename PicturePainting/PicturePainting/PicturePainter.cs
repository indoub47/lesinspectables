using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PicturePainting
{
    public class PicturePainter
    {
        PointF[] points;
        float minX, minY, maxY;
        bool yAxis = false;

        public PicturePainter(PictureBox picBox, params PointF[] pointfs)
        {
            points = pointfs;

            Array.Sort(points, new Comparison<PointF>((a, b) => a.X.CompareTo(b.X)));

            minX = points.First().X;
            minY = points.Min(p => p.Y);
            maxY = points.Max(p => p.Y);
            yAxis = (points.First().X <= 0 && points.Last().X >= 0);

        }

        private void picBox_Paint(object sender, PaintEventArgs e)
        {
            float xScale = pb.Size.Width / (points.Last().X - minX);
            float yScale = pb.Size.Height / (points.Max(p => p.Y) - minY);

            Bitmap drawArea = new Bitmap(pb.Size.Width, pb.Size.Height);
            pb.Image = drawArea;
            Graphics g = Graphics.FromImage(drawArea);
            Pen axisPen = new Pen(Color.Black, 1);
            Pen thePen = new Pen(Color.BurlyWood, 2);

            if (yAxis)
            {
                g.DrawLine(axisPen, translate(new PointF(0, minY)), translate(new PointF(0, maxY)));
            }


            PointF firstPt = translate(points.First());
            for (int i = 0; i < points.Length-1; i++)
            {
                PointF nextPt = translate(points[i + 1]);
                g.DrawLine(thePen, firstPt, nextPt);
                firstPt = nextPt;
            }

            thePen.Dispose();
            g.Dispose();

            PointF translate(PointF point)
            {
                return new PointF(
                    (point.X - minX) * xScale,
                    pb.Size.Height - (point.Y - minY) * yScale);
            }
        }
    }
}
