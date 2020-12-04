using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;

namespace Asc
{
    public class Dal_EPA_IoT_Station : DatalineAnalysisLogic
    {
        public List<string> DeviceIdList;
        public Dictionary<string, QuickMixTank> Dic_Qmt;
        public bool useDeviceIdList = true;

        public Dal_EPA_IoT_Station()
        {

        }

        internal override void CustomizedAnalyze(string Line)
        {
            string[] LineSplits = Line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);

            bool isDeviceIdContained = true;
            if (useDeviceIdList)
                isDeviceIdContained = DeviceIdList.Contains(LineSplits[0]);

            bool isFormatRight = (LineSplits.Length == 5);
            bool isField_NonZero = true;
            bool isField_NonSpace = true;
            if (isFormatRight)
            {
                decimal actNumber;
                //{ /* ... */ }
                isField_NonZero =
                    //(LineSplits[1] != "0") && (LineSplits[2] != "0") && (LineSplits[3] != "0");
                    (decimal.TryParse(LineSplits[1], out actNumber) && actNumber > 0) &&
                    (decimal.TryParse(LineSplits[2], out actNumber) && actNumber > 0) &&
                    (decimal.TryParse(LineSplits[3], out actNumber) && actNumber > 0);
                isField_NonSpace =
                    (LineSplits[1].Trim(' ') != "") && (LineSplits[2].Trim(' ') != "") && (LineSplits[3].Trim(' ') != "");
            }

            // 1.1 把 [deviceId] 不在 [EPAIoT_station_Taichung] 資料集的測站資料篩掉。(Contains)
            if (isFormatRight && isDeviceIdContained && isField_NonZero && isField_NonSpace)
            {
                // 1.2 根據 [time] 欄位值，將 [PM2.5] 欄位值，分配至 相對應時間之[快混池] x6 (年、季、月、週、日、時)
                string DeviceID = LineSplits[0];
                string PM2_5 = LineSplits[1];
                string time = LineSplits[4];
                DistributeToEachQuickMixTank(DeviceID, PM2_5, time);
            }
            else
            {

            }
        }

        private void DistributeToEachQuickMixTank(string DeviceID, string pM2_5, string time)
        {
            // TODO: Be Care of the Convert Exception! Catch it and Write Log.
            DateTime oDate = DateTime.Parse(time);
            double oVal = Convert.ToDouble(pM2_5);

            // 根據 [time] 欄位值，將 [PM2.5] 欄位值，分配至 相對應 [時間模式] 之 [快混池] (月、週、日、時)
            DistributeQmt_Month(oDate, oVal);
            DistributeQmt_Week(oDate, oVal);
            DistributeQmt_Day(oDate, oVal);
            DistributeQmt_Hour(oDate, oVal);

            // 根據 [time] 欄位值，將 [PM2.5] 欄位值，分配至 相對應 [空間模式] 之 [快混池] (年均值、季均值)
            DistributeQmt_Year(oDate, DeviceID, oVal);
            DistributeQmt_Season(oDate, DeviceID, oVal);
        }

        private void DistributeQmt_Hour(DateTime oDate, double oVal)
        {
            string t = "Hour_" + "HH" + oDate.ToString("HH");
            if (Dic_Qmt.TryGetValue(t, out QuickMixTank Qmt))
            {
                // Add
                Qmt.List_Dt.Add(oDate);
                Qmt.Ts.Add(oVal);
            }
            else
            {
                // Exception
                throw new Exception();
            }
        }

        private void DistributeQmt_Day(DateTime oDate, double oVal)
        {
            string t = "Day_" + "dd" + oDate.ToString("dd");
            if (Dic_Qmt.TryGetValue(t, out QuickMixTank Qmt))
            {
                // Add
                Qmt.List_Dt.Add(oDate);
                Qmt.Ts.Add(oVal);
            }
            else
            {
                // Exception
                throw new Exception();
            }
        }

        private void DistributeQmt_Month(DateTime oDate, double oVal)
        {
            string t = "Month_" + "MM" + oDate.ToString("MM");
            if (Dic_Qmt.TryGetValue(t, out QuickMixTank Qmt))
            {
                // Add
                Qmt.List_Dt.Add(oDate);
                Qmt.Ts.Add(oVal);
            }
            else
            {
                // Exception
                throw new Exception();
            }
        }

        private void DistributeQmt_Week(DateTime oDate, double oVal)
        {
            //string t = oDate.ToString("HH");
            // Seriously cheat.  If it is Monday, Tuesday or Wednesday, it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(oDate);
            //if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            //{
            //    oDate = oDate.AddDays(3);
            //}

            // Return the week of our adjusted day
            //int result = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(oDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            int result = (int)day;
            string t = "Week_" + "ww" + result.ToString("00");
            if (Dic_Qmt.TryGetValue(t, out QuickMixTank Qmt))
            {
                // Add
                Qmt.List_Dt.Add(oDate);
                Qmt.Ts.Add(oVal);
            }
            else
            {
                // Exception
                throw new Exception();
            }
        }

        private void DistributeQmt_Year(DateTime oDate, string DeviceID, double oVal)
        {
            string t = "Y" + oDate.ToString("yyyy") + "_" + DeviceID;
            if (Dic_Qmt.TryGetValue(t, out QuickMixTank Qmt))
            {
                // Add
                Qmt.List_Dt.Add(oDate);
                Qmt.Ts.Add(oVal);
            }
            else
            {
                // Exception
                //throw new Exception();
            }
        }

        private void DistributeQmt_Season(DateTime oDate, string DeviceID, double oVal)
        {
            string t = "Y" + oDate.ToString("yyyy") + "S" + (getSeason(oDate, false) + 1).ToString("00") + "_" + DeviceID;
            if (Dic_Qmt.TryGetValue(t, out QuickMixTank Qmt))
            {
                // Add
                Qmt.List_Dt.Add(oDate);
                Qmt.Ts.Add(oVal);
            }
            else
            {
                // Exception
                //throw new Exception();
            }
        }

        private int getSeason(DateTime date, bool ofSouthernHemisphere)
        {
            int hemisphereConst = (ofSouthernHemisphere ? 2 : 0);
            Func<int, int> getReturn = (northern) =>
            {
                return (northern + hemisphereConst) % 4;
            };
            float value = (float)date.Month + date.Day / 100f;  // <month>.<day(2 digit)>
            if (value < 3.21 || value >= 12.22) return getReturn(3);    // 3: Winter
            if (value < 6.21) return getReturn(0);  // 0: Spring
            if (value < 9.23) return getReturn(1);  // 1: Summer
            return getReturn(2);    // 2: Autumn
        }

    }
}
