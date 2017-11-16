using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kzs.OutputClasses
{
    public interface IInspectableOutputter
    {
        void Output(IEnumerable<Inspectable>insps, DateTime forDate);
        string GetFileName();
        void SetOutputDir(string outputDir);
    }
}
