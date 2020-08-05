using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoats.Standard
{
    public class AbsMaxAlarm : IJudge
    {
        GenericValue AH;
        public AbsMaxAlarm(GenericValue AH)
        {
            this.AH = AH;
        }

        public bool Judge(GenericValue PV)
        {
            int CompareResult = PV.CompareTo(AH);
            bool result;
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
