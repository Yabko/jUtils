namespace jUtils.Core.Configuration
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

        public string AllResultsTableTemplate
        {
            get
            {
                // double check lock
                if (_allResultsTableTemplate != null) return _allResultsTableTemplate;
                lock (_lockObj)
                {
                    if (_allResultsTableTemplate != null) return _allResultsTableTemplate;
                    _allResultsTableTemplate = getTemplate(_config.AllResultsTableTemplatePath);
                    return _allResultsTableTemplate;
                }
            }
        }
        private volatile string _allResultsTableTemplate;

        public string SummaryTableTemplate
        {
            get
            {
                // double check lock
                if (_summaryTemplate != null) return _summaryTemplate;
                lock (_lockObj)
                {
                    if (_summaryTemplate != null) return _summaryTemplate;
                    _summaryTemplate = getTemplate(_config.SummaryTableTemplatePath);
                    return _summaryTemplate;
                }
            }
        }
        private volatile string _summaryTemplate;

        public string AnalysisTemplate
        {
            get
            {
                // double check lock
                if (_analysisTemplate != null) return _analysisTemplate;
                lock (_lockObj)
                {
                    if (_analysisTemplate != null) return _analysisTemplate;
                    _analysisTemplate = getTemplate(_config.AllResultsTableTemplatePath);
                    return _analysisTemplate;
                }
            }
        }
        private volatile string _analysisTemplate;

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
