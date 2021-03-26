using System;
using System.Collections.Generic;
using System.Text;

namespace Asc
{   
    public class StandbyMinAlarm : IJudge
    {
        private GenericValue SV;
        private GenericValue AL;

        public StandbyMinAlarm(GenericValue SV, GenericValue AL)
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
