using KopkeHome_ModelLayer.DataModel.PaymentAndSubscription;
using KopkeHome_ModelLayer.ViewModels;

namespace KopkeHome_BusinessLayer.Interface
{
    public interface IPaymentService
    {
        Task<UserMembershipSubscriptions> AddPaymentTransactionInfo(UserMembershipSubscriptions model);
        Task<UserMembershipSubscriptions> UpdatePaymentTransactionInfo(UserMembershipSubscriptions model);
        Task<UserMembershipSubscriptions> GetSubscriptionsInfoByUserId(int UserId);
        Task<MembershipPlanViewmodelApp> GetSubscriptionsInfoByUserIdApp(int UserId);
        Task<UserMembershipSubscriptions> GetSubscriptionByStripCustomerId(string StripeCustomerId);
    }
}
