using CsvUtil.Abstractions;
using CsvUtil.Core.Configuration;
using CsvUtil.Core.Html;
using CsvUtil.Models;

namespace CsvUtil.Core.Processing.JMeter
{
    public class JMeterCsvProcessor : ICsvProcessor
    {
        public string Process(CsvData data, TemplatesProvider provider)
        {
            var jData = new JMeterData(data);
            var allResultstable = new HtmlTableBuilder().Build(data, provider);
            var summaryTable = new HtmlSummaryBuilder().Build(jData.Summary, provider);
            var formatted = string.Format(provider.HtmlPageTemplate, summaryTable, allResultstable);
            return formatted;
        }
    }
}
