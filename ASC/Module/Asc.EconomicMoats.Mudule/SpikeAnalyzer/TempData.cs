using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;

namespace Asc
{
    public class TempData
    {
        [LoadColumn(0)]
        public string STAID;
        //[LoadColumn(1)]
        //public string SOUID;
        [LoadColumn(1)]
        public string DATE;
        [LoadColumn(2)]
        public float TG;
        [LoadColumn(3)]
        public string Q_TG;
    }

    public class TempPrediction
    {
        //vector to hold alert, score, p-value values
        [VectorType(3)]
        public double[] Prediction { get; set; }

        public string STAID { get; set; }
        public string DATE { get; set; }
        public string Q_TG { get; set; }
    }
}
