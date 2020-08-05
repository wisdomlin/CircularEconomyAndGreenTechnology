using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoats.Standard
{   
    public class AbsMinAlarm : IJudge
    {
        private GenericValue AL;

        public AbsMinAlarm(GenericValue AL)
        {
            this.AL = AL;
        }

        public bool Judge(GenericValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
