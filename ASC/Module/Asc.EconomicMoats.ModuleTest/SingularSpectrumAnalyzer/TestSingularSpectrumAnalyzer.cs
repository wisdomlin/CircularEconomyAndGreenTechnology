﻿
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Asc
{
    class TestSingularSpectrumAnalyzer
    {
        [Test]
        public void UC01_SampleDataWithAlglib()
        {
            //
            // Here we demonstrate SSA trend/noise separation for some toy problem:
            // small monotonically growing series X are analyzed with 3-tick window
            // and "top-K" version of SSA, which selects K largest singular vectors
            // for analysis, with K=1.
            //
            alglib.ssamodel s;
            double[] x = new double[] { 0, 0.5, 1, 1, 1.5, 2 };

            //
            // First, we create SSA model, set its properties and add dataset.
            //
            // We use window with width=3 and configure model to use direct SSA
            // algorithm - one which runs exact O(N*W^2) analysis - to extract
            // one top singular vector. Well, it is toy problem :)
            //
            // NOTE: SSA model may store and analyze more than one sequence
            //       (say, different sequences may correspond to data collected
            //       from different devices)
            //
            alglib.ssacreate(out s);
            alglib.ssasetwindow(s, 3);
            alglib.ssaaddsequence(s, x);
            alglib.ssasetalgotopkdirect(s, 1);

            //
            // Now we begin analysis. Internally SSA model stores everything it needs:
            // data, settings, solvers and so on. Right after first call to analysis-
            // related function it will analyze dataset, build basis and perform analysis.
            //
            // Subsequent calls to analysis functions will reuse previously computed
            // basis, unless you invalidate it by changing model settings (or dataset).
            //
            double[] trend;
            double[] noise;
            alglib.ssaanalyzesequence(s, x, out trend, out noise);
            System.Console.WriteLine("{0}", alglib.ap.format(trend, 2)); // EXPECTED: [0.3815,0.5582,0.7810,1.0794,1.5041,2.0105]
        }

        [Test]
        public void UC02_FpiDataWithSsa()
        {
            double[] Fpi = new double[] {
                64.1, 64.4, 63.8, 65.8, 64.4, 63.7, 62.5, 61.5, 61, 61.1, 61.9, 61.9, 60.8, 61.5, 62, 60.7, 60.6, 61, 61.8, 61.4, 62.1, 64.3, 64.4, 64, 64.8, 65.7, 66.1, 64.5, 64.8, 65.6, 64.4, 62.8, 63.5, 61.5, 62.4, 61, 61.3, 61.2, 62.9, 62.5, 62, 61.5, 61.9, 60.5, 60.9, 61.2, 63, 64.3, 64.5, 64.1, 64.5, 63.7, 65.6, 65.2, 63.9, 66.2, 69.3, 69.9, 73.2, 73.1, 72.9, 74.4, 75.1, 74, 74, 75.1, 79.5, 78, 78.3, 80.4, 79.8, 78, 77, 77.5, 78.6, 80.8, 82.4, 80.5, 80.2, 79.9, 76.9, 74.1, 72.5, 71.4, 70.9, 71, 72.8, 73.4, 72.8, 70.2, 68.4, 69.8, 70, 69.7, 70.3, 68, 67.4, 67.6, 67.4, 66.8, 66.4, 64.2, 64.3, 62.4, 61.9, 62.7, 62.6, 62.1, 61, 58.4, 57, 55.8, 55.4, 54.6, 52.6, 54.4, 54.6, 53.2, 53.2, 51.9, 52.6, 52.3, 52.8, 53.4, 53.6, 53.8, 54, 53.2, 52.6, 53.4, 53.8, 54.4, 53.8, 54.2, 55.1, 54.6, 55.4, 54.8, 56.5, 56.6, 55.4, 54.2, 54.7, 54.3, 53.5, 51.7, 52, 51, 50.5, 51.3, 52, 53.4, 54.9, 55.2, 56.3, 55.6, 55.9, 56.5, 55.5, 55.6, 56.3, 56, 56.7, 57.6, 58.7, 60.3, 61.8, 62.4, 64.3, 65, 66.7, 67.2, 66.3, 66.1, 65.6, 64.5, 65.2, 64.7, 65.4, 65.6, 65.4, 65.9, 67.1, 65.6, 66.3, 67.2, 67.4, 67.5, 68.4, 69.4, 68.8, 69.5, 69.4, 70.4, 69.4, 69.7, 71, 71.1, 72.9, 72.9, 73, 74.9, 77.5, 78.6, 78.1, 79.6, 80.8, 83.5, 86.2, 91.1, 95.3, 98.9, 105, 107.6, 110.4, 114.4, 120, 128, 132.1, 130.4, 130, 132.5, 130.1, 121.1, 112.8, 97.6, 89.5, 86, 86.4, 84.2, 84, 87.2, 93.3, 93.3, 90.7, 93.5, 93, 94.8, 98.8, 100.7, 101.5, 99, 96.3, 96.8, 96.3, 95.7, 100, 107.7, 114.1, 120.1, 124.1, 129.3, 133.7, 137.6, 134.3, 136.4, 135.2, 134.9, 133.2, 133, 130.4, 125.7, 126.1, 122.1, 122.4, 125.4, 125.8, 124.4, 119, 115.9, 122.1, 123.3, 125.2, 124, 123.6, 122.9, 123.4, 123.2, 122.9, 122.9, 122.1, 120.9, 117.9, 116.1, 116.4, 118.7, 118.7, 118.2, 116.2, 118.5, 122.1, 121.4, 121.3, 119.3, 116.4, 113, 109.4, 109.4, 108.2, 105.2, 100.7, 98.5, 95.6, 94.7, 95, 94.5, 93.8, 89.6, 89, 90.4, 87.7, 87.1, 84.9, 86, 87.4, 89.2, 90.6, 93.8, 93, 95.3, 96.1, 95.9, 95.8, 95.1, 97.7, 98.1, 96.2, 95.1, 97.4, 98, 100.2, 99.3, 99.9, 98.9, 98.9, 96.4, 96.7, 97.8, 99, 98.5, 98.6, 96.9, 95, 95.9, 94.2, 93.3, 92.2, 92.2, 93.2, 94, 93.1, 93.6, 94.2, 95.3, 95.1, 94, 93.3, 95.2, 98.6, 101, 102.5, 99.4, 95.1, 92.4, 91.1, 93.2
            };

            SingularSpectrumAnalyzer Ssa = new SingularSpectrumAnalyzer();
            Ssa.SetAddSequences(Fpi);
            Ssa.SetWindow(3);
            Ssa.SetAlgoTopKDirect(1);

            double[] trend;
            double[] noise;
            Ssa.AnalyzeSequence(out trend, out noise);

            string FilePath = AppDomain.CurrentDomain.BaseDirectory
                + @"Result\" + DateTime.Now.ToString("yyyyMMdd-HHmm-ss") + ".csv";
            FileInfo FI = new FileInfo(FilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(FilePath, true))
            {
                file.WriteLine(string.Join(",", trend));
                file.WriteLine(string.Join(",", noise));
            }
        }
    }
}
