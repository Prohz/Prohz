using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Helper
{
    public class DownloadEventArgs:EventArgs
    {
        public bool FileSaved = false;
        public DownloadEventArgs(bool fileSaved)
        {
            FileSaved = fileSaved;
        }
    }
}
