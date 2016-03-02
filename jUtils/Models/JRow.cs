using System;

namespace jUtils.Models
{
    public class JRow
    {
        #region Properties

        public DateTime TimeStamp { get; set; }
        public int Elapsed { get; set; }
        public string Label { get; set; }
        public string ResponceCode { get; set; }
        public string ResponceMessage { get; set; }
        public int Thread { get; set; }
        public string DataType { get; set; }
        public bool Success { get; set; }
        public int Bytes { get; set; }
        public int GroupThreads { get; set; }
        public int AllThreads { get; set; }
        public int Latency { get; set; }


        /*
        th>Time Stamp</th>
        <th>Elapsed</th>
        <th>Label</th>
        <th>Responce Code</th>
        <th>Responce Message</th>
        <th>Thread</th>
        <th>Data type</th>
        <th>Success</th>
        <th>Bytes</th>
        <th>Group Threads</th>
        <th>All Threads</th>
        <th>Latency</th>
        */
        public string[] PlainData { get; set; }

        #endregion

        #region Constructor

        public JRow(string[] raw)
        {
            PlainData = raw;
            TimeStamp = convertUnixTimeStampToDateTime(double.Parse(raw[0]));
            Elapsed = int.Parse(raw[1]);
            Label = raw[2];
            ResponceCode = raw[3];
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
