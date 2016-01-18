using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CsvUtil.Core
{
    public class CsvParser
    {
        private Config _config;

        public CsvParser(Config configuration)
        {
            _config = configuration;
        }

        public string Parse()
        {
            var path = _config.InputPath;
            var stream = new StreamReader(path);
            Console.WriteLine("Loading csv report...");
            var text = stream.ReadToEnd();
            Console.WriteLine("Loaded!");
            Console.WriteLine(text);

            return text;
        }



    }
}
