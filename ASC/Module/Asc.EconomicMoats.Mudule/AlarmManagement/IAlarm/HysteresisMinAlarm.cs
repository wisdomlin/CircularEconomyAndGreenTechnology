using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoats.Standard
{   
    public class HysteresisMinAlarm : IJudge
    {
        private GenericValue SV;
        private GenericValue AL;

        public HysteresisMinAlarm(GenericValue SV, GenericValue AL)
        {
            this.SV = SV;
            this.AL = AL;
        }

        public bool Judge(GenericValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
