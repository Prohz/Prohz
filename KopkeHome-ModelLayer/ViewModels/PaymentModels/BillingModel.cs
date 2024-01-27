using KopkeHome_ModelLayer.DataModel;
using System.ComponentModel.DataAnnotations;

namespace KopkeHome_ModelLayer.ViewModels.PaymentModels
{
#nullable disable
    public class BillingModel
    {

        public User User { get; set; }

        [Required(ErrorMessage = "Enter valid card number.")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Expiry Month is Required.")]
        public string ExpiryMonth { get; set; }

        [Required(ErrorMessage = "Expiry Year is Required.")]
        public string ExpiryYear { get; set; }

        [Required(ErrorMessage = "CVV number is Required.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?!00000)[0-9]{3,4}$", ErrorMessage = "Please Enter Valid CVV")]
        public string CVV { get; set; }

        //[Required]

        public bool AutoRenewal { get; set; }

        public long? PriceInCent { get; set; }
        public string PriceInDollar { get; set; }
        public string Currency { get; set; }
        public string ProductId { get; set; }

        public string Token { get; set; }
        public string Interval { get; set; }

        //for the billing details

        public string BillingName { get; set; }


        public string BillingPhoneNumber { get; set; }


        //[Remote("EmailExists", "Account", HttpMethod = "POST", ErrorMessage = "Email is already in use for another User.")]
        public string BillingEmail { get; set; }

        [Required(ErrorMessage = "Address is Required.")]
        public string BillingAddress { get; set; }

        public string BillingOther { get; set; }

        public string BillingCity { get; set; }


        public string BillingPostalCode { get; set; }

        public int UserId { get; set; }
        public int StateId { get; set; }
    }
}
