using System.Collections.Generic;
namespace Kzs
{
    public interface IDangerCalculator
    {
        void BatchCalculate(IEnumerable<Inspectable> insps);
        void SetParams(decimal x0, decimal y0, decimal x1, decimal y1, decimal x2, decimal y2,
            decimal paramMain, decimal paramOverdued, decimal paramThermit);
    }
}