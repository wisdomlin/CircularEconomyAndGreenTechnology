using System;
using System.Collections.Generic;
using System.Text;

namespace Asc
{   
    public class RelMinAlarm : IJudge
    {
        private GenericValue SV;
        private GenericValue AL;

        public RelMinAlarm(GenericValue SV, GenericValue AL)
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
