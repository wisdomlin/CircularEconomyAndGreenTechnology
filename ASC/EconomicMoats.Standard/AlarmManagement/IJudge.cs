using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoats.Standard
{
    interface IJudge
    {
        bool Judge(GenericValue PV);
    }
}
