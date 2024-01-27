using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class City
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string CityName { get; set; }
        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public State State { get; set; }
    }
}
