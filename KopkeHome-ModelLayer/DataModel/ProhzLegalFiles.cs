using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class ProhzLegalFiles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public int FileType { get; set; }
    }
}
