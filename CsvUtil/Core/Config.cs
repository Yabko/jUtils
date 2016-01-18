using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvUtil.Core
{
    public class Config
    {
        public string InputPath { get; private set; }
        public string OutputPath { get; private set; }
        public string TemplatePath { get { return @"Templates\template.html"; } }

        /// <summary>
        /// we expect next args format provided for this console utility:
        /// app.exe target.csv output.html
        /// without any additional keys at the moment
        /// </summary>
        /// <param name="args"></param>
        public Config(string[] args)
        {
            if (args.Length < 2) throw new ArgumentException("App has to be started with two required parameters, csv file and output file");
            InputPath = args[0];
            OutputPath = args[1];
        }


    }
}
