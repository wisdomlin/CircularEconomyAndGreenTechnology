using System;
using System.Collections.Generic;
using System.Data;

namespace DBInMemoryEngine
{
    public sealed class RawDataStore
    {
        /* Singleton */
        private static readonly Lazy<RawDataStore> lazy =
        new Lazy<RawDataStore>(() => new RawDataStore());
        public static RawDataStore Instance { get { return lazy.Value; } }

        /* Constructor */
        private RawDataStore()
        {
            dtRawData = new DataTable("RawData");
            DataColumn[] cols ={
                new DataColumn("Timestamp",typeof(string)),
                new DataColumn("Data",typeof(string)),
            };
            dtRawData.Columns.AddRange(cols);
        }

        /* dtRawData */
        private DataTable dtRawData;
        public void AddRawData(double d)
        {
            DataRow row1 = dtRawData.NewRow();
            row1["Timestamp"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            row1["Data"] = d.ToString("0.000");
            dtRawData.Rows.Add(row1);
        }

        public void ClearRawDataStore()
        {
            dtRawData.Clear();
        }

        public void GetNminsRawData(DateTime t, int N, out double[] rows)
        {
            string start = t.Subtract(new TimeSpan(0, N, 0)).ToString("yyyy-MM-dd HH:mm:ss.fff");
            string end = t.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string query = "Timestamp >= #" + start + "# AND Timestamp <= #" + end + "#";
            DataRow[] answer = dtRawData.Select(query);
            List<double> list = new List<double>();

            foreach (DataRow r in answer)
            {
                double d = Convert.ToDouble(r["Data"].ToString());
                list.Add(d);
            }
            rows = list.ToArray();
        }
    }
}
