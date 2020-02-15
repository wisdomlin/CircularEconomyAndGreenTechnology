using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class StandbyMaxJudge : IJudge
    {
        private SpecValue AlarmHigh;

        public StandbyMaxJudge(SpecValue AlarmHigh)
        {
            this.AlarmHigh = AlarmHigh;
        }

        public bool Judge(PresentValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
