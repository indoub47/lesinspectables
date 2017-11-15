using System.Collections.Generic;
namespace Kzs
{
    public interface IDangerCalculator
    {
        void BatchCalculate(IEnumerable<Inspectable> insps);
        void SetParams(int x0, int y0, int x1, int y1, int x2, int y2,
            int paramMain, int paramOverdued, int paramThermit);
    }
}