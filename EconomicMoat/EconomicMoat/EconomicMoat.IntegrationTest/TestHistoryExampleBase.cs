using EconomicMoat.Standard;
using NUnit.Framework;
using System.Data;

namespace EconomicMoat.IntegrationTest
{
    public class TestHistoryExampleBase
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ContainAllBasicCases()
        {
            HistoryExampleBase Heb = new HistoryExampleBase();
            Assert.IsTrue(Heb.contains("1757, July, Europe"));
            Assert.IsTrue(Heb.contains("1906, NA, UK"));
            Assert.IsTrue(Heb.contains("1911, NA, UK"));
            Assert.IsTrue(Heb.contains("1955, NA, UK"));
            Assert.IsTrue(Heb.contains("1976, NA, UK"));
            Assert.IsTrue(Heb.contains("1990, NA, UK"));
            Assert.IsTrue(Heb.contains("1995, NA, UK"));
            Assert.IsTrue(Heb.contains("2003, NA, Europe"));
            Assert.IsTrue(Heb.contains("2006, NA, Europe"));
            Assert.IsTrue(Heb.contains("2007, NA, Europe"));
            Assert.IsTrue(Heb.contains("2010, NA, Europe"));
            Assert.IsTrue(Heb.contains("2013, NA, UK"));
            Assert.IsTrue(Heb.contains("2014, NA, Sweden"));
            Assert.IsTrue(Heb.contains("2018, NA, UK"));
            Assert.IsTrue(Heb.contains("2018, NA, Europe"));
            Assert.IsTrue(Heb.contains("2019, June, Europe"));
            Assert.IsTrue(Heb.contains("2019, July, Europe"));
        }

        /// <summary>
        /// https://eca.knmi.nl/dailydata/predefinedseries.php
        /// </summary>
        [Test]
        public void GenTgCsv()
        {
            

            DataTable dt;
            // index: Datetime
            // col 1: Region
            // col 2: Temperature
            
        }

        [Test]
        public void GetHeatWaveDatetimeByTG()
        {




        }

        [Test]
        public void CompareHeatWaveDatetime()
        {


            

        }

    }
}