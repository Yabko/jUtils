using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvUtil.Models
{
    public class CsvData
    {
        public List<CsvRow> Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }
        private List<CsvRow> _rows = new List<CsvRow>();
    }

}
