

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Asc
{
    class TestChangePointAnalyzer
    {
        [Test]
        public void UC01_TestCPA_Fpi()
        {
            ChangePointAnalyzer Cpa = new ChangePointAnalyzer();
            //Cpa._dataPath = @"C:\Workspace\Branches\CircularEconomyAndGreenTechnology\EconomicMoat\EconomicMoat\EconomicMoats.ModuleTest\ChangePointAnalysis\Data\FPI_jul20_CPA.csv";
            Cpa._InputDataPath = @"C:\Workspace\Branches\CircularEconomyAndGreenTechnology\EconomicMoat\EconomicMoat\EconomicMoats.ModuleTest\ChangePointAnalysis\Data\FpiAfterSsaAsTrend.csv";
            Cpa._docsize = 366;
            Cpa.RunAnalysis();
        }
    }
}
