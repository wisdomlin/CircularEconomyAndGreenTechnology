using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class AbsMaxAlarm : IJudge
    {
        private SpecValue AH;

        public AbsMaxAlarm(SpecValue AH)
        {
            this.AH = AH;
        }

        public bool Judge(PresentValue PV)
        {
            bool result = false;
            int CompareResult = PV.CompareTo(AH);
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
