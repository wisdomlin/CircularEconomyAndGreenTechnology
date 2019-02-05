using DBInMemoryEngine;
using System;
using System.Linq;

namespace SamplingItemBase
{
    public class Opacity : SamplingItem
    {
        public Opacity()
        {
            IntervalAmountRequired = 36;
            Nmins = 6;
            AnalyzedData = 0;
        }

        
        public override bool RecordRawData()
        {
            try
            {
                RawDataStore.Instance.AddRawData(AnalyzedData);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool RecordAvgData()
        {
            try
            {
                RawDataStore.Instance.GetNminsRawData(DateTime.Now, Nmins, out double[] rows);
                if (rows.Length >= IntervalAmountRequired)
                {
                    AvgData = rows.Average();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return false;
            }
        }
    }
}
