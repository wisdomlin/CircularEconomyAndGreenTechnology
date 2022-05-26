using System;
using System.Collections.Generic;
using System.IO;

namespace Asc
{
    public class DACF_Fao
    {
        public string FaoFilePath;
        public CsvFileAnalyzer Cfa;
        private Dal_FaoFpi Dal;

        // F-MDCOS
        private double[] trendFpi;
        private double[] noiseFpi;
        private double[] trendMpi;
        private double[] noiseMpi;
        private double[] trendDpi;
        private double[] noiseDpi;
        private double[] trendCpi;
        private double[] noiseCpi;
        private double[] trendOpi;
        private double[] noiseOpi;
        private double[] trendSpi;
        private double[] noiseSpi;

        private double LowerFpi;
        private double UpperFpi;
        private double LowerMpi;
        private double UpperMpi;
        private double LowerDpi;
        private double UpperDpi;
        private double LowerCpi;
        private double UpperCpi;
        private double LowerOpi;
        private double UpperOpi;
        private double LowerSpi;
        private double UpperSpi;

        public bool UseCsvFileAnalyzer()
        {
            // 1. Creation Management
            int HeaderLineStartAt = 3;
            int DataLinesStartAt = 5;
            int FooterLinesCount = 0;
            CsvFileStructure Cfs = new CsvFileStructure(HeaderLineStartAt, DataLinesStartAt, FooterLinesCount);

            char[] Delimiters = new char[] { ',', ' ', '\t' };
            DatalineEntityFormat Def = new DatalineEntityFormat(Delimiters);

            Dal = new Dal_FaoFpi(Def);

            string FilePath = FaoFilePath;
            Cfa = new CsvFileAnalyzer(Cfs, Dal, FilePath);

            // 1. Creation Management
            //Cfa = new CsvFileAnalyzer();
            //Cfa.FilePath = FaoFilePath;
            //Cfa.Delimiters = new char[] { ',', ' ', '\t' };

            //CsvFileStructure Cfs = new CsvFileStructure();
            //Cfs.HeaderLineStartAt = 3;
            //Cfs.DataLinesStartAt = 5;
            //Cfs.FooterLinesCount = 0;

            //Dal = new Dal_FaoFpi();
            //DatalineEntityFormat Def = new DatalineEntityFormat();

            // 2. Dependency Management
            //Cfa.Cfs = Cfs;
            //Dal.Def = Def;
            //Cfa.Dal = Dal;

            // 3. Read Csv File
            bool result = Cfa.ReadCsvFile();

            // 4. Store as Csv 
            StoreArrayAsResultCsv(Dal.DateList.ToArray(), Dal.FpiList.ToArray(), "OriginalFpi");
            StoreArrayAsResultCsv(Dal.DateList.ToArray(), Dal.MpiList.ToArray(), "OriginalMpi");
            StoreArrayAsResultCsv(Dal.DateList.ToArray(), Dal.DpiList.ToArray(), "OriginalDpi");
            StoreArrayAsResultCsv(Dal.DateList.ToArray(), Dal.CpiList.ToArray(), "OriginalCpi");
            StoreArrayAsResultCsv(Dal.DateList.ToArray(), Dal.OpiList.ToArray(), "OriginalOpi");
            StoreArrayAsResultCsv(Dal.DateList.ToArray(), Dal.SpiList.ToArray(), "OriginalSpi");
            return result;
        }

