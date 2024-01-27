using System.ComponentModel.DataAnnotations;


namespace KopkeHome_ModelLayer
{
#nullable disable
    public class SignIn
    {


        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsRememberme { get; set; }
    }
}

