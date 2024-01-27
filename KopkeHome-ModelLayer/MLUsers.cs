namespace KopkeHome_ModelLayer
{
#nullable disable
    public class MLUsers
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long MobileNo { get; set; }
        public long? OTP { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public int LoginType { get; set; }
        public int loginStatus { get; set; }
        public int UserStatus { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }
        public bool IsEmailVerified { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string ResetPassword { get; set; }

    }
}
