using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;


namespace InspectedOnes.OutputClasses
{
    public class JSONK40Writer : CommonK40Writer
    {
        readonly string[] dbMapping;

        public JSONK40Writer()
        {
            dbMapping = Properties.Settings.Default.MappingDb;
        }

        override public string GetExtensionFilter()
        {
            return "JSON (*.json)|*.json";
        }

        override public void Output(List<object[]> rawRecords, string destFileName)
        {

            List<object[]> records = PreprocessRecords(rawRecords, dbMapping);
            string output = JsonConvert.SerializeObject(records);

            try
            {
                File.WriteAllText(destFileName, output);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
