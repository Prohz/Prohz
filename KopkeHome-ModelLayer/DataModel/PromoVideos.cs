using System.ComponentModel.DataAnnotations;

namespace KopkeHome_ModelLayer.DataModel
{
#nullable disable
    public class PromoVideos
    {
        public int Id { get; set; }
        [StringLength(500)]
        public string OriginalName { get; set; }
        [StringLength(500)]
        public string FilePath { get; set; }
        [StringLength(500)]
        public string FileName { get; set; }
        public bool IsActive { get; set; }

    }
}
