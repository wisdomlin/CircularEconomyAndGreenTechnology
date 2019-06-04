# CircularEconomyAndGreenTechnology

Example for 需求、測試、設計
## 需求分析
* 在這個做垃圾集中與分類的工廠中，需要一個**連續自動監測設施 (Continuous Auto Monitoring Systems，簡稱 CAMS)**，它可以連續自動「*採樣 (Sampling)*」、「*分析 (Analyzing)*」 與「*記錄 (Recording)*」。監測設施有許多種類，例如：粒狀污染物、氣狀污染物、稀釋氣體、排放流率等。
* **粒狀污染物監測設施 (CAMS_ParticulateMatters)**，其量測項目為：**不透光率 (Opacity)**。
* **氣狀污染物監測設施 (CAMS_GaseousPollutants)**，其量測項目為：**二氧化硫 (SulfurDioxide)**、**氮氧化物 (NitrogenOxides)**、**一氧化碳 (CarbonMonoxide)**、**總還原硫 (TotalReducedSulfur)**、**氯化氫 (HydrogenChloride)**、**揮發性有機物 (VolatileOrganicLiquid)**。
* **稀釋氣體監測設施 (CAMS_DilutionGas)**，其量測項目為：**氧氣 (Oxygen)**。
* **排放流率監測設施 (CAMS_FlowRate)**，其量測項目為：**排放流率 (FlowRate)**、**溫度 (Temperature)**。
* 各種監測設施之**量測項目 (SamplingItem)**，將隨現實演進增減。

## 測試案例
```C#
[TestMethod]
public void TestNewCAMS_ParticulateMatters()
{
    CAMS_ParticulateMatters CAMS_PM = new CAMS_ParticulateMatters();
    CAMS_PM.SamplingItemList.Add(new Opacity());
    Assert.IsNotNull(CAMS_PM);
}

[TestMethod]
public void TestSamplingItemFactory()
{
    CAMS_ParticulateMatters CAMS_PM = new CAMS_ParticulateMatters();
    XDocument doc = XDocument.Load("CAMS_SamplingItemList.xml");
    var query =
        from c in doc.Descendants("CAMS_SamplingItem")
        where (string)c.Attribute("CAMS") == "CAMS_ParticulateMatters"
        select c;
    foreach (XElement e in query)
    {
        SamplingItemBase item = SamplingItemFactory.CreateSamplingItem(
            (string)e.Attribute("SamplingItem"));
        CAMS_PM.SamplingItemList.Add(item);
    }
    Assert.IsNotNull(CAMS_PM);
}
```

## 設計圖
![https://ithelp.ithome.com.tw/upload/images/20181022/20107753BQ2hIkZ1Od.png](https://ithelp.ithome.com.tw/upload/images/20181022/20107753BQ2hIkZ1Od.png)
