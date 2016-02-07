using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jUtils.Abstractions;
using jUtils.Core.Configuration;
using jUtils.Core.Html;
using jUtils.Models;

namespace jUtils.Core.Processing.Common
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
