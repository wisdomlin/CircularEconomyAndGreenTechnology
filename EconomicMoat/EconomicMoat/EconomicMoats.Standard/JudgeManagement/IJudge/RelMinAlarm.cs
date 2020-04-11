using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class RelMinAlarm : IJudge
    {
        private SpecValue SV;
        private SpecValue AL;

        public RelMinAlarm(SpecValue SV, SpecValue AL)
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
