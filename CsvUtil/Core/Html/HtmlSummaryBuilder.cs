using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvUtil.Core.Configuration;
using CsvUtil.Models;

namespace CsvUtil.Core.Html
{
    public class HtmlSummaryBuilder
    {
        public string Build(Dictionary<string, object> summary, string methodName, TemplatesProvider provider)
        {
            var allResultsTableTemplate = provider.SummaryTableTemplate;
            var sb = new StringBuilder();
            var i = 0;
            foreach (var row in summary)
            {
                var rowClass = i % 2 == 0 ? "evenRow" : "oddRow";
                sb.AppendLine($"<tr class=\"{rowClass}\"><td>{row.Key}</td><td>{row.Value}</td></tr>");
                i++;
            }

            return string.Format(allResultsTableTemplate, methodName, sb);
        }

    }
}
