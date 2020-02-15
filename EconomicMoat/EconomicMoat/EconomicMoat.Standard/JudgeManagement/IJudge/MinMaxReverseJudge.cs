using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class MinMaxReverseJudge : IJudge
    {
        private SpecValue AlarmHigh;

        public MinMaxReverseJudge(SpecValue AlarmHigh)
        {
            this.AlarmHigh = AlarmHigh;
        }

        public bool Judge(PresentValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
