using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class MaxJudge : IJudge
    {
        private SpecValue AlarmHigh;

        public MaxJudge(SpecValue AlarmHigh)
        {
            this.AlarmHigh = AlarmHigh;
        }

        public bool Judge(PresentValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
