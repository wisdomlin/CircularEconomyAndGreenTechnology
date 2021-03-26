using System;
using System.Collections.Generic;
using System.Text;

namespace Asc
{   
    public class RelMaxAlarm : IJudge
    {
        private GenericValue SV;
        private GenericValue AH;

        public RelMaxAlarm(GenericValue SV, GenericValue AH)
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
