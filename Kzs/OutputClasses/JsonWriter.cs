using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using System.IO;

namespace Kzs.OutputClasses
{
    public class JSONWriter : CommonWriter
    {
        override public string GetExtensionFilter()
        {
            return "JSON (*.json)|*.json";
        }

        override public void Output(IEnumerable<Inspectable> insps, DateTime forDate, string fileName)
        {

            List<Inspectable> orderedInsps = PreprocessRecords(insps);
            string output = JsonConvert.SerializeObject(orderedInsps);

            try
            {
                File.WriteAllText(fileName, output);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
