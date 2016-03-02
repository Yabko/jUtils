using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jUtils.Core.Configuration;
using jUtils.Models;

namespace jUtils.Core.Html
{
    public class HtmlTableBuilder
    {
        public string Build(JMeterData data, TemplatesProvider provider)
        {
            var allResultsTableTemplate = provider.AllResultsTableTemplate;
            var sb = new StringBuilder();
            var i = 0;
            foreach (var row in data.Rows)
            {
                BuildRow(row.PlainData, sb, i);
                i++;
            }

            return string.Format(allResultsTableTemplate, sb);
        }

        public void BuildRow(IEnumerable<string> plainData, StringBuilder sb, int i)
        {
            var rowClass = i % 2 == 0 ? "evenRow" : "oddRow";
            sb.AppendLine($"<tr class=\"{rowClass}\">");
            foreach (var s in plainData)
            {
                sb.Append($"<td>{s}</td>");
            }
            sb.AppendLine("</tr>");            
        }
    }
}
