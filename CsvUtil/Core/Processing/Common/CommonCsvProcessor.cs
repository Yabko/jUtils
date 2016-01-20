using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvUtil.Abstractions;
using CsvUtil.Core.Configuration;
using CsvUtil.Core.Html;
using CsvUtil.Models;

namespace CsvUtil.Core.Processing.Common
{
    class CommonCsvProcessor:ICsvProcessor
    {
        public string Process(CsvData data, TemplatesProvider provider)
        {
            var table = new HtmlTableBuilder();
            var tableHtml = table.Build(data, provider);
            var formatted = string.Format(provider.HtmlPageTemplate, tableHtml);
            return formatted;
        }
    }
}
