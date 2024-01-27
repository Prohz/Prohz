using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class ZipCode
    {
        [Key]
        public int Id { get; set; }
        //public int StateId { get; set; }


        //[ForeignKey("StateId")]
        //public State State { get; set; }


        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
        public string Zipcode { get; set; }
    }
}
