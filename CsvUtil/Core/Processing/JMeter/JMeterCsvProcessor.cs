using System.Linq;
using System.Text;
using jUtils.Abstractions;
using jUtils.Core.Configuration;
using jUtils.Core.Html;
using jUtils.Models;

namespace jUtils.Core.Processing.JMeter
{
    public class JMeterCsvProcessor : ICsvProcessor
    {
        public string Process(CsvData data, TemplatesProvider provider)
        {          
            var jDatas = from row in data.Rows
                         group row by row.PlainData[2] into grouped
                         select new JMeterData(grouped.ToList(), grouped.Key.Trim());

            var sb = new StringBuilder();

            foreach (var methodData in jDatas)
            {
                var summaryTable = new HtmlSummaryBuilder().Build(methodData.Summary, methodData.MethodName, provider);
                sb.Append(summaryTable);
            }


            var allResultstable = new HtmlTableBuilder().Build(data, provider);

            var formatted = string.Format(provider.HtmlPageTemplate, sb.ToString(), allResultstable);
            return formatted;
        }
    }
}
