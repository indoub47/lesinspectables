using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kzs.Properties;

namespace Kzs
{
    public class DangerCalculator:IDangerCalculator
    {
        private float x0, y0, x1, y1, x2, y2, overdued, thermit, main;
        private float a0, b0, a1, b1;

        public DangerCalculator()
        {
            main = 1;
            overdued = 1;
            thermit = 1;

            x0 = UserSettings.Default.X0;
            y0 = UserSettings.Default.Y0;
            x1 = UserSettings.Default.X1;
            y1 = UserSettings.Default.Y1;
            x2 = UserSettings.Default.X2;
            y2 = UserSettings.Default.Y2;

            a0 = getA(x0, y0, x1, y1);
            b0 = getB(x0, y0, x1, y1);

            a1 = getA(x1, y1, x2, y2);
            b1 = getB(x1, y1, x2, y2);
        }

        private float getA(float x, float y, float xx, float yy)
        {
            return (yy - y) / (xx - x);
        }

        private float getB(float x, float y, float xx, float yy)
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

        public void SetParams(float pMain, float pOverdued, float pThermit)
        {
            main = pMain;
            overdued = pOverdued;
            thermit = pThermit;
        }
    }
}
