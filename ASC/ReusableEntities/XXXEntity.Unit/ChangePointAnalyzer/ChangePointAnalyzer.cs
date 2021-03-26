using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.ML;

namespace Asc
{
    public class ChangePointAnalyzer
    {

        public string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "FPI_jul20_CPA.csv");

        //assign the Number of records in dataset file to constant variable
        public int _docsize = 366;
        public string _docName = "ChangePoints";
        public bool _hasHeader = false;

        public int Confidence = 95;   // Magic Number to have 6 Change Points
        public int SlidingWindowDivided = 30;   // Magic Number to have 6 Change Points

        public void RunAnalysis()
        {
            // Create MLContext to be shared across the model creation workflow objects
            // <SnippetCreateMLContext>
            MLContext mlContext = new MLContext();
            // </SnippetCreateMLContext>

            //STEP 1: Common data loading configuration
            // <SnippetLoadData>
            IDataView dataView = mlContext.Data.LoadFromTextFile<TrendData>(path: _dataPath, hasHeader: _hasHeader, separatorChar: ',');
            // </SnippetLoadData>

            //// Spike detects pattern temporary changes
            //// <SnippetCallDetectSpike>
            //DetectSpike(mlContext, _docsize, dataView);
            //// </SnippetCallDetectSpike>

            // Changepoint detects pattern persistent changes
            // <SnippetCallDetectChangepoint>
            DetectChangepoint(mlContext, _docsize, dataView);
            // </SnippetCallDetectChangepoint>
        }

        public void DetectChangepoint(MLContext mlContext, int docSize, IDataView trendData)
        {
            

            //Console.WriteLine("Detect Persistent changes in pattern");

            //string ResultFilePath = AppDomain.CurrentDomain.BaseDirectory
            //        + @"Result\" + DateTime.Now.ToString("yyyyMMdd-HHmm") + "\\Cpa\\" + _docName + ".csv";
            string ResultFilePath = AppDomain.CurrentDomain.BaseDirectory
                                + @"Meta\DACF_EuroStat\" + @"Cpa\" + _docName + ".csv";

            FileInfo FI = new FileInfo(ResultFilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(ResultFilePath, true))
            {
                //file.WriteLine("Detect Persistent changes in pattern");
            }

            //STEP 2: Set the training algorithm
            // <SnippetAddChangePointTrainer>
            var iidChangePointEstimator = mlContext.Transforms.DetectIidChangePoint(
                outputColumnName: nameof(TrendPrediction.Prediction),
                inputColumnName: nameof(TrendData.Index),
                confidence: Confidence,
                changeHistoryLength: 8);      // 12 months * 15 years / 6 change points = 30, but CHL = 8 to get 6 change points
                //changeHistoryLength: _docsize / SlidingWindowDivided);    // [100% / Divided] x-range: The length of the sliding window on p-values for computing the martingale score.
                // 366/30=12.2
            // </SnippetAddChangePointTrainer>

            //STEP 3: Create the transform
            //Console.WriteLine("=============== Training the model Using Change Point Detection Algorithm===============");            
            using (var file = new StreamWriter(ResultFilePath, true))
            {
                //file.WriteLine("=============== Training the model Using Change Point Detection Algorithm===============");
            }
            // <SnippetTrainModel2>
            var iidChangePointTransform = iidChangePointEstimator.Fit(CreateEmptyDataView(mlContext));
            // </SnippetTrainModel2>
            //Console.WriteLine("=============== End of training process ===============");            
            using (var file = new StreamWriter(ResultFilePath, true))
            {
                //file.WriteLine("=============== End of training process ===============");
            }

            //Apply data transformation to create predictions.
            // <SnippetTransformData2>
            IDataView transformedData = iidChangePointTransform.Transform(trendData);
            // </SnippetTransformData2>

            // <SnippetCreateEnumerable2>
            var predictions = mlContext.Data.CreateEnumerable<TrendPrediction>(transformedData, reuseRowObject: false);
            // </SnippetCreateEnumerable2>

            // <SnippetDisplayHeader2>
            //Console.WriteLine("Alert\tScore\tP-Value\tMartingale value");
            using (var file = new StreamWriter(ResultFilePath, false))
            {
                file.WriteLine("Alert\tScore\tP-Value\tMartingale value");
            }
            // </SnippetDisplayHeader2>

            // <SnippetDisplayResults2>
            int counter = 0;
            int CpCnt = 0;
            foreach (var p in predictions)
            {
                var results = $"{p.Prediction[0]}\t{p.Prediction[1]:f2}\t{p.Prediction[2]:F2}\t{p.Prediction[3]:F2}";

                if (p.Prediction[0] == 1)
                {
                    results += " <-- alert is on, predicted changepoint";
                    CpCnt++;
                }
                //Console.WriteLine(results);                
                using (var file = new StreamWriter(ResultFilePath, true))
                {
                    file.WriteLine(results);
                }
                counter++;
            }
            //Console.WriteLine("");            
            using (var file = new StreamWriter(ResultFilePath, true))
            {
                //file.WriteLine("Counter: " + counter.ToString());
            }
            // </SnippetDisplayResults2>
        }

        // <SnippetCreateEmptyDataView>
        static IDataView CreateEmptyDataView(MLContext mlContext)
        {
            // Create empty DataView. We just need the schema to call Fit() for the time series transforms
            IEnumerable<TrendData> enumerableData = new List<TrendData>();
            return mlContext.Data.LoadFromEnumerable(enumerableData);
        }
        // </SnippetCreateEmptyDataView>
    }
}
