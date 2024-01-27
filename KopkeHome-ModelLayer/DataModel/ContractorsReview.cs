using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class ContractorsReview
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ContractorId { get; set; }
        [JsonIgnore]
        [ForeignKey("UserId")]
        public User User { get; set; }

        public bool IsLiked { get; set; }

    }

}
