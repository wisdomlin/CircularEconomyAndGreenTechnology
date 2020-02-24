using EconomicMoat.Standard;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace EconomicMoat.ModuleTest
{
    class TestCsvFileReader
    {
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

        [Test]
        public void UC03_ReadTenCsvFiles()
        {
            string ConfigPath = GetConfigFolder() + "UC01_ReadOneCsvFile.Config";
            CsvFileReader Cfr = new CsvFileReaderFactory().CreateCsvFileReader(ConfigPath);

            // Find all files in a folder
            string FolderPath = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg";
            DirectoryInfo d = new DirectoryInfo(FolderPath);

            int i = 0;
            foreach (FileInfo file in d.GetFiles("TG_*.txt"))
            {
                // Do something for each file
                string FilePath = file.FullName;
                Cfr.SetFilePath(FilePath);
                bool result = Cfr.ReadFullFile();
                Assert.IsTrue(result);

                i++;
                if (i >= 10)
                    break;
            }
        }

        [Test]
        public void UC04_JsonStructLogAnalyzeTenCsvFiles()
        {
            // STAID, SOUID, DATE, TG, Q_TG
            string ConfigPath = GetConfigFolder() + "UC02_ReadFileAndCustomizeAnalysis.Config";
            CsvFileReader Cfr = new CsvFileReaderFactory().CreateCsvFileReader(ConfigPath);

            // Find all CSV files in a folder
            string FolderPath = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg";
            DirectoryInfo Dir = new DirectoryInfo(FolderPath);

            int i = 0;
            foreach (FileInfo file in Dir.GetFiles("TG_*.txt"))
            {
                // Do something for each file
                string FilePath = file.FullName;
                Cfr.SetFilePath(FilePath);
                bool result = Cfr.ReadFullFile();
                Assert.IsTrue(result);

                i++;
                if (i >= 10)
                    break;
            }
        }

        private string GetConfigFolder()
        {
            string ProjectFolderPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            return ProjectFolderPath + @"CsvFileReader\CsvFileReaderConfig\";
        }
    }
}
