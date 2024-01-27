using System.ComponentModel.DataAnnotations;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class FAQ
    {
        public int Id { get; set; }
        [StringLength(250)]
        public string Question { get; set; }
        [StringLength(2000)]
        public string Answer { get; set; }
        public bool IsActive { get; set; }
    }
}
