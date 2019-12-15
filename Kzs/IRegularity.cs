using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs
{
    public interface IRegularity
    {
        /*
         * Kiek dienų liko nuo toDate iki tikrinimo lango pabaigos
         */
        int GetDaysLeft(DateTime baseDate, Kelintas whichToPerform, DateTime toDate);

        /*
         * Tikrinimo lango pradžia
         */
        DateTime GetDateFrom(DateTime baseDate, Kelintas whichToPerform);

        /*
         * Tikrinimo lango pabaiga
         */
        DateTime GetDateTo(DateTime baseDate, Kelintas whichToPerform);

        /*
         * DateX - tai yra data, kuri gaunama nuo kažkokios konkrečios datos atėmus
         * periodą, kuris skiria atraminę datą (baseDate) ir tikrinimo lango pradžią.
         * Jeigu baseDate yra ankstesnė nei DateX, suvirinimą galima tikrinti,
         * o jeigu vėlesnė, suvirinimą tikrinti dar per anksti
         */
        DateTime GetDateX(DateTime toDate, Kelintas whichToPerform);

        /* turi partempti Inpsectables:
         * 1. įrašai, kurių skodas yra 06.3 arba 06.4
         * 2. įrašai, kuriems neatliktas tikrinimas
         * 3. įrašai, kuriuos galima tikrinti
         */
        string[] GetSQLTails();
    }
}
