using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.Helper
{
    public static class Utilities
    {
        public static bool IsFromStatusChanged { get; set; }
        public static int WorkStatusId {get; set;}
        public delegate Task GetStatus();
        public static GetStatus getStatus;
    }
}
