using System;
using System.IO;
using System.Linq;
using jUtils.Abstractions;
using jUtils.Core.Configuration;
using jUtils.Core.Processing.JMeter;
using jUtils.Models;

namespace jUtils.Core.Processing
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
            var internalProcessor = getProcessor();
            // process all data
            var reprot = internalProcessor.Process(sourceData, _templatesProvider);

            var errors = sourceData.Rows.Where(it => it.PlainData[3] != "200").ToList();
            if (errors.Count > 0 && !string.IsNullOrEmpty(_config.ErrorsOutputPath))
            {
                var errorProcessor = getProcessor();
                var erorsLog = errorProcessor.Process(new CsvData() { Rows = errors }, _templatesProvider);
                writeResult(erorsLog, _config.ErrorsOutputPath);
            }

            writeResult(reprot, _config.OutputPath);
        }

        private ICsvProcessor getProcessor()
        {
            ICsvProcessor internalProcessor;
            internalProcessor = new JMeterCsvProcessor();
            return internalProcessor;
        }

        private void writeResult(string result, string filepath)
        {
            var path = new FileInfo(filepath);
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
