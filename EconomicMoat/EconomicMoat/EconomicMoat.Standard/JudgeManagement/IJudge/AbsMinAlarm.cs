using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class AbsMinAlarm : IJudge
    {
        private SpecValue AL;

        public AbsMinAlarm(SpecValue AL)
        {
            this.AL = AL;
        }

        public bool Judge(PresentValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
