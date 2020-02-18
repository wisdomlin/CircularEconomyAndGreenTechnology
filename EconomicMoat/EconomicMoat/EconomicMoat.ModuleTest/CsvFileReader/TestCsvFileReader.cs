using EconomicMoat.Standard;
using NUnit.Framework;
using System;
using System.Data;
using System.IO;

namespace EconomicMoat.ModuleTest
{
    class TestCsvFileReader
    {
        private string GetConfigFolder()
        {
            string ProjectFolderPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            return ProjectFolderPath + @"CsvFileReader\CsvFileReaderConfig\";
        }

        [Test]
        public void UC01_ReadOneCsvFile()
        {
            string ConfigPath = GetConfigFolder() + "UC01_ReadOneCsvFile.Config";
            CsvFileReader Cfr = new CsvFileReaderFactory().CreateCsvFileReader(ConfigPath);
            bool result = Cfr.ReadFullFile();
            Assert.IsTrue(result);
        }

        [Test]
        public void UC02_ReadFileAndCustomizeAnalysis()
        {
            string ConfigPath = GetConfigFolder() + "UC02_ReadFileAndCustomizeAnalysis.Config";
            CsvFileReader Cfr = new CsvFileReaderFactory().CreateCsvFileReader(ConfigPath);
            bool result = Cfr.ReadFullFile();
            Assert.IsTrue(result);
        }
    }
}
