using System.ComponentModel.DataAnnotations;

namespace KopkeHome_ModelLayer.DataModel
{
    public class VerifyOTP
    {
        [Key]
        public int OtpId { get; set; }
        public string? Email { get; set; }

        [Required(ErrorMessage = "Verification code is required")]
        public string? VerificationCode { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
}
