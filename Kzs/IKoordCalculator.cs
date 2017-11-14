using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    public interface IKoordCalculator
    {
        // grą-ina linijos kodą ir koordinatę
        Tuple<string, int> Calculate(ulong id, Vk vk);

        void Charge();
    }
}