        public bool UseSingularSpectrumAnalyzer()
        {
            bool result = true;
            try
            {
                SingularSpectrumAnalyzer SsaFpi = new SingularSpectrumAnalyzer();
                SsaFpi.SetAddSequences(Dal.FpiList.ToArray());
                SsaFpi.SetWindow(3);
                SsaFpi.SetAlgoTopKDirect(1);
                SsaFpi.AnalyzeSequence(out trendFpi, out noiseFpi);
                StoreArrayAsMetaCsv(Dal.DateList.ToArray(), trendFpi, "trendFpi");
                StoreArrayAsResultCsv(Dal.DateList.ToArray(), trendFpi, "trendFpi");
                StoreArrayAsResultCsv(Dal.DateList.ToArray(), noiseFpi, "noiseFpi");

                SingularSpectrumAnalyzer SsaMpi = new SingularSpectrumAnalyzer();
                SsaMpi.SetAddSequences(Dal.MpiList.ToArray());
                SsaMpi.SetWindow(3);
                SsaMpi.SetAlgoTopKDirect(1);
                SsaMpi.AnalyzeSequence(out trendMpi, out noiseMpi);
                StoreArrayAsMetaCsv(Dal.DateList.ToArray(), trendMpi, "trendMpi");
                StoreArrayAsResultCsv(Dal.DateList.ToArray(), trendMpi, "trendMpi");
                StoreArrayAsResultCsv(Dal.DateList.ToArray(), noiseMpi, "noiseMpi");

                SingularSpectrumAnalyzer SsaDpi = new SingularSpectrumAnalyzer();
                SsaDpi.SetAddSequences(Dal.DpiList.ToArray());
                SsaDpi.SetWindow(3);
                SsaDpi.SetAlgoTopKDirect(1);
                SsaDpi.AnalyzeSequence(out trendDpi, out noiseDpi);
                StoreArrayAsMetaCsv(Dal.DateList.ToArray(), trendDpi, "trendDpi");
                StoreArrayAsResultCsv(Dal.DateList.ToArray(), trendDpi, "trendDpi");
                StoreArrayAsResultCsv(Dal.DateList.ToArray(), noiseDpi, "noiseDpi");

                SingularSpectrumAnalyzer SsaCpi = new SingularSpectrumAnalyzer();
                SsaCpi.SetAddSequences(Dal.CpiList.ToArray());
                SsaCpi.SetWindow(3);
                SsaCpi.SetAlgoTopKDirect(1);
                SsaCpi.AnalyzeSequence(out trendCpi, out noiseCpi);
                StoreArrayAsMetaCsv(Dal.DateList.ToArray(), trendCpi, "trendCpi");
                StoreArrayAsResultCsv(Dal.DateList.ToArray(), trendCpi, "trendCpi");
                StoreArrayAsResultCsv(Dal.DateList.ToArray(), noiseCpi, "noiseCpi");

                SingularSpectrumAnalyzer SsaOpi = new SingularSpectrumAnalyzer();
                SsaOpi.SetAddSequences(Dal.OpiList.ToArray());
                SsaOpi.SetWindow(3);
                SsaOpi.SetAlgoTopKDirect(1);
                SsaOpi.AnalyzeSequence(out trendOpi, out noiseOpi);
                StoreArrayAsMetaCsv(Dal.DateList.ToArray(), trendOpi, "trendOpi");
                StoreArrayAsResultCsv(Dal.DateList.ToArray(), trendOpi, "trendOpi");
                StoreArrayAsResultCsv(Dal.DateList.ToArray(), noiseOpi, "noiseOpi");

                SingularSpectrumAnalyzer SsaSpi = new SingularSpectrumAnalyzer();
                SsaSpi.SetAddSequences(Dal.SpiList.ToArray());
                SsaSpi.SetWindow(3);
                SsaSpi.SetAlgoTopKDirect(1);
                SsaSpi.AnalyzeSequence(out trendSpi, out noiseSpi);
                StoreArrayAsMetaCsv(Dal.DateList.ToArray(), trendSpi, "trendSpi");
                StoreArrayAsResultCsv(Dal.DateList.ToArray(), trendSpi, "trendSpi");
                StoreArrayAsResultCsv(Dal.DateList.ToArray(), noiseSpi, "noiseSpi");
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool UseOutlierTrimmingAnalyzer()
        {
            bool result = true;

            OutlierTrimmingAnalyzer OtaFpi = new OutlierTrimmingAnalyzer();
            result &= OtaFpi.SetOriginalSeriesAndDoAnalysis(noiseFpi);
            OtaFpi.GetConfidenceIntervals(out LowerFpi, out UpperFpi);
            StoreArrayAsResultCsv(LowerFpi, UpperFpi, "OtaFpi");

            OutlierTrimmingAnalyzer OtaMpi = new OutlierTrimmingAnalyzer();
            result &= OtaMpi.SetOriginalSeriesAndDoAnalysis(noiseMpi);
            OtaMpi.GetConfidenceIntervals(out LowerMpi, out UpperMpi);
            StoreArrayAsResultCsv(LowerMpi, UpperMpi, "OtaMpi");

            OutlierTrimmingAnalyzer OtaDpi = new OutlierTrimmingAnalyzer();
            result &= OtaDpi.SetOriginalSeriesAndDoAnalysis(noiseDpi);
            OtaDpi.GetConfidenceIntervals(out LowerDpi, out UpperDpi);
            StoreArrayAsResultCsv(LowerDpi, UpperDpi, "OtaDpi");

            OutlierTrimmingAnalyzer OtaCpi = new OutlierTrimmingAnalyzer();
            result &= OtaCpi.SetOriginalSeriesAndDoAnalysis(noiseCpi);
            OtaCpi.GetConfidenceIntervals(out LowerCpi, out UpperCpi);
            StoreArrayAsResultCsv(LowerCpi, UpperCpi, "OtaCpi");

            OutlierTrimmingAnalyzer OtaOpi = new OutlierTrimmingAnalyzer();
            result &= OtaOpi.SetOriginalSeriesAndDoAnalysis(noiseOpi);
            OtaOpi.GetConfidenceIntervals(out LowerOpi, out UpperOpi);
            StoreArrayAsResultCsv(LowerOpi, UpperOpi, "OtaOpi");

            OutlierTrimmingAnalyzer OtaSpi = new OutlierTrimmingAnalyzer();
            result &= OtaSpi.SetOriginalSeriesAndDoAnalysis(noiseSpi);
            OtaSpi.GetConfidenceIntervals(out LowerSpi, out UpperSpi);
            StoreArrayAsResultCsv(LowerSpi, UpperSpi, "OtaSpi");

            return result;
        }

        public bool UseChangePointAnalyzer()
        {
            try
            {
                ChangePointAnalyzer CpaFpi = new ChangePointAnalyzer();
                CpaFpi._InputDataPath = AppDomain.CurrentDomain.BaseDirectory
                    + @"Meta\" + "trendFpi" + ".csv";
                CpaFpi._hasHeader = false;
                CpaFpi._docsize = 366;
                CpaFpi._docName = "ChangePointsFpi";
                CpaFpi.Confidence = 95;
                CpaFpi.SlidingWindowDivided = 30;   // 6
                CpaFpi.RunAnalysis();

                ChangePointAnalyzer CpaMpi = new ChangePointAnalyzer();
                CpaMpi._InputDataPath = AppDomain.CurrentDomain.BaseDirectory
                    + @"Meta\" + "trendMpi" + ".csv";
                CpaMpi._hasHeader = false;
                CpaMpi._docsize = 366;
                CpaMpi._docName = "ChangePointsMpi";
                CpaMpi.Confidence = 95;
                CpaMpi.SlidingWindowDivided = 40;   // 6
                CpaMpi.RunAnalysis();

                ChangePointAnalyzer CpaDpi = new ChangePointAnalyzer();
                CpaDpi._InputDataPath = AppDomain.CurrentDomain.BaseDirectory
                    + @"Meta\" + "trendDpi" + ".csv";
                CpaDpi._hasHeader = false;
                CpaDpi._docsize = 366;
                CpaDpi._docName = "ChangePointsDpi";
                CpaDpi.Confidence = 94;
                CpaDpi.SlidingWindowDivided = 30;   // 5
                CpaDpi.RunAnalysis();

                ChangePointAnalyzer CpaCpi = new ChangePointAnalyzer();
                CpaCpi._InputDataPath = AppDomain.CurrentDomain.BaseDirectory
                    + @"Meta\" + "trendCpi" + ".csv";
                CpaCpi._hasHeader = false;
                CpaCpi._docsize = 366;
                CpaCpi._docName = "ChangePointsCpi";
                CpaCpi.Confidence = 95;
                CpaCpi.SlidingWindowDivided = 30;   // 6
                CpaCpi.RunAnalysis();

                ChangePointAnalyzer CpaOpi = new ChangePointAnalyzer();
                CpaOpi._InputDataPath = AppDomain.CurrentDomain.BaseDirectory
                    + @"Meta\" + "trendOpi" + ".csv";
                CpaOpi._hasHeader = false;
                CpaOpi._docsize = 366;
                CpaOpi._docName = "ChangePointsOpi";
                CpaOpi.Confidence = 95;
                CpaOpi.SlidingWindowDivided = 30;   // 6
                CpaOpi.RunAnalysis();

                ChangePointAnalyzer CpaSpi = new ChangePointAnalyzer();
                CpaSpi._InputDataPath = AppDomain.CurrentDomain.BaseDirectory
                    + @"Meta\" + "trendSpi" + ".csv";
                CpaSpi._hasHeader = false;
                CpaSpi._docsize = 366;
                CpaSpi._docName = "ChangePointsSpi";
                CpaSpi.Confidence = 95;
                CpaSpi.SlidingWindowDivided = 32;   // 6
                CpaSpi.RunAnalysis();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void StoreArrayAsResultCsv(double Index1, double Index2, string FileName)
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory
                + @"Result\" + DateTime.Now.ToString("yyyyMMdd-HHmm") + "\\" + FileName + ".csv";
            FileInfo FI = new FileInfo(FilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(FilePath, false))
            {
                file.WriteLine(string.Format("{0},{1}", Index1, Index2));
            }
        }

        private void StoreArrayAsResultCsv(string[] DateArr, double[] IndexArr, string FileName)
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory
                + @"Result\" + DateTime.Now.ToString("yyyyMMdd-HHmm") + "\\" + FileName + ".csv";
            FileInfo FI = new FileInfo(FilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(FilePath, false))
            {
                int len = IndexArr.Length;
                for (int i = 0; i < len; i++)
                {
                    file.WriteLine(string.Format("{0},{1}", DateArr[i], IndexArr[i]));
                }
            }
        }

        private void StoreArrayAsMetaCsv(string[] DateArr, double[] IndexArr, string FileName)
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory
                + @"Meta\" + FileName + ".csv";
            FileInfo FI = new FileInfo(FilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(FilePath, false))
            {
                int len = IndexArr.Length;
                for (int i = 0; i < len; i++)
                {
                    file.WriteLine(string.Format("{0},{1}", DateArr[i], IndexArr[i]));
                }
            }
        }
    }
}
