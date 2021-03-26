using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asc
{
    public class PrimarySedimentationTank
    {
        public string TankId;
        public double DataAvg;
        public int DataAmount;

        public PrimarySedimentationTank(string tankType)
        {
            TankId = tankType;
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
                    DataAvg = (G1 * ((double)N1 / (double)N2) + G2) / (((double)N1 / (double)N2) + 1);
                    DataAmount += N2;
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
