using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EconomicMoats.Standard
{
    public class OutlierTrimmingIndicator
    {
        private double[] source;
        private bool isSourceSet = false;
        private double ConfidenceInterval_Lower;
        private double ConfidenceInterval_Upper;
        private double Med;
        private double MAD;

        public void SetOringinalSeries(double[] remainingErrorComponent)
        {
            source = remainingErrorComponent;
            isSourceSet = true;
            OutlierIndicatorProcedure();
        }

        private void OutlierIndicatorProcedure()
        {
            if (isSourceSet == true)
            {
                Array.Sort(source);

                Med = source.Length % 2 == 0
                  ? (source[source.Length / 2 - 1] + source[source.Length / 2]) / 2.0
                  : source[source.Length / 2];

                double[] d = source
                  .Select(x => Math.Abs(x - Med))
                  .OrderBy(x => x)
                  .ToArray();

                MAD = 1.4826 * (d.Length % 2 == 0
                  ? (d[d.Length / 2 - 1] + d[d.Length / 2]) / 2.0
                  : d[d.Length / 2]);

                ConfidenceInterval_Lower = Med - 3 * MAD;
                ConfidenceInterval_Upper = Med + 3 * MAD;
            }
            else
            {
                // throw new No Source Exception
            }
        }

        public void GetMad(out double Mad)
        {
            Mad = MAD;
        }

        public void GetMed(out double M)
        {
            M = Med;
        }

        public void GetConfidenceIntervals(out double lower, out double upper)
        {
            lower = ConfidenceInterval_Lower;
            upper = ConfidenceInterval_Upper;
        }

        public void GetOutliers(out double[] outliers)
        {
            List<double> outliersList = new List<double>();
            foreach (double s in source)
            {
                if (s <= ConfidenceInterval_Lower || ConfidenceInterval_Upper <= s)
                    outliersList.Add(s);
            }
            outliers = outliersList.ToArray();
        }
    }
}
