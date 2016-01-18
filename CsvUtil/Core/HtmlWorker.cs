using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvUtil.Core
{
    class HtmlWorker
    {
        private Config _config;

        public HtmlWorker(Config configuration)
        {
            _config = configuration;
        }

        public void Write(string sourceData)
        {
            var templatePath = _config.TemplatePath;
            var stream = new System.IO.StreamReader(templatePath);
            var template = stream.ReadToEnd();
            stream.Close();

            var path = _config.OutputPath;
            var swrite = new System.IO.StreamWriter(path);

            swrite.Write(string.Format(template, sourceData));
            swrite.Close();
        }
    }
}
