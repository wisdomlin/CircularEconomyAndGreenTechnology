using System;
using System.Collections.Generic;
using System.Text;

namespace Asc
{
    public class SingularSpectrumAnalyzer
    {

        //
        // Here we demonstrate SSA trend/noise separation for some toy problem:
        // small monotonically growing series X are analyzed with 3-tick window
        // and "top-K" version of SSA, which selects K largest singular vectors
        // for analysis, with K=1.
        //
        private alglib.ssamodel s;
        private double[] Seq;

        public SingularSpectrumAnalyzer()
        {
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
        }

        public void SetAddSequences(double[] Os)
        {
            Seq = Os;
            alglib.ssaaddsequence(s, Os);
        }

        public void SetWindow(int WindowWidth)
        {
            alglib.ssasetwindow(s, WindowWidth);
        }

        public void SetAlgoTopKDirect(int TopK)
        {
            alglib.ssasetalgotopkdirect(s, TopK);
        }

        public void AnalyzeSequence(out double[] trend, out double[] noise)
        {
            //
            // Now we begin analysis. Internally SSA model stores everything it needs:
            // data, settings, solvers and so on. Right after first call to analysis-
            // related function it will analyze dataset, build basis and perform analysis.
            //
            // Subsequent calls to analysis functions will reuse previously computed
            // basis, unless you invalidate it by changing model settings (or dataset).
            //
            alglib.ssaanalyzesequence(s, Seq, out trend, out noise);
        }
    }
}
