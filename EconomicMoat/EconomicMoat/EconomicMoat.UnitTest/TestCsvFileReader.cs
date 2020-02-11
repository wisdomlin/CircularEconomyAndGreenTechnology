using EconomicMoat.Standard;
using NUnit.Framework;
using System;
using System.Data;

namespace EconomicMoat.UnitTest
{
    class TestCsvFileReader
    {
        string Path;
        CsvFileReader Cfr;
        CsvFileStructure Cfs;

        [SetUp]
        public void Setup()
        {
            Path = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg\TG_STAID000001.txt";
            Cfr = new CsvFileReader();
            Cfr.FilePath = Path;
            Cfr.Delimiters = new Char[] { ',', ' ' };

            Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 21;
            Cfs.DataLinesStartAt = 22;
            Cfs.FooterLinesStartAt = Cfs.NOT_APPLIED;

            Cfr.Cfs = Cfs;
        }

        [Test]
        public void TestReadFullFile()
        {
            Cfr.Dal = new DatalineAnalysisLogic();  // TODO: 創建，不做事的 DAL
            Cfr.Dal.dlf = new DataLinesFormat();
            bool result = Cfr.ReadFullFile();
            Assert.IsTrue(result);
        }

        [Test]
        public void TestFindDatesBeyondTgThreshold()
        {
            Cfr.Dal = new DatalineAnalysisLogic();
            Cfr.Dal.dlf = new DataLinesFormat();
            bool result = Cfr.ReadFullFile();
            Assert.IsTrue(result);
        }

        [Test]
        public void TestReadAllTgFiles()
        {
            //cfr.Dlf = new DataLinesFormatTg();
            //bool result = Cfr.ReadAllTgFiles();
            //Assert.IsTrue(result);
        }
    }
}
