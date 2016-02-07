using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jUtils.Models
{
    public class JRow
    {
        #region Properties

        public DateTime TimeStamp { get; set; }
        public int Elapsed { get; set; }
        public string Label { get; set; }
        public int ResponceCode { get; set; }
        public string ResponceMessage { get; set; }
        public int Thread { get; set; }
        public string DataType { get; set; }
        public bool Success { get; set; }
        public int Bytes { get; set; }
        public int GroupThreads { get; set; }
        public int AllThreads { get; set; }
        public int Latency { get; set; }

        #endregion

        #region Constructor

        public JRow(CsvRow raw)
        {
            TimeStamp = convertUnixTimeStampToDateTime(double.Parse(raw.PlainData[0]));
            Elapsed = int.Parse(raw.PlainData[1]);
            int code = 0;
            int.TryParse(raw.PlainData[3], out code);
            ResponceCode = code;
        }

        #endregion

        #region Methods

        private static DateTime convertUnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        #endregion
    }
}
