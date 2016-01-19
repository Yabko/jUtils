using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CsvUtil.Core.Configuration;
using CsvUtil.Core.Html;
using CsvUtil.Models;

namespace CsvUtil.Core
{
    class CsvProcessor
    {
        private Config _config;
        private TemplatesProvider _templatesProvider;

        public CsvProcessor(Config configuration)
        {
            _config = configuration;
            _templatesProvider = new TemplatesProvider(_config);
        }

        public void CreateHtmlResult(CsvData sourceData)
        {
            var path = new System.IO.FileInfo(_config.OutputPath);

            var table = new SummaryTable();
            var tableHtml = table.BuildHtmlTable(sourceData, _templatesProvider);

            using (var swrite = new System.IO.StreamWriter(path.FullName))
            {
                swrite.Write(_templatesProvider.HtmlPageTemplate, tableHtml);
            }

            try
            {
                File.Copy(_config.CssFilePath, $@"{path.Directory.FullName}\{_config.CssName}", true);
            }
            catch (Exception)
            {
                // ignore at this point

            }
        }
    }
}
