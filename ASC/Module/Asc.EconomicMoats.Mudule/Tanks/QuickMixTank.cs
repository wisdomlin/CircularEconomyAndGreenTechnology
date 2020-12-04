using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asc
{
    public class QuickMixTank
    {
        public List<DateTime> List_Dt;
        public List<double> Ts;

        public string TankID;
        public double DataAvg;
        public int DataAmount
        {
            get { return Ts.Count; }
        }

        public QuickMixTank(string tankType)
        {
            TankID = tankType;
            Initialize();
        }

        private void Initialize()
        {
            DataAvg = 0;
            List_Dt = new List<DateTime>();
            Ts = new List<double>();
        }

        public void Reset()
        {
            DataAvg = 0;
            List_Dt.Clear();
            Ts.Clear();
        }

        public bool ComputeAvg()
        {
            try
            {
                DataAvg = Ts.Count > 0 ? Ts.Average() : 0.0;    // Use [0.0] instead of [double.NaN] in this case
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                return false;
            }
        }
    }
}
