using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace Asc
{
    public class DACF_EuroStat
    {
        public string FilePath;
        public CsvFileAnalyzer Cfa;
        private Dal_EuroStatFrench Dal;

        public DACF_EuroStat()
        {
            Dic_trendFpiArr = new ConcurrentDictionary<string, double[]>();
            Dic_noiseFpiArr = new ConcurrentDictionary<string, double[]>();

            Dic_FpiLower = new ConcurrentDictionary<string, double>();
            Dic_FpiUpper = new ConcurrentDictionary<string, double>();
        }

        // F-MDCOS
        private ConcurrentDictionary<string, double[]> Dic_trendFpiArr;
        private ConcurrentDictionary<string, double[]> Dic_noiseFpiArr;

        private ConcurrentDictionary<string, double> Dic_FpiLower;
        private ConcurrentDictionary<string, double> Dic_FpiUpper;

        public bool UseCsvFileAnalyzer()
        {
            // 1. Creation Management
            Cfa = new CsvFileAnalyzer();
            Cfa.FilePath = FilePath;
            Cfa.Delimiters = new char[] { '\t' };

            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 1;
            Cfs.DataLinesStartAt = 2;
            Cfs.FooterLinesCount = 0;

            Dal = new Dal_EuroStatFrench();
            DatalineEntityAndFormat Def = new DatalineEntityAndFormat();

            // 2. Dependency Management
            Cfa.Cfs = Cfs;
            Dal.Def = Def;
            Cfa.Dal = Dal;

            // 3. Read Csv File
            bool result = Cfa.ReadCsvFile();

            // 4. Store as Excel 
            Efa_xlsx_DicDoubleList Ea = new Efa_xlsx_DicDoubleList();
            Ea.FilePath = AppDomain.CurrentDomain.BaseDirectory
                        + "Result\\Result_Summary\\" + "Result_Auto_Data_Original" + ".xlsx";
            Ea.SheetName = "Original";
            Ea.dicListDate = Dal.dicListDate;
            Ea.dicListFpi = Dal.dicListFpi;
            Ea.CreateExcel();

            return result;
        }

        public bool UseSingularSpectrumAnalyzer()
        {
            bool result = true;
            try
            {
                foreach (KeyValuePair<string, List<double>> entry in Dal.dicListFpi)
                {
                    // do something with entry.Value or entry.Key
                    SingularSpectrumAnalyzer SsaFpi = new SingularSpectrumAnalyzer();
                    SsaFpi.SetAddSequences(entry.Value.ToArray());
                    SsaFpi.SetWindow(3);
                    SsaFpi.SetAlgoTopKDirect(1);
                    SsaFpi.AnalyzeSequence(out double[] trendFpiArr, out double[] noiseFpiArr);
                    Dic_trendFpiArr.TryAdd(entry.Key, trendFpiArr);
                    Dic_noiseFpiArr.TryAdd(entry.Key, noiseFpiArr);
                    StoreArrayAsMetaCsv(Dal.dicListDate[entry.Key].ToArray(), trendFpiArr, "trendFpi_" + entry.Key.ToString());
                    //StoreArrayAsResultCsv(Dal.dicListDate[entry.Key].ToArray(), trendFpiArr, "Trend\\" + "trendFpi_" + entry.Key.ToString());
                    //StoreArrayAsResultCsv(Dal.dicListDate[entry.Key].ToArray(), noiseFpiArr, "Noise\\" + "noiseFpi_" + entry.Key.ToString());                    
                }
                Efa_xlsx_DicDoubleArrFpi Ea = new Efa_xlsx_DicDoubleArrFpi();
                Ea.dicListDate = Dal.dicListDate;

                Ea.FilePath = AppDomain.CurrentDomain.BaseDirectory
                            + "Result\\Result_Summary\\" + "Result_Auto_Data_Trend" + ".xlsx";
                Ea.SheetName = "Trend";
                Ea.dicArrData = Dic_trendFpiArr;
                Ea.CreateExcel();

                Ea.FilePath = AppDomain.CurrentDomain.BaseDirectory
                            + "Result\\Result_Summary\\" + "Result_Auto_Data_Noise" + ".xlsx";
                Ea.SheetName = "Noise";
                Ea.dicArrData = Dic_noiseFpiArr;
                Ea.CreateExcel();
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

            foreach (KeyValuePair<string, double[]> entry in Dic_noiseFpiArr)
            {
                // do something with entry.Value or entry.Key
                OutlierTrimmingAnalyzer OtaFpi = new OutlierTrimmingAnalyzer();
                result &= OtaFpi.SetOriginalSeriesAndDoAnalysis(entry.Value);
                OtaFpi.GetConfidenceIntervals(out double LowerFpi, out double UpperFpi);
                Dic_FpiLower.TryAdd(entry.Key, LowerFpi);
                Dic_FpiUpper.TryAdd(entry.Key, UpperFpi);
                //StoreArrayAsResultCsv(LowerFpi, UpperFpi, "Ota\\" + "OtaFpi_" + entry.Key);
            }

            Efa_xlsx_DicDouble Ea = new Efa_xlsx_DicDouble();
            Ea.FilePath = AppDomain.CurrentDomain.BaseDirectory
                        + "Result\\Result_Summary\\" + "Result_Auto_Data_Ota" + ".xlsx";
            Ea.SheetName = "Ota";
            Ea.Dic_FpiLower = Dic_FpiLower;
            Ea.Dic_FpiUpper = Dic_FpiUpper;
            Ea.CreateExcel();

            return result;
        }

        public bool UseChangePointAnalyzer()
        {
            try
            {
                foreach (KeyValuePair<string, double[]> entry in Dic_trendFpiArr)
                {
                    ChangePointAnalyzer CpaFpi = new ChangePointAnalyzer();
                    CpaFpi._dataPath = AppDomain.CurrentDomain.BaseDirectory
                        + @"Meta\" + "trendFpi_" + entry.Key + ".txt";
                    CpaFpi._hasHeader = false;
                    CpaFpi._docsize = 177;
                    CpaFpi._docName = "ChangePointsFpi_" + entry.Key;
                    CpaFpi.Confidence = 95;
                    CpaFpi.SlidingWindowDivided = 30;
                    CpaFpi.RunAnalysis();
                }
                return true;
            }
            catch (Exception e)
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
                + @"Meta\" + "\\" + FileName + ".txt";
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
