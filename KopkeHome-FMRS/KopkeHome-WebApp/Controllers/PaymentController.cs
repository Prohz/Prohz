using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.ViewModels.PaymentModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
using System.Text.RegularExpressions;

namespace KopkeHome_WebApp.Controllers
{
#nullable disable
    public class PaymentController : Controller
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PaymentController(IConfiguration iConfig, ILogger<PaymentController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = iConfig;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            StripeConfiguration.ApiKey = _configuration.GetValue<string>("Stripe:SecretKey");


        }

        public async Task<IActionResult> UpgradeSubscription(string subId, string CusId, string PriceId, string PlanId)
        {
            HttpContext.Session.Remove("PlanId");
            HttpContext.Session.SetString("PlanId", PlanId);
            //var s = await CancelSubscription(subId);
            //var CurrentDomain = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + _httpContextAccessor.HttpContext.Request.PathBase;

            //var options = new Stripe.Checkout.SessionCreateOptions
            //{


            //    LineItems = new List<SessionLineItemOptions>
            //        {
            //            new SessionLineItemOptions
            //            {
            //                Price = PriceId,
            //                Quantity = 1,

            //            },
            //        },
            //    Customer = CusId,
            //    AllowPromotionCodes = false,

            //    Mode = "subscription",

            //    ////Local url
            //    SuccessUrl = CurrentDomain + "/Membership/PaymentSuccess" + "?session_id={CHECKOUT_SESSION_ID}",
            //    CancelUrl = CurrentDomain + "/HOME/Paymentfailed",

            //};
            //var service2 = new SessionService();

            //Session session = await service2.CreateAsync(options);

            UpgradeSubscriptionRequestModel model = new UpgradeSubscriptionRequestModel();
            model.StripesubId = subId;
            model.StripePriceId = PriceId;
            model.PlanId = PlanId;
            model.StripeCusId = CusId;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");
                var httpResponse = await client.PostAsJsonAsync<UpgradeSubscriptionRequestModel>("UpgradeSubscription", model);
                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var paymentResponse = JsonConvert.DeserializeObject<Response>(content);
                    string Url = JsonConvert.SerializeObject(paymentResponse.Data);
                    Url = Regex.Replace(Url, "^\"|\"$", "");
                    return Json(Url);

                }
                else
                {
                    return Json(content);
                }
            }
            // return Json(session.Url);


        }


        public async Task<IActionResult> CancelSubscription(string subId)
        {
            try
            {
                // UserMembershipSubscriptions model=new UserMembershipSubscriptions();
                //var service = new SubscriptionService();
                //Subscription subscription = await service.GetAsync(subId);

                //var items = new List<SubscriptionItemOptions> {
                //        new SubscriptionItemOptions {
                //            Id = subscription.Items.Data[0].Id,

                //        },
                //                };

                //var options = new SubscriptionUpdateOptions
                //{
                //    CancelAtPeriodEnd = true,
                //    // ProrationBehavior = "always_invoice",
                //    Items = items,
                //};
                //subscription.CancelAtPeriodEnd = true;
                //subscription = await service.UpdateAsync(subId, options);

                //model.StripeSubscriptionId = subId;
                //model.StripeStatus = "Cancelled";
                //model.CancelledOn=DateTime.Now;

                //var SSoptions = new SubscriptionScheduleListOptions
                //{
                //    Limit = 10,
                //    Customer = subscription.CustomerId
                //};
                //var service2 = new SubscriptionScheduleService();
                //StripeList<SubscriptionSchedule> subscriptionSchedules = service2.List(SSoptions);
                //if (subscriptionSchedules.Data.Any())
                //{
                //    //foreach (var item in subscriptionSchedules.Data)
                //    //{
                //    //    if (item.Status != "canceled")
                //    //    {
                //    //        var SubSchdservice = new SubscriptionScheduleService();
                //    //        SubSchdservice.Cancel(
                //    //          item.Id);
                //    //    }
                //    //}
                //}
                using (var request = new HttpClient())
                {
                    request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");

                    var SendsubId = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("subId", subId),

                        });
                    var response = await request.PostAsync("CancelSubscription", SendsubId);
                    var result = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        return Json(1);
                    }
                    else
                    {
                        return Json(0);
                    }

                }
                //using (var client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");
                //    var httpResponse = await client.PostAsJsonAsync<UserMembershipSubscriptions>("UpdatePaymentTransactionInfo", model);
                //    var content = await httpResponse.Content.ReadAsStringAsync();
                //    if (httpResponse.IsSuccessStatusCode)
                //    {
                //        return Json(1);
                //    }
                //    else
                //    {
                //        return Json(0);
                //    }
                //}


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }


        public async Task<IActionResult> DowngradeSubscription(string subId, string CusId, string PriceID, string PlanId)
        {

            try
            {
                //STEP-1 CANCELS SUBSCRIPTION CURRENT.
                HttpContext.Session.Remove("PlanId");
                HttpContext.Session.SetString("PlanId", PlanId);

                // var SUB = new SubscriptionService();
                //var res= SUB.Get(subId);
                // var endsAt = res.CurrentPeriodEnd;
                // var startsubAt = DateTimeOffset.FromUnixTimeSeconds(Startdate).UtcDateTime;
                //var options = new SubscriptionScheduleCreateOptions
                //{
                //    Customer = CusId,
                //    StartDate = endsAt,
                //    EndBehavior = "release",
                //    Phases = new List<SubscriptionSchedulePhaseOptions>
                //    {
                //      new SubscriptionSchedulePhaseOptions
                //      {
                //        Items = new List<SubscriptionSchedulePhaseItemOptions>
                //        {
                //          new SubscriptionSchedulePhaseItemOptions
                //          {
                //            Price = PriceID,
                //            Quantity = 1,
                //          },
                //        },
                //        //Iterations = 12,
                //      },
                //    },
                //};
                //var service = new SubscriptionScheduleService();
                //service.Create(options);

                //payment link generation.
                //var CurrentDomain = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + _httpContextAccessor.HttpContext.Request.PathBase;

                //var options2 = new Stripe.Checkout.SessionCreateOptions
                //{


                //    LineItems = new List<SessionLineItemOptions>
                //        {
                //            new SessionLineItemOptions
                //            {
                //                Price = PriceID,
                //                Quantity = 1,

                //            },
                //        },
                //    Customer = CusId,
                //    AllowPromotionCodes = false,

                //    Mode = "subscription",

                //    ////Local url
                //    SuccessUrl = CurrentDomain + "/Membership/PaymentSuccess" + "?session_id={CHECKOUT_SESSION_ID}",
                //    CancelUrl = CurrentDomain + "/HOME/Paymentfailed",

                //};
                //var service2 = new SessionService();

                //Session session = await service2.CreateAsync(options2);
                //var cancelSub = await CancelSubscription(subId);
                DowngradeSubscriptionRequestModel model = new DowngradeSubscriptionRequestModel();
                model.StripesubId = subId;
                model.StripePriceId = PriceID;
                model.PlanId = PlanId;
                model.StripeCusId = CusId;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");
                    var httpResponse = await client.PostAsJsonAsync<DowngradeSubscriptionRequestModel>("DowngradeSubscription", model);
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var paymentResponse = JsonConvert.DeserializeObject<Response>(content);
                        string Url = JsonConvert.SerializeObject(paymentResponse.Data);
                        Url = Regex.Replace(Url, "^\"|\"$", "");
                        return Json(Url);

                    }
                    else
                    {
                        return Json(content);
                    }
                }
                //return Json(session.Url);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }

        }

    }
}
