using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_ModelLayer.DataModel
{
    public class ProhzReferral
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [StringLength(250)]
        public string? SalesPersonName { get; set; }
        public long MemberId { get; set; }
        public bool IsRegistrationComplete { get; set; }
    }
}
