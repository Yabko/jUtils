using System;
using System.IO;
using CsvUtil.Abstractions;
using CsvUtil.Core.Configuration;
using CsvUtil.Core.Html;
using CsvUtil.Core.Processing.Common;
using CsvUtil.Core.Processing.JMeter;
using CsvUtil.Models;

namespace CsvUtil.Core.Processing
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

        public void CreateHtmlResult(CsvData sourceData)
        {
            ICsvProcessor internalProcessor;
            if (_config.IsJMeterMode)
            {
                internalProcessor = new JMeterCsvProcessor();
            }
            else
            {
                internalProcessor = new CommonCsvProcessor();   
            }
            var reprot = internalProcessor.Process(sourceData, _templatesProvider);
       
            writeResult(reprot);
        }

        private void writeResult(string result)
        {
            var path = new FileInfo(_config.OutputPath);
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
    }
}
