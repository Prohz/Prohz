using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class WorkGallery
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
        [StringLength(250)]
        public string ImageUrl { get; set; }



    }
}
