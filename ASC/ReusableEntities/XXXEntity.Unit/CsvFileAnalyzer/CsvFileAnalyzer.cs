using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks;

namespace Asc
{
    /// <summary>
    /// Default Delimiters: { ',', '\\', '\n', ' ', '\t' }
    /// 
    /// </summary>
    public class CsvFileAnalyzer
    {
        public CsvFileStructure Cfs;
        public DatalineAnalysisLogic Dal;

        #region File Properties
        public string FilePath;
        public char[] Delimiters;
        #endregion

        #region Supports
        private string[] Headers;
        private int FileTotalLinesCount;

        BufferBlock<string> buffer;
        #endregion

        public CsvFileAnalyzer()
        {

        }

        /// <summary>
        /// FilePath is required.
        /// </summary>
        /// <returns></returns>
        public bool ReadCsvFile()
        {
            bool res = true;
            int LineIndex = 1;
            string Line = "";
            Dal.Delimiters = Delimiters;

            //// Create a BufferBlock<byte[]> object. This object serves as the 
            //// target block for the producer and the source block for the consumer.
            //buffer = new BufferBlock<string>();

            //// Start the consumer. The Consume method runs asynchronously. 
            ////Dal_TgAbsMaxAlarmConsumer Dalpc = new Dal_TgAbsMaxAlarmConsumer();
            //Dal.Delimiters = Delimiters;
            //List<Task> consumers = new List<Task>();
            //for (int j = 1; j <= 10; j++)
            //{
            //    consumers.Add(((Dal_TgAbsMaxAlarmConsumer)Dal).CustomizedAnalyze_AsyncSendReceive(buffer));
            //}

            try
            {
                // Post source data to [the BufferBlock].
                // [using statement] for better memory management
                using (FileStream fs = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {
                    if (Cfs.FooterLinesCount > 0)
                        FileTotalLinesCount = File.ReadLines(FilePath).Count();
                    while ((Line = sr.ReadLine()) != null)
                    {
                        ProcessForEachLine(Line, LineIndex);
                        LineIndex++;
                    }
                }

                // Wait for the consumer to process all data.
                //Task.WaitAll(consumers.ToArray());
                //foreach (Task consumer in consumers)
                //{
                //    consumer.Wait();
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine("LineIndex: " + LineIndex.ToString() + "\tLine: " + Line.ToString());
                res = false;
            }
            finally
            {
                // Do nothing
            }
            return res;
        }

        public void SetFilePath(string FilePath)
        {
            this.FilePath = FilePath;
        }

        private void ProcessForEachLine(string Line, int LineIndex)
        {
            CsvFileStructure.LocationInFile Loc = DetermineLocationInFile(LineIndex);
            switch (Loc)
            {
                case CsvFileStructure.LocationInFile.HeadingLines:
                    {
                        break;
                    }
                case CsvFileStructure.LocationInFile.HeaderLines:
                    {
                        Headers = Line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                        break;
                    }
                case CsvFileStructure.LocationInFile.DataLines:
                    {
                        ProcessForEachDataline(Line);
                        break;
                    }
                case CsvFileStructure.LocationInFile.FooterLines:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void ProcessForEachDataline(string Line)
        {
            //string[] LineSplits = Line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
            // TODO: Check (splits.Length == headers.Length)

            Dal.CustomizedAnalyze(Line);

            //// Post Input data to [TargetBlock].
            //buffer.Post(Line);
        }

        private CsvFileStructure.LocationInFile DetermineLocationInFile(int LineIndex)
        {
            // 如果已經進入 FooterLines，則直接回傳 LocationInFile.FooterLines
            if (Cfs.FooterLinesCount > 0 &&
                    (FileTotalLinesCount - LineIndex) < Cfs.FooterLinesCount)
                return CsvFileStructure.LocationInFile.FooterLines;

            // 針對所有分界點由低到高排序 (每個分界點皆須是 Unique，不適用的分界點為 -1)
            List<Tuple<int, CsvFileStructure.LocationInFile>> list = new List<Tuple<int, CsvFileStructure.LocationInFile>>();
            list.Add(Tuple.Create(Cfs.HeaderLineStartAt, CsvFileStructure.LocationInFile.HeaderLines));
            list.Add(Tuple.Create(Cfs.DataLinesStartAt, CsvFileStructure.LocationInFile.DataLines));
            list.Sort((x, y) => x.Item1.CompareTo(y.Item1));

            // 依序比較是否進入該點範圍內，直到全部比較完畢
            CsvFileStructure.LocationInFile result = CsvFileStructure.LocationInFile.HeadingLines;
            foreach (Tuple<int, CsvFileStructure.LocationInFile> entry in list)
            {
                if (entry.Item1 <= LineIndex)
                {
                    result = entry.Item2;
                }
            }
            return result;
        }

        public bool ReadAllTgFiles()
        {
            throw new NotImplementedException();
        }

    }
}
