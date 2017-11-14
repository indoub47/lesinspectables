using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    [Serializable()]
    public class Inspectable
    {
        public ulong Id { get; }
        public string Linija { get; }
        public Vk Vkodas { get; }
        public long Koord { get; }
        public string Skodas { get; }
        public Kelintas Ktas { get; }
        public int Liko { get; }
        public int Danger { get; internal set; }

        public Inspectable(ulong id, string linija, Vk vkodas, long koord, string skodas, Kelintas ktas, int liko)
        {
            Id = id;
            Linija = linija;
            Vkodas = vkodas;
            Koord = koord;
            Skodas = skodas;
            Ktas = ktas;
            Liko = liko;
            Danger = -1;
        }
    }
}
