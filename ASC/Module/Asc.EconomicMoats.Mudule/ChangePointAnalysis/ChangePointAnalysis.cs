using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.ML;

namespace Asc
{
    public class ChangePointAnalysis
    {

        public string _dataPath = Path.Combine(Environment.CurrentDirectory, "Data", "FPI_jul20_CPA.csv");

        //assign the Number of records in dataset file to constant variable
        const int _docsize = 366;

        public void RunAnalysis()
        {
            // Create MLContext to be shared across the model creation workflow objects
            // <SnippetCreateMLContext>
            MLContext mlContext = new MLContext();
            // </SnippetCreateMLContext>

            //STEP 1: Common data loading configuration
            // <SnippetLoadData>
            IDataView dataView = mlContext.Data.LoadFromTextFile<TrendData>(path: _dataPath, hasHeader: true, separatorChar: ',');
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
            int Divided = 30;   // Magic Number to have *6 Change Points*
            string FilePath = @"C:\Workspace\Branches\CircularEconomyAndGreenTechnology\EconomicMoat\EconomicMoat\EconomicMoats.ModuleTest\ChangePointAnalysis\Result\" +
                "result" + DateTime.Now.ToString("_yyyyMMdd-HHmm-ss_") + Divided.ToString() + ".csv";

            //Console.WriteLine("Detect Persistent changes in pattern");
            using (var file = new StreamWriter(FilePath, true))
            {
                file.WriteLine("Detect Persistent changes in pattern");
            }

            //STEP 2: Set the training algorithm
            // <SnippetAddChangePointTrainer>
            var iidChangePointEstimator = mlContext.Transforms.DetectIidChangePoint(
                outputColumnName: nameof(TrendPrediction.Prediction),
                inputColumnName: nameof(TrendData.Index),
                confidence: 95,
                changeHistoryLength: docSize / Divided);
            // </SnippetAddChangePointTrainer>

            //STEP 3: Create the transform
            //Console.WriteLine("=============== Training the model Using Change Point Detection Algorithm===============");            
            using (var file = new StreamWriter(FilePath, true))
            {
                file.WriteLine("=============== Training the model Using Change Point Detection Algorithm===============");
            }
            // <SnippetTrainModel2>
            var iidChangePointTransform = iidChangePointEstimator.Fit(CreateEmptyDataView(mlContext));
            // </SnippetTrainModel2>
            //Console.WriteLine("=============== End of training process ===============");            
            using (var file = new StreamWriter(FilePath, true))
            {
                file.WriteLine("=============== End of training process ===============");
            }

            //Apply data transformation to create predictions.
            // <SnippetTransformData2>
            IDataView transformedData = iidChangePointTransform.Transform(trendData);
            // </SnippetTransformData2>

            // <SnippetCreateEnumerable2>
            var predictions = mlContext.Data.CreateEnumerable<TrendPrediction>(transformedData, reuseRowObject: false);
            // </SnippetCreateEnumerable2>

            // <SnippetDisplayHeader2>
            Console.WriteLine("Alert\tScore\tP-Value\tMartingale value");
            using (var file = new StreamWriter(FilePath, true))
            {
                file.WriteLine("Alert\tScore\tP-Value\tMartingale value");
            }
            // </SnippetDisplayHeader2>

            // <SnippetDisplayResults2>
            int counter = 0;
            foreach (var p in predictions)
            {
                var results = $"{p.Prediction[0]}\t{p.Prediction[1]:f2}\t{p.Prediction[2]:F2}\t{p.Prediction[3]:F2}";

                if (p.Prediction[0] == 1)
                {
                    results += " <-- alert is on, predicted changepoint";
                }
                //Console.WriteLine(results);                
                using (var file = new StreamWriter(FilePath, true))
                {
                    file.WriteLine(results);
                }
                counter++;
            }
            //Console.WriteLine("");            
            using (var file = new StreamWriter(FilePath, true))
            {
                file.WriteLine("Counter: " + counter.ToString());
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
