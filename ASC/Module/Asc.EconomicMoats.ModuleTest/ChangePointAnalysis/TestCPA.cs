using EconomicMoats.Standard;
using EconomicMoats.Mudule;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace EconomicMoat.ModuleTest
{
    class TestCPA
    {
        [Test]
        public void UC01_TestCPA_Fpi()
        {
            ChangePointAnalysis Cpa = new ChangePointAnalysis();
            //Cpa._dataPath = @"C:\Workspace\Branches\CircularEconomyAndGreenTechnology\EconomicMoat\EconomicMoat\EconomicMoats.ModuleTest\ChangePointAnalysis\Data\FPI_jul20_CPA.csv";
            Cpa._dataPath = @"C:\Workspace\Branches\CircularEconomyAndGreenTechnology\EconomicMoat\EconomicMoat\EconomicMoats.ModuleTest\ChangePointAnalysis\Data\FpiAfterSsaAsTrend.csv"; 
            Cpa.RunAnalysis();
        }
    }
}
