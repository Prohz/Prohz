using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.PaymentAndSubscription;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;

namespace KopkeHome_BusinessLayer.Interface
{
    public interface IAccount
    {

        Task<User> BasicInfo(UserViewModel userDataModel);
        Task<Response> ChangePassword(ChangePasswordModel model);
        Task<User> UserWorkStatus(WorkStatusViewModel model);
        Task<BusinessProfileDataModel> CheckBusinessProfile(User user);
        Task<BusinessProfileSubContractor> CheckBusinessProfileSub(User user);
        Task<bool> CheckEmailExist(string emailId);
        Task<bool> ChechVerificatrionCode(string Otp, string Email);
        Task<List<State>> GetStateList();
        Task<List<User>> GetUserList();
        Task<List<City>> GetCitesList(int Id);
        Task<User> GetUserByEmail(string email);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<Response> ResetPasswordAsync(string token, string email, string NewPassword);
        Task<string> SendOtp(string email, string name);
        Task<bool> CheckContractorsOnZipcode(int zipcode);
        Task<Response> SignIn(SignIn signIn);
        Task<User> UpdateBasicInfo(UpdateBasicInfo userDataModel);
        Task<User> UpdateBasicInfoApp(UpdateBasicInfo model);
        Task<User> UpdateBasicInfoHomeOwner(UpdateBasicInfoHomeOwner model);

        //Task<bool> VerifyOTP(string otp,string Email);

        //Task<bool> SignUpAfterVerifyOTP(User userViewModel, string pass);
        Task<ZipcodesAndCategoriesViewModel> StatesCategoriesAndStatesList(int userid);
        Task<UserMembershipSubscriptions> CheckUserSubscriptions(User user);
        Task<MembershipBenifitsPlan> CheckUserCurrentPlan(User user);
        Task<bool> CheckUserZipcodes(User user);
        Task<bool> CheckUserCategories(User user);
        Task<bool> CheckZipsAndCatsOnUpgradeOrDowngrade(User user);
        Task<User> GetUserByID(int Id);
        Task<bool> DeleteUser(int Id);
        Task<ProhzReferral> GetReferralsById(int v);
        Task<bool> CheckReferralId(string referralId);
    }
}
