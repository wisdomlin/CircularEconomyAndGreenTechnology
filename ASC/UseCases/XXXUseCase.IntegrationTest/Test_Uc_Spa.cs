
using NUnit.Framework;
using System;
using System.Data;
using System.IO;

namespace Asc
{
    class Test_Uc_Spa
    {        
        [Test]
        public void Test_Uc_Spa_FrTg_Serial()
        {
            // Arrange
            Uc_Spa_Serial Uc_Spa = new Uc_Spa_Serial();

            // Data Folder Path
            Uc_Spa.RawFolderPath = @"D:\ECAD\ECA_blend_tg\";
            Uc_Spa.MetaFolderPath = @"D:\Meta\DACF_FrTemp\";
            Uc_Spa.ResultFolderPath = @"D:\Result\";

            // TODO: 也許 直接在外部給定 filepath 才是對的? 再梳理一次以 filepath 外部供應為導向的設計
            // Precip Stations in French
            Uc_Spa.SID_Prefix = "Tg";
            Uc_Spa.SID_Array = new string[] {
                "31",
                "32",
                "33",
                "34",
                "36",
                "37",
                "39",
                "322",
                "323",
                "434",
                "736",
                "737",
                "738",
                "739",
                "740",
                "742",
                "745",
                "749",
                "750",
                "755",
                "756",
                "757",
                "758",
                "784",
                "786",
                "793",
                "2184",
                "2190",
                "2192",
                "2195",
                "2196",
                "2199",
                "2200",
                "2203",
                "2205",
                "2207",
                "2209",
                "11243",
                "11244",
                "11245",
                "11246",
                "11247",
                "11248",
                "11249"
              };

            // Act
            bool result;
            result = Uc_Spa.Run();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_Uc_Spa_FrRr_Serial()
        {
            // Arrange
            Uc_Spa_Serial Uc_Spa = new Uc_Spa_Serial();

            // Data Folder Path
            Uc_Spa.RawFolderPath = @"D:\ECAD\ECA_blend_rr\";
            Uc_Spa.MetaFolderPath = @"D:\Meta\DACF_FrPrecip\";
            Uc_Spa.ResultFolderPath = @"D:\Result\";

            // TODO: 也許 直接在外部給定 filepath 才是對的? 再梳理一次以 filepaht 外部供應為導向的設計
            // Precip Stations in French
            Uc_Spa.SID_Prefix = "Rr";
            Uc_Spa.SID_Array = new string[] {
                "31",
                "32",
                "33",
                "34",
                "36",
                "37",
                "39",
                "322",
                "323",
                "434",
                "736",
                "737",
                "738",
                "739",
                "740",
                "742",
                "745",
                "749",
                "750",
                "755",
                "756",
                "757",
                "758",
                "759",
                "761",
                "764",
                "767",
                "770",
                "771",
                "773",
                "774",
                "776",
                "778",
                "781",
                "785",
                "786",
                "787",
                "790",
                "792",
                "793",
                "796",
                "804",
                "2184",
                "2190",
                "2192",
                "2195",
                "2196",
                "2199",
                "2200",
                "2203",
                "2205",
                "2207",
                "2209",
                "11243",
                "11244",
                "11245",
                "11246",
                "11247",
                "11248",
                "11249",
                "21359",
                "21360"
              };

            // Act
            bool result;
            result = Uc_Spa.Run();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_Uc_Spa_FrTg_Parallel()
        {
            // Arrange
            Uc_Spa_Parallel Uc_Spa = new Uc_Spa_Parallel();

            // Data Folder Path
            Uc_Spa.RawFolderPath = @"D:\ECAD\ECA_blend_tg\";
            Uc_Spa.MetaFolderPath = @"D:\Meta\DACF_FrTemp\";
            Uc_Spa.ResultFolderPath = @"D:\Result\";

            // TODO: 也許 直接在外部給定 filepath 才是對的? 再梳理一次以 filepath 外部供應為導向的設計
            // Precip Stations in French
            Uc_Spa.SID_Prefix = "Tg";
            Uc_Spa.SID_Array = new string[] {
                "31",
                "32",
                "33",
                "34",
                "36",
                "37",
                "39",
                "322",
                "323",
                "434",
                "736",
                "737",
                "738",
                "739",
                "740",
                "742",
                "745",
                "749",
                "750",
                "755",
                "756",
                "757",
                "758",
                "784",
                "786",
                "793",
                "2184",
                "2190",
                "2192",
                "2195",
                "2196",
                "2199",
                "2200",
                "2203",
                "2205",
                "2207",
                "2209",
                "11243",
                "11244",
                "11245",
                "11246",
                "11247",
                "11248",
                "11249"
              };

            // Act
            bool result;
            result = Uc_Spa.Run();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_Uc_Spa_FrRr_Parallel()
        {
            // Arrange
            Uc_Spa_Parallel Uc_Spa = new Uc_Spa_Parallel();

            // Data Folder Path
            Uc_Spa.RawFolderPath = @"D:\ECAD\ECA_blend_rr\";
            Uc_Spa.MetaFolderPath = @"D:\Meta\DACF_FrPrecip\";
            Uc_Spa.ResultFolderPath = @"D:\Result\";

            // TODO: 也許 直接在外部給定 filepath 才是對的? 再梳理一次以 filepaht 外部供應為導向的設計
            // Precip Stations in French
            Uc_Spa.SID_Prefix = "Rr";
            Uc_Spa.SID_Array = new string[] {
                "31",
                "32",
                "33",
                "34",
                "36",
                "37",
                "39",
                "322",
                "323",
                "434",
                "736",
                "737",
                "738",
                "739",
                "740",
                "742",
                "745",
                "749",
                "750",
                "755",
                "756",
                "757",
                "758",
                "759",
                "761",
                "764",
                "767",
                "770",
                "771",
                "773",
                "774",
                "776",
                "778",
                "781",
                "785",
                "786",
                "787",
                "790",
                "792",
                "793",
                "796",
                "804",
                "2184",
                "2190",
                "2192",
                "2195",
                "2196",
                "2199",
                "2200",
                "2203",
                "2205",
                "2207",
                "2209",
                "11243",
                "11244",
                "11245",
                "11246",
                "11247",
                "11248",
                "11249",
                "21359",
                "21360"
              };

            // Act
            bool result;
            result = Uc_Spa.Run();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_Uc_Spa_TwTg()
        {
            // Arrange
            DACF_TwTemp Dacf = new DACF_TwTemp();
            Dacf.DateTime_Start = DateTime.Now.ToString("yyyyMMdd-HHmm");   // 2008/1/28
            //Dacf.FilePath = AppDomain.CurrentDomain.BaseDirectory
            //+ @"DACF\Data\TG_STAID" + value.PadLeft(6, '0') + ".txt";
            //Dacf.FilePath = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg\" +
            //    "TG_STAID" + value.PadLeft(6, '0') + ".txt";

            // Act
            bool result;
            result = Dacf.UseCsvFileAnalyzer(); // -9999 先不篩
            Assert.IsTrue(result);

            //result = Dacf.UseSingularSpectrumAnalyzer();
            //Assert.IsTrue(result);

            //result = Dacf.UseOutlierTrimmingAnalyzer();
            //Assert.IsTrue(result);

            result = Dacf.IntraRawSpikeAnalyzer();
            Assert.IsTrue(result);

            result = Dacf.InterIntegratedSpikeAnalyzer();
            Assert.IsTrue(result);

        }

        [Test]
        public void Test_Uc_Spa_TwRr()
        {
            // Arrange
            Uc_Spa_Serial Dacf = new Uc_Spa_Serial();

            // Act
            bool result;
            result = Dacf.UseCsvFileAnalyzer();
            Assert.IsTrue(result);

            result = Dacf.IntraRawSpikeAnalyzer();
            Assert.IsTrue(result);

            result = Dacf.InterIntegratedSpikeAnalyzer();
            Assert.IsTrue(result);

        } 
    }
}
