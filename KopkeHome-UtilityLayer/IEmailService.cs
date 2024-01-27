namespace KopkeHome_UtilityLayer
{
    public interface IEmailService
    {
        string GenerateOTPForAuthentication();
        bool SendEmail(string mailto, string subject, string MailBody);
    }
}
