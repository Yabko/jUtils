using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvUtil.Core.Configuration;
using CsvUtil.Models;

namespace CsvUtil.Core
{
    public class CsvParser
    {
        private Config _config;

        public CsvParser(Config configuration)
        {
            _config = configuration;
        }

        public CsvData Parse()
        {
            var path = _config.InputPath;
            var data = new CsvData();

            Console.WriteLine("Loading csv report...");
            using (var stream = new StreamReader(path))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    var row = new CsvRow();
                    row.PlainData = line.Split(',');
                    data.Rows.Add(row);
                }
            }
            Console.WriteLine("Parsed!");
            return data;
        }



    }
}
