using jUtils.Core.Configuration;
using jUtils.Models;

namespace jUtils.Abstractions
{
    public interface ICsvProcessor
    {
        string Process(CsvData data, TemplatesProvider provider);
    }
}