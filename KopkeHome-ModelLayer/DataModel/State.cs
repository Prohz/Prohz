using System.ComponentModel.DataAnnotations;


namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class State
    {
        [Key]
        public int StateId { get; set; }
        [StringLength(50)]
        public string StateName { get; set; }
        public int CountryId { get; set; }
        [StringLength(2)]
        public string USAStateCode { get; set; }

    }
}
