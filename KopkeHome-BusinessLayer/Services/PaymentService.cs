using KopkeHome_BusinessLayer.Interface;
using KopkeHome_DataAccessLayer;
using KopkeHome_DataAccessLayer.Repository;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.PaymentAndSubscription;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_UtilityLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using XAct;

namespace KopkeHome_BusinessLayer.Services
{
#nullable disable
    public class PaymentService : IPaymentService
    {

        private readonly IRepository<UserMembershipSubscriptions> _iRepository;
        private readonly IEmailService _email;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<PaymentService> _logger;
        private readonly IConfiguration _configuration;
        public PaymentService(IRepository<UserMembershipSubscriptions> SubscriptionsRepository, IConfiguration configuration, ILogger<PaymentService> logger, ApplicationDbContext dbContext, IEmailService email)
        {
            _dbContext = dbContext;
            _iRepository = SubscriptionsRepository;
            _email = email;
            _logger = logger;
            _configuration = configuration;
        }


        /// <summary>
        /// Add Payment Transaction Info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserMembershipSubscriptions> AddPaymentTransactionInfo(UserMembershipSubscriptions model)
        {
            try
            {

                // Find your Account SID and Auth Token at twilio.com/console
                // and set the environment variables. See http://twil.io/secure

                var userid = _dbContext.User.Where(x => x.Email == model.Email).FirstOrDefault();
                var CheckExistingSubscriptions = _dbContext.UserMembershipSubscriptions.Where(x => x.Email == model.Email).FirstOrDefault();
                if (CheckExistingSubscriptions == null)
                {


                    model.CreatedOn = DateTime.Now;
                    model.IsActive = true;
                    model.UserId = userid.Id;

                    var respo = await _iRepository.Add(model);
                    if (respo != null)
                    {
                        var accountSid = _configuration.GetSection("Twilio")["accountSid"];
                        string authToken = _configuration.GetSection("Twilio")["authToken"];

                        TwilioClient.Init(accountSid, authToken);

                        var message = MessageResource.Create(
                            body: "New " + Constant.GetNames(userid.RoleId) + " joined Prohz.Member id is " + userid.UniqueMemberId,




                            from: new Twilio.Types.PhoneNumber(_configuration.GetSection("Twilio")["TwilioFromNumber"]),

                            to: new Twilio.Types.PhoneNumber(_configuration.GetSection("Twilio")["TwilioToNumber"])
                        );

                        var Referal = _dbContext.ProhzReferral.Where(X => X.UserId == userid.Id).FirstOrDefault();
                        if (Referal != null)
                        {
                            var res = await UpdateReferralClaims(Referal.MemberId);
                            if (res)
                            {
                                Referal.IsRegistrationComplete = true;
                                await _dbContext.SaveChangesAsync();
                            }
                        }
                        var ReferalSales = _dbContext.ProhzSalesAssciates.Where(X => X.JoinedMemberEmail == userid.Email).FirstOrDefault();
                        if (ReferalSales != null)
                        {

                            ReferalSales.IsRegistred = true;
                            ReferalSales.JoinedOn = DateTime.Now;
                            await _dbContext.SaveChangesAsync();
                        }


                    }
                    return respo;

                }
                else
                {

                    CheckExistingSubscriptions.CreatedOn = DateTime.Now;

                    CheckExistingSubscriptions.StripeStatus = model.StripeStatus;
                    CheckExistingSubscriptions.PaymentStatus = model.PaymentStatus;
                    CheckExistingSubscriptions.StripeCustomerID = model.StripeCustomerID;

                    CheckExistingSubscriptions.PeriodStartDate = model.PeriodStartDate;
                    CheckExistingSubscriptions.PeriodEndDate = model.PeriodEndDate;

                    CheckExistingSubscriptions.InvoiceUrl = model.InvoiceUrl;
                    CheckExistingSubscriptions.InvoiceNumber = model.InvoiceNumber;


                    CheckExistingSubscriptions.StripePriceId = model.StripePriceId;
                    CheckExistingSubscriptions.StripeSubscriptionId = model.StripeSubscriptionId;
                    CheckExistingSubscriptions.IsActive = true;
                    CheckExistingSubscriptions.UserId = userid.Id;
                    CheckExistingSubscriptions.Email = model.Email;
                    CheckExistingSubscriptions.PlanId = model.PlanId;

                    _dbContext.UserMembershipSubscriptions.Update(CheckExistingSubscriptions);
                    await _dbContext.SaveChangesAsync();
                    var removezip = _dbContext.CustomZipcodesRequest.Where(x => x.UserId == userid.Id).FirstOrDefault();
                    if (removezip != null && model.PlanId != 13)
                    {
                        _dbContext.CustomZipcodesRequest.Remove(removezip);
                        await _dbContext.SaveChangesAsync();
                    }


                    return _dbContext.UserMembershipSubscriptions.Where(x => x.StripeSubscriptionId == model.StripeSubscriptionId).FirstOrDefault();

                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }


        /// <summary>
        /// Update Payment Transaction Info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserMembershipSubscriptions> UpdatePaymentTransactionInfo(UserMembershipSubscriptions model)
        {
            try
            {

                UserMembershipSubscriptions option = new UserMembershipSubscriptions();
                //model.CreatedOn = DateTime.Now;
                //model.IsActive = true;
                var userid = _dbContext.UserMembershipSubscriptions.Where(x => x.StripeSubscriptionId == model.StripeSubscriptionId).FirstOrDefault();
                //model.UserId = userid.Id;
                //var plan=
                if (userid != null)
                {
                    userid.StripeStatus = model.StripeStatus;
                    userid.CancelledOn = model.CancelledOn;

                    _dbContext.UserMembershipSubscriptions.Update(userid);

                    await _dbContext.SaveChangesAsync();

                    option = _dbContext.UserMembershipSubscriptions.Where(x => x.StripeSubscriptionId == model.StripeSubscriptionId).FirstOrDefault();
                }


                return option;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }

        /// <summary>
        /// Get Subscriptions InfoBy UserId
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>

        public async Task<UserMembershipSubscriptions> GetSubscriptionsInfoByUserId(int UserId)
        {
            var result = await _iRepository.FindSingleByCondition(x => x.UserId == UserId);


            return result;
        }

        /// <summary>
        /// Get Subscriptions Info By UserId App
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<MembershipPlanViewmodelApp> GetSubscriptionsInfoByUserIdApp(int UserId)
        {
            MembershipPlanViewmodelApp data = new();
            var customData = await _dbContext.CustomZipcodesRequest.Where(x => x.UserId == UserId).FirstOrDefaultAsync();
            if (customData == null) { 
            var planData = await _dbContext.UserMembershipSubscriptions.Join(_dbContext.MembershipBenefitsPlan, ms => ms.PlanId, mb => mb.Id, (ms, mb) => new { mb, ms })
                                  .Where(x => x.ms.UserId == UserId)
                                  .Select(x => new MembershipPlanViewmodelApp
                                  {
                                      PlanId=x.ms.PlanId,
                                      UserId = x.ms.UserId,
                                      Title = x.mb.Title,
                                      ZipCodes = x.mb.ZipCodes,
                                      Categories = x.mb.Categories,
                                      PhoneApp = x.mb.PhoneApp,
                                      Website = x.mb.Website,
                                      PeriodStartDate = x.ms.PeriodStartDate,
                                      PeriodEndDate = x.ms.PeriodEndDate,
                                      StripePriceId = x.ms.StripePriceId,
                                      StripePriceIdAnu = x.mb.AnnuallyStripePriceId,
                                      StripePriceIdmon=x.mb.MonthlyStripePriceId,
                                      PaymentStatus=x.ms.PaymentStatus,
                                      InvoiceNumber=x.ms.InvoiceNumber,
                                      InvoiceUrl=x.ms.InvoiceUrl,
                                      UpgradedOn=x.ms.UpgradedOn,
                                      DowngradedOn=x.ms.DowngradedOn,
                                      CancelledOn=x.ms.CancelledOn,
                                      IsActive=x.ms.IsActive

                                  }).FirstOrDefaultAsync();
                if (planData != null)
                {
                    data = planData;
                }
            
         
            if (data != null)
            {
                var pricedata = await _dbContext.MembershipBenefitsPlan.Where(x=>x.Id==data.PlanId).FirstOrDefaultAsync();  
                if (data.StripePriceId == data.StripePriceIdAnu)
                {
                    data.Days = 365;
                        if(pricedata != null)
                        {
                            data.Price = pricedata.PricePerYear;
                        }
                        else
                        {
                            data.Price = 0;
                        }
                }
                else if (data.StripePriceId == data.StripePriceIdmon)
                {
                    data.Days = 90;
                    data.Price = pricedata.PricePerMonth;
                }
            }

            }
            else
            {
                var planData = await _dbContext.UserMembershipSubscriptions.Join(_dbContext.MembershipBenefitsPlan, ms => ms.PlanId, mb => mb.Id, (ms, mb) => new { mb, ms })
                                 .Where(x => x.ms.UserId == UserId)
                                 .Select(x => new MembershipPlanViewmodelApp
                                 {
                                     PlanId = x.ms.PlanId,
                                     UserId = x.ms.UserId,
                                     Title = x.mb.Title,
                                     ZipCodes = customData.NumberOfZipcodes.ToString(),
                                     Categories = customData.NumberOfCategories.ToString(),
                                     PhoneApp = x.mb.PhoneApp,
                                     Website = x.mb.Website,
                                     PeriodStartDate = x.ms.PeriodStartDate,
                                     PeriodEndDate = x.ms.PeriodEndDate,
                                     StripePriceId = x.ms.StripePriceId,
                                     StripePriceIdAnu = x.mb.AnnuallyStripePriceId,
                                     StripePriceIdmon = x.mb.MonthlyStripePriceId,
                                     PaymentStatus = x.ms.PaymentStatus,
                                     InvoiceNumber = x.ms.InvoiceNumber,
                                     InvoiceUrl = x.ms.InvoiceUrl,
                                     UpgradedOn = x.ms.UpgradedOn,
                                     DowngradedOn = x.ms.DowngradedOn,
                                     CancelledOn = x.ms.CancelledOn,
                                     IsActive = x.ms.IsActive

                                 }).FirstOrDefaultAsync();
                
                if(planData != null)
                {
                    data = planData;
                }
                if (customData.IsYearly)
                {
                    data.Days = 365;
                    data.Price = customData.PriceYearly;

                }
               else if (!customData.IsYearly)
                {
                    data.Days = 90;
                    data.Price = customData.PriceMonthly;

                }
            }

            return data;
        }

        /// <summary>
        /// Get Subscription By Strip CustomerId
        /// </summary>
        /// <param name="StripeCustomerId"></param>
        /// <returns></returns>
        public async Task<UserMembershipSubscriptions> GetSubscriptionByStripCustomerId(string StripeCustomerId)
        {
            var result = await _iRepository.FindSingleByCondition(x => x.StripeCustomerID.Contains(StripeCustomerId));


            return result;
        }


        /// <summary>
        /// Update Referral Claims
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public async Task<bool> UpdateReferralClaims(long MemberId)
        {
            bool flag = false;
            var ReferdBy = _dbContext.User.Where(x => x.UniqueMemberId == MemberId).FirstOrDefault();
            if (ReferdBy != null)
            {
                var SubscriptionExtension = _dbContext.UserMembershipSubscriptions.Where(x => x.UserId == ReferdBy.Id).FirstOrDefault();
                if (SubscriptionExtension != null)
                {
                    var dt = SubscriptionExtension.PeriodEndDate;
                    var Nextdt = dt.AddDays(60);

                    SubscriptionExtension.Extensions = SubscriptionExtension.Extensions + 60;
                    SubscriptionExtension.PeriodEndDate = Nextdt;
                    _dbContext.UserMembershipSubscriptions.Update(SubscriptionExtension);
                    await _dbContext.SaveChangesAsync();
                    var InfoUser = await UpdateUserReferralClaims(ReferdBy);
                    flag = true;
                }
            }

            return flag;


        }

        /// <summary>
        /// Update User ReferralClaims
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUserReferralClaims(User user)
        {
            bool flag = false;
            string Subject = "Your Prohz membership is extended by 60 days.";
            string Body = "Congratulations, Your Prohz membership is extended by 60 days.";
            var IsSent = _email.SendEmail(user.Email, Subject, Body);
            if (IsSent)
            {
                flag = true;
            }
            return flag;
        }
    }
}
