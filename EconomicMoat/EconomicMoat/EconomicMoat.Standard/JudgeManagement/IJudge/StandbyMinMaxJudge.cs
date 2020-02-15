using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class StandbyMinMaxJudge : IJudge
    {
        private SpecValue AlarmHigh;

        public StandbyMinMaxJudge(SpecValue AlarmHigh)
        {
            this.AlarmHigh = AlarmHigh;
        }

        public bool Judge(PresentValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
