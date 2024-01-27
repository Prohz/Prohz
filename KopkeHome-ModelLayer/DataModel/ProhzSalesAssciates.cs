using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class ProhzSalesAssciates
    {
        public int Id { get; set; }
        public string SalesPersonName { get; set; }
        public string JoinedMemberName { get; set; }
        public long JoinedMemberMemberId { get; set; }
        public string JoinedMemberEmail { get; set; }
        public bool IsRegistred { get; set; }
        public DateTime JoinedOn { get; set; }

    }
}
