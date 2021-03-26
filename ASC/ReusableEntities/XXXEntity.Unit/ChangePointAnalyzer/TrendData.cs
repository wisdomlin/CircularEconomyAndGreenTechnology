using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;

namespace Asc
{
    public class TrendData
    {
        [LoadColumn(0)]
        public string Date;

        [LoadColumn(1)]
        public float Index;
    }

    public class TrendPrediction
    {
        //vector to hold alert, score, p-value values
        [VectorType(3)]
        public double[] Prediction { get; set; }
    }
}
