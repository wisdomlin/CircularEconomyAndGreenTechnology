using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class HysteresisMinJudge : IJudge
    {
        private SpecValue AlarmHigh;

        public HysteresisMinJudge(SpecValue AlarmHigh)
        {
            this.AlarmHigh = AlarmHigh;
        }

        public bool Judge(PresentValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
