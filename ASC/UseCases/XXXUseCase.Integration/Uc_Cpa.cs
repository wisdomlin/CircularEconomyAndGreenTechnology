using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Asc
{
    public class Uc_Cpa
    {
        // Variables that need to be set from outside
        public string RawFolderPath;
        public string MetaFolderPath;
        public string ResultFolderPath;

        public string RawFileName;

        // Internal Objects
        public CsvFileAnalyzer Cfa;
        private Dal_EuroStatFrench Dal;

        /// <summary>
        /// Use Case' Standard Interface
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            bool result = true;
            result &= this.UseCsvFileAnalyzer();
            result &= this.UseSingularSpectrumAnalyzer();
            result &= this.UseOutlierTrimmingAnalyzer();
            result &= this.UseChangePointAnalyzer();
            return result;
        }

        public Uc_Cpa()
        {
            
        }

        // Data Structures used by Modules (and Inter-Mudules!)
        private ConcurrentDictionary<string, double[]> Dic_trendFpiArr = new ConcurrentDictionary<string, double[]>();
        private ConcurrentDictionary<string, double[]> Dic_noiseFpiArr = new ConcurrentDictionary<string, double[]>();

        private ConcurrentDictionary<string, double> Dic_FpiLower = new ConcurrentDictionary<string, double>();
        private ConcurrentDictionary<string, double> Dic_FpiUpper = new ConcurrentDictionary<string, double>();

        private ConcurrentDictionary<string, double[]> Dic_Cpa01Score = new ConcurrentDictionary<string, double[]>();   // ?? redundant?
        private ConcurrentDictionary<string, double[]> Dic_Cpa02Score = new ConcurrentDictionary<string, double[]>();
        public ConcurrentDictionary<string, List<string>> Dic_Cpa02Date = new ConcurrentDictionary<string, List<string>>();

        public bool UseCsvFileAnalyzer()
        {
            // 1. Creation Management
            Cfa = new CsvFileAnalyzer();
            Cfa.FilePath = RawFolderPath + RawFileName;

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
            Efa_Dic_StringList_DoubleList_Fpi Ea = new Efa_Dic_StringList_DoubleList_Fpi();
            Ea.FilePath = ResultFolderPath + "Original\\" + "Result_Original_Price" + ".xlsx";
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

                    string FilePath = MetaFolderPath + "Ssa\\" + "trendFpi_" + entry.Key.ToString() + ".csv";
                    StoreArrayAsMetaCsv(
                        Dal.dicListDate[entry.Key].ToArray(), trendFpiArr,
                        FilePath);
                }

                // Write Excel
                Efa_Dic_StringList_DoubleArray_EuroStat Ea = new Efa_Dic_StringList_DoubleArray_EuroStat();
                Ea.dicListDate = Dal.dicListDate;
                // Trend
                Ea.FilePath = ResultFolderPath + "Ssa\\" + "Result_Ssa_Trend" + ".xlsx";
                Ea.SheetName = "Trend";
                Ea.dicArrData = Dic_trendFpiArr;
                Ea.CreateExcel();
                // Noise
                Ea.FilePath = ResultFolderPath + "Ssa\\" + "Result_Ssa_Noise" + ".xlsx";
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
            }

            Efa_Dic_Double_Double_Ota Ea = new Efa_Dic_Double_Double_Ota();
            Ea.FilePath = ResultFolderPath + "Ota/" + "Result_Ota" + ".xlsx";
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
                // Run Cpa Analysis and Write as Meta File
                foreach (KeyValuePair<string, double[]> entry in Dic_trendFpiArr)
                {
                    ChangePointAnalyzer CpaFpi = new ChangePointAnalyzer();
                    CpaFpi._InputDataPath = MetaFolderPath + @"Ssa\" + "trendFpi_" + entry.Key + ".csv";
                    CpaFpi._OutputDataPath = MetaFolderPath + @"Cpa\" + "CpaFpi_" + entry.Key + ".csv";
                    CpaFpi._hasHeader = false;
                    CpaFpi._docsize = 177;
                    CpaFpi._docName = "CpaFpi_" + entry.Key;
                    CpaFpi.Confidence = 95;
                    CpaFpi.SlidingWindowDivided = 30;
                    CpaFpi.RunAnalysis();
                }

                // Read Meta and Output an Integrated Auto Data CSV
                ReadMetaAndOutputAnIntegratedAutoDataCSV();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void ReadMetaAndOutputAnIntegratedAutoDataCSV()
        {
            foreach (KeyValuePair<string, double[]> entry in Dic_trendFpiArr)
            {
                // Use key name to generate meta filename to read
                //string CpaMetaFilePath = AppDomain.CurrentDomain.BaseDirectory
                //    + @"Meta\DACF_EuroStat\" + @"Cpa\" + "ChangePointsFpi_" + entry.Key + ".csv";
                string CpaMetaFilePath = MetaFolderPath + @"Cpa\" + "CpaFpi_" + entry.Key + ".csv";

                bool res = true;
                int LineIndex = 0;
                string Line = "";
                char[] Delimiters = new char[] { '\t' };

                List<double> TempCpa01Score = new List<double>();
                List<double> TempCpa02Score = new List<double>();
                List<string> TempCpa02Date = new List<string>();

                GetExcelFigureMax(entry.Value.Max(), entry.Value.Min(),
                     out double Ymax, out double Ymin, out double Yscale);

                try
                {
                    using (FileStream fs = File.Open(CpaMetaFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (BufferedStream bs = new BufferedStream(fs))
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        //if (Cfs.FooterLinesCount > 0)
                        //    FileTotalLinesCount = File.ReadLines(FilePath).Count();
                        while ((Line = sr.ReadLine()) != null)
                        {
                            // Skip First Header Line
                            LineIndex++;
                            if (LineIndex == 1)
                                continue;

                            // if it is a Change Point
                            string[] Splits = Line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                            if (Splits[0] == "1")
                            {
                                // Cpa01Score
                                TempCpa01Score.Add(Ymax);
                                // Cpa02Date
                                string d = Dal.dicListDate[entry.Key][LineIndex - 2];  // 2 = Header Line + Zero Index
                                TempCpa02Date.Add(d);
                                // Cpa02Score
                                double s = Convert.ToDouble(Splits[1]);
                                TempCpa02Score.Add(s);
                            }
                            else
                            {
                                TempCpa01Score.Add(0);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("entry.Key: " + entry.Key +
                        "\tLineIndex: " + LineIndex.ToString() + "\tLine: " + Line.ToString());
                    res = false;
                }
                finally
                {
                    // Do nothing
                }

                Dic_Cpa01Score.TryAdd(entry.Key, TempCpa01Score.ToArray());
                Dic_Cpa02Date.TryAdd(entry.Key, TempCpa02Date);
                Dic_Cpa02Score.TryAdd(entry.Key, TempCpa02Score.ToArray());
            }

            // Form #1
            Efa_Dic_StringList_DoubleArray_EuroStat Efa = new Efa_Dic_StringList_DoubleArray_EuroStat();
            Efa.FilePath = ResultFolderPath + @"Cpa\" + "Result_Cpa01" + ".xlsx";
            Efa.SheetName = "Cpa";
            Efa.dicListDate = Dal.dicListDate;
            Efa.dicArrData = Dic_Cpa01Score;
            Efa.CreateExcel();

            // Save as CSV also (for Integrated Analysis metadata)
            string FilePath = ResultFolderPath + @"Cpa\" + "Result_Cpa01" + ".csv";
            StoreArrayAsResultCsv(Dal.dicListDate, Dic_Cpa01Score, FilePath);

            // Form #2
            Efa_Dic_StringList_DoubleArray_EuroStat Efa2 = new Efa_Dic_StringList_DoubleArray_EuroStat();
            Efa2.FilePath = ResultFolderPath + @"Cpa\" + "Result_Cpa02" + ".xlsx";
            Efa2.SheetName = "Cpa";
            Efa2.dicListDate = Dic_Cpa02Date;
            Efa2.dicArrData = Dic_Cpa02Score;
            Efa2.CreateExcel();

            FilePath = ResultFolderPath + @"Cpa\" + "Result_Cpa02" + ".csv";
            StoreArrayAsResultCsv(Dic_Cpa02Date, Dic_Cpa02Score, FilePath);
        }

        private void GetExcelFigureMax(double max, double min,
            out double Ymax, out double Ymin, out double Yscale)
        {
            if (max < min)
            {
                double tempswap = max;
                max = min;
                min = tempswap;
            }
            else if (max == min)
            {
                //increase max by 1 %
                max *= 1.01;
                //decrease min by 1 %
                max *= 0.99;
            }

            double padding = 0.01 * (max - min);
            if (max > 0 && min > 0)
            {
                max += padding;
                min += padding;
            }
            if (max < 0 && min < 0)
            {
                max -= padding;
                min -= padding;
            }
            if (max == 0 && min == 0)
            {
                max = 1;
            }

            // instead of the 1, 2, 5 sequence, I use 1, 2.5, 5. Thus, I get major units of 10, 25, 50, 100, 250, 500...
            double power = Math.Log(max - min) / Math.Log(10);
            double scale = Math.Pow(10, power - Math.Round(power));    // 10 ^ (power - int(power));
            if (scale <= 2.5)
                scale = 2;
            else if (scale <= 5)
                scale = 2;
            else if (scale <= 7.5)
                scale = 5;
            else
                scale = 10;
            scale *= Math.Pow(10, Math.Round(power));   // multiply scale by 10 ^ int(power)

            // Outputs: 
            Ymin = scale * Math.Floor(min / scale);     // scale* floor(min / scale)
            Ymax = scale * Math.Ceiling(max / scale);   // scale* ceil(max / scale)
            Yscale = scale;                             // scale

            // Ymax + (Ymax - Ymin) / 20
            //double FigureMax = Ymax + (Ymax - Ymin) / 20;

            //double multiple = v / 20;
            //double remain = v % 20;
            //double FigureMax = 0;
            //if (remain == 0)
            //    FigureMax = multiple * 20;
            //else
            //    FigureMax = (multiple + 1) * 20;

            //if (FigureMax >= 200)   // Egg Case 
            //    FigureMax += 50;

            //return FigureMax;
        }

        private void StoreArrayAsMetaCsv(string[] DateArr, double[] IndexArr,
            string OutputFilePath)
        {
            FileInfo FI = new FileInfo(OutputFilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(OutputFilePath, false))
            {
                int len = IndexArr.Length;
                for (int i = 0; i < len; i++)
                {
                    file.WriteLine(string.Format("{0},{1}", DateArr[i], IndexArr[i]));
                }
            }
        }

        private void StoreArrayAsResultCsv(
            ConcurrentDictionary<string, List<string>> DateArr,
            ConcurrentDictionary<string, double[]> IndexArr,
            string OutputFilePath)
        {

            FileInfo FI = new FileInfo(OutputFilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(OutputFilePath, false))
            {
                // Food
                if (DateArr.TryGetValue("Food", out List<string> dates))
                {
                    // Write Supply Chain row
                    file.WriteLine("Food");
                    // Write Date row
                    int s_cnt = 1;
                    foreach (string s in dates)
                    {
                        string[] splits = s.Split('M');
                        string S_format = splits[0] + "-" + splits[1] + "-01"; // yyyy-MM-dd
                        if (s_cnt < dates.Count)
                            file.Write(string.Format("{0},", S_format));
                        else
                            file.WriteLine(string.Format("{0}", S_format));
                        s_cnt++;
                    }
                    // Write Index row
                    if (IndexArr.TryGetValue("Food", out double[] Indexes))
                    {
                        int d_cnt = 1;
                        foreach (double d in Indexes)
                        {
                            if (d_cnt < Indexes.Length)
                                file.Write(string.Format("{0},", d));
                            else
                                file.WriteLine(string.Format("{0}", d));
                            d_cnt++;
                        }
                    }
                    else
                    {
                        // NOT FOUND
                    }
                }
                else
                {
                    // NOT FOUND
                }

                // Except "Food"
                foreach (KeyValuePair<string, List<string>> entry
                    in DateArr.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value))
                {
                    if (entry.Key == "Food")
                        continue;
                    // Write Supply Chain row
                    file.WriteLine(string.Format("{0}", entry.Key));
                    // Write Date row
                    int s_cnt = 1;
                    foreach (string s in entry.Value)
                    {
                        string[] splits = s.Split('M');
                        string S_format = splits[0] + "-" + splits[1] + "-01"; // yyyy-MM-dd
                        if (s_cnt < entry.Value.Count)
                            file.Write(string.Format("{0},", S_format));
                        else
                            file.WriteLine(string.Format("{0}", S_format));
                        s_cnt++;
                    }
                    // Write Index row
                    if (IndexArr.TryGetValue(entry.Key, out double[] Indexes))
                    {
                        int d_cnt = 1;
                        foreach (double d in Indexes)
                        {
                            if (d_cnt < Indexes.Length)
                                file.Write(string.Format("{0},", d));
                            else
                                file.WriteLine(string.Format("{0}", d));
                            d_cnt++;
                        }
                    }
                    else
                    {
                        // NOT FOUND
                    }
                }
            }
        }
    }
}
