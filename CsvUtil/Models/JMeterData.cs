using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqStatistics;

namespace CsvUtil.Models
{
    public class JMeterData
    {
        #region Properties
        public List<JRow> Rows { get; private set; }

        public string MethodName { get; set; }

        public Dictionary<string, object> Summary { get; set; }
        #endregion

        #region Constructor
        public JMeterData(List<CsvRow> rows, string method)
        {
            MethodName = method;
            Rows = new List<JRow>(rows.Count);
            Summary = new Dictionary<string, object>();
            foreach (var csvRow in rows)
            {
                Rows.Add(new JRow(csvRow));
            }

            prepareSummary();
        }

        private void prepareSummary()
        {
            var first = Rows.First();
            var last = Rows.Last();

            Summary["Test Start Time"] = first.TimeStamp.ToString(CultureInfo.InvariantCulture);
            Summary["Test Duration"] = (last.TimeStamp.AddMilliseconds(last.Elapsed) - first.TimeStamp).TotalSeconds + " seconds";
            Summary["Transactions Count"] = Rows.Count;
            Summary["Average Response Time"] = Rows.Average(it => it.Elapsed).ToString("N2") + " ms";
            Summary["Median Response Time"] = Rows.Median(it => it.Elapsed) + " ms";
            Summary["Min Response Time"] = Rows.Min(it => it.Elapsed) + " ms";
            Summary["Max Response Time"] = Rows.Max(it => it.Elapsed) + " ms";
            Summary["Response Time Deviation"] = Rows.StandardDeviation(it => it.Elapsed).ToString("N2") + " ms";
            var mode = Rows.Mode(it => it.Elapsed);
            var count = ((double)Rows.Count(it => it.Elapsed == mode) / Rows.Count * 100).ToString("N2");
            Summary[$"Most Common Value ({count}%)"] = mode + "ms";


        }

        #endregion

        #region Methods


        #endregion

    }
}
