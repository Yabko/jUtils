using System;
using System.IO;
using System.Linq;
using System.Text;
using jUtils.Core.Configuration;
using jUtils.Core.Html;
using jUtils.Models;
using System.Collections.Generic;

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

        // Prepare analysis table
        private string processAnalysis(JMeterData allData)
        {

            var tableBuilder = new HtmlTableBuilder();

            var sbTotal = new StringBuilder();
            var total = allData.AnaliseResponseTimes();
            // remove Method name because it is not used
            total.RemoveAt(0);
            tableBuilder.BuildRow(total, sbTotal, 0);


            // let split all results by method
            var jDatas = from row in allData.Rows
                         group row by row.Label into grouped
                         select new JMeterData(grouped.ToList(), grouped.Key.Trim());

            int i = 0;
            var sb = new StringBuilder();
            foreach (var methodData in jDatas)
            {
                // get analyzed data per each method
                var analyzed = methodData.AnaliseResponseTimes();
                // add to stringBuilder sb, to get this data as row <tr> </tr>. Use i to determine odd or even row.
                tableBuilder.BuildRow(analyzed, sb, i++);
            }
           
            var formatted = string.Format(_templatesProvider.AnalysisTableTemplate, sb.ToString(), getConfData(), sbTotal.ToString());
            return formatted;
        }


        public string getConfData()
        {
            List<string> configData = new List<string>();
            configData.Add(JsonConfig.users.ToString());
            configData.Add(JsonConfig.loop.ToString());
            configData.Add(JsonConfig.rampup.ToString());
            configData.Add(JsonConfig.coolTime.ToString());
            configData.Add(JsonConfig.okTime.ToString());

            String confData = String.Format("<b> Users:{0} <br> Loop:{1} <br> Rampup:{2} <br> CoolTime:<{3}ms <br> OkTime:<{4}ms </b>", configData[0], configData[1], configData[2], configData[3], configData[4]);
            return confData;
        }
    }
    
}
