namespace CsvUtil.Core.Configuration
{
    public class TemplatesProvider
    {
        private readonly Config _config;
        private readonly object _lockObj = new object();

        public string HtmlPageTemplate
        {
            get
            {
                // double check lock
                if (_htmlPageTemplate != null) return _htmlPageTemplate;
                lock (_lockObj)
                {
                    if (_htmlPageTemplate != null) return _htmlPageTemplate;
                    _htmlPageTemplate = getTemplate(_config.HtmlPageTemplatePath);
                    return _htmlPageTemplate;
                }
            }
        }
        private volatile string _htmlPageTemplate;

        public string SummaryTableTemplate
        {
            get
            {
                // double check lock
                if (_summaryTableTemplate != null) return _summaryTableTemplate;
                lock (_lockObj)
                {
                    if (_summaryTableTemplate != null) return _summaryTableTemplate;
                    _summaryTableTemplate = getTemplate(_config.SummaryTableTemplatePath);
                    return _summaryTableTemplate;
                }
            }
        }
        private volatile string _summaryTableTemplate;

        public TemplatesProvider(Config config)
        {
            _config = config;
        }

        private string getTemplate(string path)
        {
            var stream = new System.IO.StreamReader(path);
            var template = stream.ReadToEnd();
            stream.Close();
            return template;
        }
    }
}
