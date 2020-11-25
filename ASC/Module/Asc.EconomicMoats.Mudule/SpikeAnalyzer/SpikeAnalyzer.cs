using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.ML;

namespace Asc
{
    public class SpikeAnalyzer
    {

        public string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "FPI_jul20_CPA.csv");

        //assign the Number of records in dataset file to constant variable
        public int _docsize = 366;
        public string _docName = "Spikes";
        public bool _hasHeader = true;
        public char _separatorChar = ',';

        public int Confidence = 95;   // Magic Number to have 6 Change Points
        public int SlidingWindowDivided = 30;   // Magic Number to have 6 Change Points

        public string DateTime_Start = DateTime.Now.ToString("yyyyMMdd-HHmm");

        public void RunAnalysis()
        {
            // Create MLContext to be shared across the model creation workflow objects
            // <SnippetCreateMLContext>
            MLContext mlContext = new MLContext();
            // </SnippetCreateMLContext>

            //STEP 1: Common data loading configuration
            // <SnippetLoadData>
            IDataView dataView = mlContext.Data.LoadFromTextFile<TempData>(path: _dataPath, hasHeader: _hasHeader, separatorChar: _separatorChar);
            // </SnippetLoadData>

            //// Spike detects pattern temporary changes
            //// <SnippetCallDetectSpike>
            //DetectSpike(mlContext, _docsize, dataView);
            //// </SnippetCallDetectSpike>

            // Spike detects pattern temporary changes
            // <SnippetCallDetectSpike>
            DetectSpike(mlContext, _docsize, dataView);
            // </SnippetCallDetectSpike>
        }

        public void DetectSpike(MLContext mlContext, int docSize, IDataView tempData)
        {
            //Console.WriteLine("Detect Persistent changes in pattern");

            string ResultFilePath = AppDomain.CurrentDomain.BaseDirectory
                    + @"Result\" + DateTime_Start + "\\Spa\\" + _docName + ".csv";
            //public string ResultFilePath = AppDomain.CurrentDomain.BaseDirectory + @"Result\" +
            //    "ChangePoints" + ".csv";

            FileInfo FI = new FileInfo(ResultFilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            //using (var file = new StreamWriter(ResultFilePath, true))
            //{
            //    file.WriteLine("Detect temporary changes in pattern");
            //}

            //STEP 2: Set the training algorithm
            // <SnippetAddChangePointTrainer>
            var iidSpikeEstimator = mlContext.Transforms.DetectIidSpike(
                outputColumnName: nameof(TempPrediction.Prediction),
                inputColumnName: nameof(TempData.TG),
                confidence: Confidence,
           /*pvalueHistoryLength: _docsize / SlidingWindowDivided);*/    // [100% / Divided] x-range: The length of the sliding window on p-values for computing the martingale score.
                pvalueHistoryLength: SlidingWindowDivided);
            // </SnippetAddChangePointTrainer>

            //STEP 3: Create the transform
            //Console.WriteLine("=============== Training the model Using Change Point Detection Algorithm===============");            
            //using (var file = new StreamWriter(ResultFilePath, true))
            //{
            //    file.WriteLine("=============== Training the model ===============");
            //}
            // <SnippetTrainModel2>
            var iidSpikeTransform = iidSpikeEstimator.Fit(CreateEmptyDataView(mlContext));
            // </SnippetTrainModel2>
            //Console.WriteLine("=============== End of training process ===============");            
            //using (var file = new StreamWriter(ResultFilePath, true))
            //{
            //    file.WriteLine("=============== End of training process ===============");
            //}

            //Apply data transformation to create predictions.
            // <SnippetTransformData2>
            IDataView transformedData = iidSpikeTransform.Transform(tempData);
            // </SnippetTransformData2>

            // <SnippetCreateEnumerable2>
            var predictions = mlContext.Data.CreateEnumerable<TempPrediction>(transformedData, reuseRowObject: false);
            // </SnippetCreateEnumerable2>

            // <SnippetDisplayHeader2>
            //Console.WriteLine("Alert\tScore\tP-Value");
            using (var file = new StreamWriter(ResultFilePath, true))
            {
                file.WriteLine("STAID\tDATE\tQ_TG\tAlert\tScore\tP-Value");
            }
            // </SnippetDisplayHeader2>

            // <SnippetDisplayResults2>
            //int counter = 0;
            int AnomalyCnt = 0;
            foreach (var p in predictions)
            {
                var results = $"{p.STAID}\t{p.DATE}\t{p.Q_TG}\t{p.Prediction[0]}\t{p.Prediction[1]:f2}\t{p.Prediction[2]:F2}";

                if (p.Prediction[0] == 1)
                {
                    //results += " <-- Spike detected";
                    AnomalyCnt++;
                    using (var file = new StreamWriter(ResultFilePath, true))
                    {
                        file.WriteLine(results);
                    }
                }
                //Console.WriteLine(results);                
                //counter++;
            }
            //Console.WriteLine("");            
            //using (var file = new StreamWriter(ResultFilePath, true))
            //{
            //    file.WriteLine("Spike Counter: " + AnomalyCnt.ToString());
            //}
            // </SnippetDisplayResults2>
        }

        // <SnippetCreateEmptyDataView>
        static IDataView CreateEmptyDataView(MLContext mlContext)
        {
            // Create empty DataView. We just need the schema to call Fit() for the time series transforms
            IEnumerable<TempData> enumerableData = new List<TempData>();
            return mlContext.Data.LoadFromEnumerable(enumerableData);
        }
        // </SnippetCreateEmptyDataView>
    }
}
