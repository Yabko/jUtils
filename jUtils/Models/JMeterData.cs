using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqStatistics;
using jUtils.Core.Configuration;

namespace jUtils.Models
{
    public class JMeterData
    {


        #region Properties

        public string MethodName { get; set; }
        public List<JRow> Rows { get; private set; }

        public int CoolMsTime
        {
            get { return _coolMsTime; }
            set { _coolMsTime = value; }
        }
        private int _coolMsTime = JsonConfig.coolTime;

        public int NormMsTime
        {
            get { return _normMsTime; }
            set { _normMsTime = value; }
        }
        private int _normMsTime = JsonConfig.okTime;

        #endregion

        #region Constructor
        public JMeterData(List<JRow> rows, string method)
        {
            MethodName = method;
            Rows = rows;
        }

        public Dictionary<string, object> PrepareSummary()
        {
            var summary = new Dictionary<string, object>();
            var first = Rows.First();
            var last = Rows.Last();

            summary["Test Start Time"] = first.TimeStamp.ToString(CultureInfo.InvariantCulture);
            summary["Test Duration"] = (last.TimeStamp.AddMilliseconds(last.Elapsed) - first.TimeStamp).TotalSeconds + " seconds";
            summary["Transactions Count"] = Rows.Count;
            summary["Average Response Time"] = Rows.Average(it => it.Elapsed).ToString("N2") + " ms";
            summary["Median Response Time"] = Rows.Median(it => it.Elapsed) + " ms";
            summary["Min Response Time"] = Rows.Min(it => it.Elapsed) + " ms";
            summary["Max Response Time"] = Rows.Max(it => it.Elapsed) + " ms";
            if (Rows.Count > 1)
            {
                summary["Response Time Deviation"] = Rows.StandardDeviation(it => it.Elapsed).ToString("N2") + " ms";
            }
            summary["Code"] = Rows.Select(it => it.ResponceCode).Distinct().Aggregate("", (seed, code) => seed + " " + code);

            var mode = Rows.Mode(it => it.Elapsed);
            var count = ((double)Rows.Count(it => it.Elapsed == mode) / Rows.Count * 100).ToString("N2");
            summary[$"Most Common Value ({count}%)"] = mode + "ms";
            return summary;
        }

        public List<string> AnaliseResponseTimes()
        {
            // Method ,
            var analysis = new List<string>();
            analysis.Add(MethodName);
            var coolRows = Rows.Where(row => row.Elapsed <= CoolMsTime).ToList();
            var okRows = Rows.Where(row => row.Elapsed <= NormMsTime && row.Elapsed > CoolMsTime).ToList();
            var badRows = Rows.Where(row => row.Elapsed > NormMsTime).ToList();
            var coolMean = coolRows.Count == 0 ? 0 : coolRows.Median(it => it.Elapsed);
            var okMean = okRows.Count == 0 ? 0 : okRows.Median(it => it.Elapsed);
            var badMean = badRows.Count == 0 ? 0 : badRows.Median(it => it.Elapsed);

            var coolCount = coolRows.Count();
            var okCount = okRows.Count();

            // Cool Time %, OK Time %, No Ok %,
            analysis.Add(((double)coolCount / Rows.Count * 100).ToString("N2") + " %");
            analysis.Add(((double)okCount / Rows.Count * 100).ToString("N2") + " %");
            analysis.Add(((double)badRows.Count() / Rows.Count * 100).ToString("N2") + " %");

            analysis.Add(coolMean.ToString("N2"));
            analysis.Add(okMean.ToString("N2"));
            analysis.Add(badMean.ToString("N2"));

            return analysis;
        }

        public List<string> AnalyseAverage()
        {
            var avgAnalysis = new List<string>();
            return avgAnalysis;
        }



        #endregion

        #region Methods


        #endregion

    }
}
