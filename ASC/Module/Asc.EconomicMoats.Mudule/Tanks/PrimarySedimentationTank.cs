using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asc
{
    public class PrimarySedimentationTank
    {
        public string TankType;
        public double DataAvg;
        public int DataAmount;

        public PrimarySedimentationTank(string tankType)
        {
            TankType = tankType;
            Initialize();
        }

        private void Initialize()
        {
            DataAvg = 0;
            DataAmount = 0;
        }

        public bool Aggregate(int N2, double G2)
        {
            try
            {
                if (N2 == 0)
                {
                    // Do Nothing
                }
                else
                {
                    double G1 = DataAvg;
                    int N1 = DataAmount;
                    DataAvg = (G1 * (N1 / N2) + G2) / ((N1 / N2) + 1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return false;
            }
            return true;
        }
    }
}
