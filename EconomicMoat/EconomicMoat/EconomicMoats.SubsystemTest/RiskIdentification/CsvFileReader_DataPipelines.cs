using EconomicMoat.Standard;
using NUnit.Framework;
using System;
using System.Data;
using System.IO;

namespace EconomicMoat.SubsystemTest
{
    class CsvFileReader_DataPipelines
    {
        [Test]
        public void UC01_ConsumerProducer()
        {
            // -------------------
            // ReadTenCsvFiles
            // -------------------
            string ConfigPath = GetConfigFolder() + "UC01_ConsumerProducer.Config";
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
                if (i >= 1)
                    break;
            }
        }

        private string GetConfigFolder()
        {
            string ProjectFolderPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            //return ProjectFolderPath + @"CsvFileReader\CsvFileReaderConfig\";
            return ProjectFolderPath;
        }

    }
}
