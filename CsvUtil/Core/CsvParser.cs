using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using jUtils.Core.Configuration;
using jUtils.Models;

namespace jUtils.Core
{
    public class CsvParser
    {
        private Config _config;

        public CsvParser(Config configuration)
        {
            _config = configuration;
        }

        public JMeterData Parse()
        {
            var path = _config.InputPath;
            Console.WriteLine("Loading csv report...");
            var list = new List<JRow>();
            using (var stream = new StreamReader(path))
            {
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    var row = new JRow(line.Split(','));
                    list.Add(row);
                }
            }
            Console.WriteLine("Parsed!");
            return new JMeterData(list, "");
        }



    }
}
