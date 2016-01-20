using CsvUtil.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvUtil.Core.Configuration;
using CsvUtil.Core.Processing;

namespace CsvUtil
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Velcome in CSV Utility v." + Assembly.GetExecutingAssembly().GetName().Version);
            var config = new Config(args);
            var datestart = DateTime.Now;
            Console.WriteLine("Processing started at: {0}", datestart);
            var parser = new CsvParser(config);
            var processor = new CsvProcessor(config);

            var data = parser.Parse();
            processor.CreateHtmlResult(data);

            var dateend = DateTime.Now;
            Console.WriteLine("Total Duration: {0} sec.", (dateend - datestart).TotalSeconds);
            //Console.ReadKey();
        }
    }
}
