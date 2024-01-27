using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KopkeHome_ModelLayer.DataModel
{
    public class Role : IdentityRole<int>
    {
        [Key]
        public override int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
