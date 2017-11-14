using System;
using System.Collections.Generic;

namespace Kzs
{
    public class VkodasFactory:IVkodasFactory
    {
        private string[] mapping;

        public VkodasFactory(string[] mapping)
        {
            this.mapping = mapping;
        }

        public Vk Make(object[] rec)
        {
            long id = Convert.ToInt64(rec[Array.IndexOf(mapping, "id")]);
            string linija = Convert.ToString(rec[Array.IndexOf(mapping, "linija")]);
            int kelias = Convert.ToInt32(rec[Array.IndexOf(mapping, "kelias")]);
            int km = Convert.ToInt32(rec[Array.IndexOf(mapping, "km")]);

            int pk;
            object objPk = rec[Array.IndexOf(mapping, "pk")];
            if (objPk == null)
            {
                pk = 0;
            }
            else
            {
                pk = Convert.ToInt32(objPk);
            }

            int m = Convert.ToInt32(rec[Array.IndexOf(mapping, "m")]);

            int? siule;
            object objSiule = rec[Array.IndexOf(mapping, "siule")];
            if (objSiule == null)
            {
                siule = null;
            }
            else
            {
                siule = Convert.ToInt32(objSiule);
            }

            return new Vk(id, linija, kelias, km, pk, m, siule);
        }
    }
}