using System;

namespace jUtils.Core.Configuration
{
    public class Config
    {
        public string InputPath { get; private set; }
        public string OutputPath { get; private set; }
        public string ErrorsOutputPath { get; private set; }
        public string AnalysisOutputPath { get; private set; }

        public string CssName { get { return "style.css"; } }
        public string CssFilePath { get { return $@"Templates\{CssName}"; } }

        public string HtmlPageTemplatePath { get { return @"Templates\template.html"; } }
        public string AllResultsTableTemplatePath { get { return @"Templates\jAll.html"; } }
        public string SummaryTableTemplatePath { get { return @"Templates\jSummary.html"; } }
        public string AnalysisTableTemplatePath { get { return @"Templates\jAnalysis.html"; } }



        #region Constructor

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
            if (args.Length > 2)
            {
                ErrorsOutputPath = args[2];
                if (args.Length > 3)
                {
                    AnalysisOutputPath = args[3];
                }
            }
        }
        #endregion


    }
}
