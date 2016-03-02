using System;
using System.IO;
using System.Linq;
using System.Text;
using jUtils.Core.Configuration;
using jUtils.Core.Html;
using jUtils.Models;

namespace jUtils.Core.Processing
{
    class CsvProcessor
    {
        #region Fields

        private Config _config;
        private TemplatesProvider _templatesProvider;

        #endregion

        #region Constructor

        public CsvProcessor(Config configuration)
        {
            _config = configuration;
            _templatesProvider = new TemplatesProvider(_config);
        }

        #endregion

        public void CreateHtmlResult(JMeterData sourceData)
        {
            // here we have all methods in one big table (ALL RESULTS)           
            var reprot = process(sourceData, _templatesProvider);
            writeResult(reprot, _config.OutputPath);

            if (!string.IsNullOrEmpty(_config.AnalysisOutputPath))
            {
                var analysisHtml = processAnalysis(sourceData);
                writeResult(analysisHtml, _config.AnalysisOutputPath);
            }

            // Get just those where are erros
            var errors = sourceData.Rows.Where(it => it.ResponceCode != "200").ToList();
            if (errors.Count > 0 && !string.IsNullOrEmpty(_config.ErrorsOutputPath))
            {
                var erorsLog = process(new JMeterData(errors , ""), _templatesProvider);
                writeResult(erorsLog, _config.ErrorsOutputPath);
            }


        }

        private void writeResult(string result, string filepath)
        {
            var path = new FileInfo(filepath);
            using (var swrite = new System.IO.StreamWriter(path.FullName))
            {
                swrite.Write(result);
            }

            // save related css
            try
            {
                File.Copy(_config.CssFilePath, $@"{path.Directory.FullName}\{_config.CssName}", true);
            }
            catch (Exception)
            {
                // ignore at this point

            }
        }


        private string process(JMeterData allData, TemplatesProvider provider)
        {
            // let split them by label
            var jDatas = from row in allData.Rows
                         group row by row.Label into grouped
                         select new JMeterData(grouped.ToList(), grouped.Key.Trim());

            var sb = new StringBuilder();

            foreach (var methodData in jDatas)
            {
                var summary = methodData.PrepareSummary();
                var summaryTable = new HtmlSummaryBuilder().Build(summary, methodData.MethodName, provider);
                sb.Append(summaryTable);
            }

            var allResultstable = new HtmlTableBuilder().Build(allData, provider);

            var formatted = string.Format(provider.HtmlPageTemplate, sb.ToString(), allResultstable);
            return formatted;
        }

        private string processAnalysis(JMeterData allData)
        {
            // let split them by label
            var jDatas = from row in allData.Rows
                         group row by row.Label into grouped
                         select new JMeterData(grouped.ToList(), grouped.Key.Trim());

            var sb = new StringBuilder();
            int i = 0;
            var tableBuilder = new HtmlTableBuilder();

            foreach (var methodData in jDatas)
            {
                tableBuilder.BuildRow(methodData.AnaliseResponseTimes(), sb, i);
                i++;                
            }

            var formatted = string.Format(_templatesProvider.AnalysisTableTemplate, sb.ToString());
            return formatted;
        }
    }
}
