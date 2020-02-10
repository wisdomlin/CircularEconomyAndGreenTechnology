using CAMS;
using DBInMemoryEngine;
using NUnit.Framework;
using SamplingItemBase;
using System.Linq;
using System.Xml.Linq;
using Assert = NUnit.Framework.Assert;

namespace HelloTestNamespace1
{
    [TestFixture]
    public class CAMSTests
    {
        //[Test]
        //public void CamsParticulateMatters_TestNew_NotNull()
        //{
        //    CAMS_ParticulateMatters CAMS_PM = new CAMS_ParticulateMatters();
        //    CAMS_PM.SamplingItemList.Add(new Opacity());
        //    Assert.IsNotNull(CAMS_PM);
        //}

        [Test]
        public void CamsParticulateMatters_SamplingItemFactory_NotNull()
        {
            CAMS_ParticulateMatters CAMS_PM = new CAMS_ParticulateMatters();
            Assert.IsNotNull(CAMS_PM);
        }

        [Test]
        public void SamplingFrequency_SamplingFrequency_NotOverSpec()
        {
            CAMS_ParticulateMatters CAMS_PM = new CAMS_ParticulateMatters();

            foreach (SamplingItem item in CAMS_PM.SamplingItemList)
            {
                bool res = true;
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();                
                res &= item.Sampling();
                res &= item.Analyzing();
                res &= item.RecordRawData();
                stopwatch.Stop();
                Assert.IsTrue(
                    stopwatch.Elapsed.TotalSeconds < item.SamplingFreqBySeconds);
                Assert.IsTrue(res);
            }
        }

        [Test]
        public void RecordAvgData_TestIntervalAmountRequired_MeetSpec()
        {
            bool res = true;            
            CAMS_ParticulateMatters CAMS_PM = new CAMS_ParticulateMatters();
            SamplingItem item = CAMS_PM.SamplingItemList.First();            
            for (int s = 1; s < item.IntervalAmountRequired; s++)
            {
                item.Sampling();
                item.Analyzing();
                item.RecordRawData();
            }
            res = item.RecordAvgData();
            Assert.IsFalse(res);

            for (int s = item.IntervalAmountRequired; s <= item.IntervalAmountRequired; s++)
            {
                item.Sampling();
                item.Analyzing();
                item.RecordRawData();
            }
            res = item.RecordAvgData();
            Assert.IsTrue(res);
        }

        [TearDown]
        public void TearDown()
        {
            RawDataStore.Instance.ClearRawDataStore();
        }

    }
}
