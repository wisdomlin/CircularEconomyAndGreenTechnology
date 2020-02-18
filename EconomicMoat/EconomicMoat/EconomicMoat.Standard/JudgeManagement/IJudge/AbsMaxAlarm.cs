using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class AbsMaxAlarm : IJudge
    {
        private SpecValue AlarmHigh;

        public AbsMaxAlarm(SpecValue AlarmHigh)
        {
            this.AlarmHigh = AlarmHigh;
        }

        public bool Judge(PresentValue PV)
        {
            bool result = false;
            int CompareResult = PV.CompareTo(AlarmHigh);
            if (CompareResult > 0)
            {
                // PV > AlarmHigh
                result = true;
            }
            else if (CompareResult == 0)
            {
                // PV = AlarmHigh
                result = false;
            }
            else
            {
                // PV < AlarmHigh
                result = false;
            }
            return result;
        }
    }
}
