using System.Collections.Generic;
namespace Kzs
{
    public interface IDangerCalculator
    {
        void BatchCalculate(IEnumerable<Inspectable> insps);
        void SetParams(float paramMain, float paramOverdued, float paramThermit);
    }
}