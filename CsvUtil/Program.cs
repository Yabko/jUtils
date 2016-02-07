using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using jUtils.Core;
using jUtils.Core.Configuration;
using jUtils.Core.Processing;

namespace jUtils
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome in J Utility v." + Assembly.GetExecutingAssembly().GetName().Version);
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
