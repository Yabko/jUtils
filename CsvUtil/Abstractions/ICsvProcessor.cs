using CsvUtil.Core.Configuration;
using CsvUtil.Models;

namespace CsvUtil.Abstractions
{
    public interface ICsvProcessor
    {
        string Process(CsvData data, TemplatesProvider provider);
    }
}