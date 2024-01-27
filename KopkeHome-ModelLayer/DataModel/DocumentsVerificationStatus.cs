using System.ComponentModel.DataAnnotations;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class DocumentsVerificationStatus
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
    }
}
