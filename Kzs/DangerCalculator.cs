using System;
using System.Collections.Generic;

namespace Kzs
{
    public class DangerCalculator:IDangerCalculator
    {
        private int overdued, thermit, main;
        private int x0, x1, x2;
        private float a0, b0, a1, b1;

        private const int DEFAULT_X0 = -183;
        private const int DEFAULT_X1 = -14;
        private const int DEFAULT_X2 = 0;


        public DangerCalculator()
        {
            main = 1;
            overdued = 1;
            thermit = 1;

            x0 = DEFAULT_X0;
            x1 = DEFAULT_X1;
            x2 = DEFAULT_X2;

            recalculateCoefs(x0, 0, x1, 1, x2, 1);
        }

        private void recalculateCoefs(
            int x0, int y0, int x1, int y1, int x2, int y2)
        {
            a0 = getA(x0, y0, x1, y1);
            b0 = getB(x0, y0, x1, y1);

            a1 = getA(x1, y1, x2, y2);
            b1 = getB(x1, y1, x2, y2);
        }

        private float getA(int x, int y, int xx, int yy)
        {
            return (yy - y) * 1.0f / (xx - x);
        }

        private float getB(int x, int y, int xx, int yy)
        {
            return y - x * getA(x, y, xx, yy);
        }

        private void calculate(Inspectable insp)
        {
            float res = calc(insp.Liko);

            if (insp.Skodas == "06.4")
                res *= thermit;

            if (insp.Vkodas.Pagrindinis)
                res *= main;

            insp.Danger = Convert.ToInt32(Math.Round(res));
        }

        float calc(int liko)
        {
            if (-liko < x0)
                return 0;
            if (-liko < x1)
                return a0 * (-liko) + b0;
            if (-liko < x2)
                return a1 * (-liko) + b1;
            else
                return b1 * overdued; // jeigu overdued, tai nebesikeicia
        }

        public void BatchCalculate(IEnumerable<Inspectable> insps)
        {
            foreach (var insp in insps)
            {
                calculate(insp);
            }
        }

        public void SetParams(
            int x0, int y0, int x1, int y1, int x2, int y2,
            int pMain, int pOverdued, int pThermit)
        {
            main = pMain;
            overdued = pOverdued;
            thermit = pThermit;
            this.x0 = x0;
            this.x1 = x1;
            this.x2 = x2;

            recalculateCoefs(x0, y0, x1, y1, x2, y2);

        }
    }
}
