namespace SamplingItemBase
{
    public static class SamplingItemFactory
    {
        public static SamplingItem CreateSamplingItem(string SamplingItemType)
        {
            SamplingItem item;
            if (SamplingItemType == "Opacity")
                return item = new Opacity();
            else if(SamplingItemType == "SulfurDioxide")
                return item = new SulfurDioxide();
            else if (SamplingItemType == "NitrogenOxides")
                return item = new NitrogenOxides();
            else if (SamplingItemType == "CarbonMonoxide")
                return item = new CarbonMonoxide();
            else if (SamplingItemType == "TotalReducedSulfur")
                return item = new TotalReducedSulfur();
            else if (SamplingItemType == "HydrogenChloride")
                return item = new HydrogenChloride();
            else if (SamplingItemType == "VolatileOrganicLiquid")
                return item = new VolatileOrganicLiquid();
            else if (SamplingItemType == "Oxygen")
                return item = new Oxygen();
            else if (SamplingItemType == "FlowRate")
                return item = new FlowRate();
            else if (SamplingItemType == "Temperature")
                return item = new Temperature();
            else
                return null;

            // TODO: Fatory Pattern by C# Reflection with .NET Standard 2.0
        }
    }
}
