using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoats.Standard
{   
    public class HysteresisMaxAlarm : IJudge
    {
        private GenericValue SV;
        private GenericValue AH;

        public HysteresisMaxAlarm(GenericValue SV, GenericValue AH)
        {
            this.SV = SV;
            this.AH = AH;
        }

        public bool Judge(GenericValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
