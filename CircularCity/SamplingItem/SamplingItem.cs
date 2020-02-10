namespace SamplingItemBase
{
    public abstract class SamplingItem
    {
        public int SamplingFreqBySeconds;
        public int IntervalAmountRequired;
        protected int Nmins;

        protected double AnalyzedData;
        protected double AvgData;

        public virtual bool Sampling()
        {
            AnalyzedData++;
            return true;
        }

        public virtual bool Analyzing()
        {
            return true;
        }

        /// <summary>
        /// Record Raw Data
        /// </summary>
        /// <returns></returns>
        public virtual bool RecordRawData()
        {
            return true;
        }

        /// <summary>
        /// Record Avg Data
        /// </summary>
        /// <returns></returns>
        public virtual bool RecordAvgData()
        {
            return true;
        }
    }
}
