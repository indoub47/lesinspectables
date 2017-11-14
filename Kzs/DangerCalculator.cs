using System;
using System.Collections.Generic;

namespace Kzs
{
    public class DangerCalculator:IDangerCalculator
    {
        private decimal overdued, thermit, main;
        private decimal x0, x1, x2;
        private decimal a0, b0, a1, b1;

        private const decimal DEFAULT_X0 = -183;
        private const decimal DEFAULT_X1 = -14;
        private const decimal DEFAULT_X2 = 0;


        public DangerCalculator()
        {
            main = 1;
            overdued = 1;
            thermit = 1;

            this.x0 = DEFAULT_X0;
            this.x1 = DEFAULT_X1;
            this.x2 = DEFAULT_X2;

            recalculateCoefs(x0, 0, x1, 1, x2, 1);
        }

        private void recalculateCoefs(
            decimal x0, decimal y0, decimal x1, decimal y1, decimal x2, decimal y2)
        {
            a0 = getA(x0, y0, x1, y1);
            b0 = getB(x0, y0, x1, y1);

            a1 = getA(x1, y1, x2, y2);
            b1 = getB(x1, y1, x2, y2);
        }

        private decimal getA(decimal x, decimal y, decimal xx, decimal yy)
        {
            return (yy - y) / (xx - x);
        }

        private decimal getB(decimal x, decimal y, decimal xx, decimal yy)
        {
            return y - x * getA(x, y, xx, yy);
        }

        private void calculate(Inspectable insp)
        {
            decimal res = calc(insp.Liko);

            if (insp.Skodas == "06.4")
                res *= thermit;

            if (insp.Vkodas.Pagrindinis)
                res *= main;

            insp.Danger = Convert.ToInt32(Math.Round(res));
        }

        decimal calc(int liko)
        {
            if (-liko < x0)
                return 0;
            if (-liko < x1)
                return a0 * (-liko) + b0;
            if (-liko < x2)
                return a1 * (-liko) + b1;
            else
                return overdued;
        }

        public void BatchCalculate(IEnumerable<Inspectable> insps)
        {
            foreach (var insp in insps)
            {
                calculate(insp);
            }
        }

        public void SetParams(
            decimal x0, decimal y0, decimal x1, decimal y1, decimal x2, decimal y2,
            decimal pMain, decimal pOverdued, decimal pThermit)
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
