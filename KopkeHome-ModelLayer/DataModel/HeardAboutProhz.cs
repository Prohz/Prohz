using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class HeardAboutProhz
    {
        [Key]
        public int Id { get; set; }
        public string HeardFrom { get; set; }
    }
}
