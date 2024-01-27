using System.ComponentModel.DataAnnotations;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class Categories
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
    }
}
