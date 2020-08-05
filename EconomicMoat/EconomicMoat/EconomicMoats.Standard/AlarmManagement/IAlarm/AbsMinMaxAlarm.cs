using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoats.Standard
{   
    public class AbsMinMaxAlarm : IJudge
    {
        private GenericValue AL;
        private GenericValue AH;

        public AbsMinMaxAlarm(GenericValue AL, GenericValue AH)
        {
            this.AL = AL;
            this.AH = AH;
        }

        public bool Judge(GenericValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
