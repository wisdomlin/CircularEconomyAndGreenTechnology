using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class AbsMinMaxAlarm : IJudge
    {
        private SpecValue AL;
        private SpecValue AH;

        public AbsMinMaxAlarm(SpecValue AL, SpecValue AH)
        {
            this.AL = AL;
            this.AH = AH;
        }

        public bool Judge(PresentValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
