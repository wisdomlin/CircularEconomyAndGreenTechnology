using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class StandbyMinAlarm : IJudge
    {
        private SpecValue SV;
        private SpecValue AL;

        public StandbyMinAlarm(SpecValue SV, SpecValue AL)
        {
            this.SV = SV;
            this.AL = AL;
        }

        public bool Judge(PresentValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
