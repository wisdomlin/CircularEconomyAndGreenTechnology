using EconomicMoat.Standard;
using NUnit.Framework;
using System;
using System.Data;

namespace EconomicMoat.UnitTest
{
    class TestCsvFileReader
    {
        string Path;
        CsvFileReader cfr;

        [SetUp]
        public void Setup()
        {
            Path = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg\TG_STAID000001.txt";
            cfr = new CsvFileReader();
            cfr.FilePath = Path;
            cfr.Delimiters = new Char[] { ',', ' ' };

            cfr.HeadingLinesStartAt = 1;
            cfr.HeaderLineStartAt = 21;
            cfr.DataLinesStartAt = 22;
            cfr.FooterLinesStartAt = cfr.NOT_APPLIED;
        }

        [Test]
        public void TestReadFullFile()
        {
            cfr.Dlf = new DataLinesFormat();
            bool result = cfr.ReadFullFile();
            Assert.IsTrue(result);
        }

        [Test]
        public void TestFindDatesBeyondTgThreshold()
        {
            cfr.Dlf = new DataLinesFormatTg();
            bool result = cfr.FindDatesBeyondTgThreshold(230);
            Assert.IsTrue(result);
        }

        [Test]
        public void TestReadAllTgFiles()
        {
            cfr.Dlf = new DataLinesFormatTg();
            bool result = cfr.ReadAllTgFiles();
            Assert.IsTrue(result);
        }
    }
}
