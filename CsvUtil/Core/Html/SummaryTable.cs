using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvUtil.Core.Configuration;
using CsvUtil.Models;

namespace CsvUtil.Core.Html
{
    public class SummaryTable
    {
        public string BuildHtmlTable(CsvData data, TemplatesProvider provider)
        {
            var summaryTableTemplate = provider.SummaryTableTemplate;
            var sb = new StringBuilder();
            var i = 0;
            foreach (var row in data.Rows)
            {
                var rowClass = i % 2 == 0 ? "evenRow" : "oddRow";
                sb.AppendLine($"<tr class=\"{rowClass}\">");
                foreach (var s in row.PlainData)
                {
                    sb.Append($"<td>{s}</td>");
                }
                sb.AppendLine("</tr>");
                i++;
            }

            return string.Format(summaryTableTemplate, sb);
        }
    }
}
