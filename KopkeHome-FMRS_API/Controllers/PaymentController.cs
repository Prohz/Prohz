
// using KopkeHome_FMRS_API.Properties;
// using KopkeHome_ModelLayer;
// using KopkeHome_ModelLayer.DataModel;
// using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
// using KopkeHome_ModelLayer.DataModel.PaymentAndSubscription;
// using KopkeHome_ModelLayer.ViewModels;
// using KopkeHome_ModelLayer.ViewModels.PaymentModels;
// using KopkeHome_UtilityLayer;
// using Microsoft.AspNetCore.Mvc;
// using Stripe;
// using Stripe.Checkout;

// namespace KopkeHome_FMRS_API.Controllers
// {

//     [Route("[controller]/[action]")]
//     [ApiController]
//     public class PaymentController : ControllerBase
//     {
//         private readonly IAccount _Userservice;
//         private readonly IConfiguration _configuration;
//         private readonly ILogger<PaymentController> _logger;
//         private readonly IPaymentService _service;
//         private readonly IEmailService _email;
//         private readonly IMembership _Membership;

//         public PaymentController(IMembership Membership, IEmailService email, IConfiguration configuration, IPaymentService service, ILogger<PaymentController> logger, IAccount Userservice)
//         {
//             _Membership = Membership;
//             _service = service;
//             _logger = logger;
//             _Userservice = Userservice;
//             _configuration = configuration;
//             _email = email;
//             StripeConfiguration.ApiKey = _configuration.GetValue<string>("Stripe:SecretKey");
//         }

//         [HttpPost]
//         public async Task<UserMembershipSubscriptions> AddPaymentTransactionDetails(UserMembershipSubscriptions model)
//         {
//             try
//             {



//                 return await _service.AddPaymentTransactionInfo(model);
//             }
//             catch (Exception ex)
//             {

//                 _logger.LogError(ex.Message);
//                 throw;
//             }

//         }
//         [HttpPost]
//         public async Task<UserMembershipSubscriptions> UpdatePaymentTransactionInfo(UserMembershipSubscriptions model)
//         {
//             try
//             {



//                 return await _service.UpdatePaymentTransactionInfo(model);
//             }
//             catch (Exception ex)
//             {

//                 _logger.LogError(ex.Message);
//                 throw;
//             }

//         }
//         [HttpPost]
//         public async Task<UserMembershipSubscriptions> GetSubscriptionDetailByUserId([FromForm] string UserId)
//         {
//             try
//             {
//                 return await _service.GetSubscriptionsInfoByUserId(Convert.ToInt32(UserId));
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex.Message);
//                 throw;
//             }

//         }

//         [HttpGet]
//         public async Task<MembershipPlanViewmodelApp> GetSubscriptionDetailByUserIdApp(string UserId)
//         {
//             try
//             {
//                 return await _service.GetSubscriptionsInfoByUserIdApp(Convert.ToInt32(UserId));
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex.Message);
//                 throw;
//             }

//         }
//         [HttpPost]
//         public async Task<Response> CheckUserHaveSubscriptionOrNotByEmail([FromForm] string Email)
//         {
//             try
//             {
//                 Response httpResponse = new Response();

//                 httpResponse.Data = false;
//                 var user = _Userservice.GetUserByEmail(Email);
//                 if (user != null)
//                 {
//                     var plans = await GetSubscriptionDetailByUserId(user.Result.Id.ToString());

//                     if (plans != null)
//                     {
//                         httpResponse.Message = Resources.SubscriptionMsg;
//                         httpResponse.Data = true;
//                         httpResponse.Statuscode = System.Net.HttpStatusCode.BadRequest;
//                     }
//                     else
//                     {
//                         httpResponse.Message = Resources.IsSubscription;

//                         httpResponse.Statuscode = System.Net.HttpStatusCode.NotFound;
//                     }
//                 }
//                 else
//                 {
//                     httpResponse.Message = Resources.UserNotFound;

//                     httpResponse.Statuscode = System.Net.HttpStatusCode.NotFound;
//                 }

//                 return httpResponse;
//             }
//             catch (Exception ex)
//             {

//                 _logger.LogError(ex.Message);
//                 throw;
//             }
//         }

//         /// <summary>
//         /// subscribe to a custom plan
//         /// </summary>
//         /// <param name="model"></param>
//         /// <returns></returns>

//         [HttpPost]
//         public async Task<Response> SubscribeToAPlanCustom(SubscribeToAPlanModel model)
//         {
//             try
//             {
//                 Response Response = new Response();

//                 BillingModel userBilling = new();


//                 var user = await _Userservice.GetUserByID(model.UserID);
//                 if (user == null)
//                 {
//                     Response.Status = Resources.FailureMsg;
//                     Response.Message = Resources.RegisterYourself;
//                     Response.Statuscode = System.Net.HttpStatusCode.NotFound;
//                     return Response;
//                 }
//                 else
//                 {
//                     var plans = await GetSubscriptionDetailByUserId(user.Id.ToString());

//                     if (plans != null)
//                     {
//                         if (plans.PlanId==13)
//                         {
//                             Response.Message = Resources.SubscriptionMsg;
//                             Response.Status = Resources.isSubscribed;
//                             Response.Statuscode = System.Net.HttpStatusCode.BadRequest;
//                             return Response;
//                         }
                        
//                     }
//                 }

//                 userBilling.User = user;

//                 //  User's Billing Details
//                 userBilling.BillingName = userBilling.User.FirstName + " " + userBilling.User.LastName;
//                 userBilling.BillingEmail = userBilling.User.Email;
//                 userBilling.BillingAddress = userBilling.User.BusinessAddress;
//                 userBilling.BillingPhoneNumber = userBilling.User.PhoneNumber;




//                 var service = new PriceService();
//                 Price price = service.Get(model.StripePriceId);
//                 userBilling.Interval = price.Recurring.Interval;
//                 userBilling.PriceInCent = price.UnitAmount;
//                 userBilling.PriceInDollar = Convert.ToDecimal(price.UnitAmountDecimal / 100).ToString("0.00");
//                 userBilling.Currency = price.Currency;
//                 userBilling.ProductId = price.ProductId;


//                 //creating token step-1
//                 //  var token = CreateToken(userBilling);
//                 var stripeCustomer = CreateCustomer(userBilling.User);
//                 var priceService = new PriceService();
//                 var CurrentDomain = _configuration.GetValue<string>("Stripe:SecretKey");
                
//                 var options = new Stripe.Checkout.SessionCreateOptions
//                 {


//                     LineItems = new List<SessionLineItemOptions>
//                     {
//                         new SessionLineItemOptions
//                         {
//                             Price = model.StripePriceId,
//                             Quantity = 1,

//                         },
//                     },
//                     Customer = stripeCustomer.Id,
//                     AllowPromotionCodes = false,

//                     Mode = "subscription",

//                     ////Local url
//                     SuccessUrl = _configuration.GetValue<string>("PaymentUrl:PaymentSuccessCustomPlanURLWeb") + "?session_id={CHECKOUT_SESSION_ID}",
//                     CancelUrl = _configuration.GetValue<string>("PaymentUrl:PaymentFailUrl"),
//                     //var link = Url.Action("PaymentSuccess", "Membership", new {  session_id = "{CHECKOUT_SESSION_ID}" }, Request.Scheme);

//                 };
//                 var service2 = new SessionService();

//                 Session session = await service2.CreateAsync(options);
//                 Response.Statuscode = System.Net.HttpStatusCode.OK;
//                 Response.Message = Resources.FollowUrl;
//                 Response.Data = session.Url;
//                 return Response;
//                 //return View(userBilling);
//             }
//             catch (Exception ex)
//             {
//                 throw;

//             }
//         }
//         /// <summary>
//         /// This method subscribe a plan.
//         /// </summary>
//         /// <param name="Email"></param>
//         /// <param name="priceId"></param>
//         /// <returns></returns>

//         [HttpPost]
//         public async Task<Response> SubscribeToAPlan(SubscribeToAPlanModel model)
//         {
//             try
//             {
//                 Response Response = new Response();

//                 BillingModel userBilling = new();


//                 var user = await _Userservice.GetUserByID(model.UserID);
//                 if (user == null)
//                 {
//                     Response.Status = Resources.FailureMsg;
//                     Response.Message = Resources.RegisterYourself;
//                     Response.Statuscode = System.Net.HttpStatusCode.NotFound;
//                     return Response;
//                 }
//                 else
//                 {
//                     var plans = await GetSubscriptionDetailByUserId(user.Id.ToString());

//                     if (plans != null)
//                     {
//                         Response.Message = Resources.SubscriptionMsg;
//                         Response.Status = Resources.isSubscribed;
//                         Response.Statuscode = System.Net.HttpStatusCode.BadRequest;
//                         return Response;
//                     }
//                 }

//                 userBilling.User = user;

//                 //  User's Billing Details
//                 userBilling.BillingName = userBilling.User.FirstName + " " + userBilling.User.LastName;
//                 userBilling.BillingEmail = userBilling.User.Email;
//                 userBilling.BillingAddress = userBilling.User.BusinessAddress;
//                 userBilling.BillingPhoneNumber = userBilling.User.PhoneNumber;




//                 var service = new PriceService();
//                 Price price = service.Get(model.StripePriceId);
//                 userBilling.Interval = price.Recurring.Interval;
//                 userBilling.PriceInCent = price.UnitAmount;
//                 userBilling.PriceInDollar = Convert.ToDecimal(price.UnitAmountDecimal / 100).ToString("0.00");
//                 userBilling.Currency = price.Currency;
//                 userBilling.ProductId = price.ProductId;


//                 //creating token step-1
//                 //  var token = CreateToken(userBilling);
//                 var stripeCustomer = CreateCustomer(userBilling.User);
//                 var priceService = new PriceService();
//                 var CurrentDomain = _configuration.GetValue<string>("Stripe:SecretKey");
//                 var stripePriceId = _configuration.GetValue<string>("Stripe:PriceId");

//                 System.Diagnostics.Debug.WriteLine($"Plan: {model.StripePriceId}");
//                 System.Diagnostics.Debug.WriteLine($"Verification: {stripePriceId}");

//                 Console.WriteLine($"Plan: {model.StripePriceId}");
//                 Console.WriteLine($"Verification: {stripePriceId}");

//                 // Build the line items list, starting with the plan's own price.
//                 var lineItems = new List<SessionLineItemOptions>
//                 {
//                     new SessionLineItemOptions
//                     {
//                         Price = model.StripePriceId,
//                         Quantity = 1,
//                     },
//                 };

//                 // Only add the $99 verification/service-fee line item when:
//                 //   1. A fee price is actually configured, and
//                 //   2. The selected plan is NOT a free plan (UnitAmount > 0), and
//                 //   3. The fee price is not the same Stripe Price as the plan itself
//                 //      (guards against the "duplicate recurring price" Stripe error).
//                 bool isFreePlan = price.UnitAmount is null or 0;

//                 if (!isFreePlan
//                     && !string.IsNullOrWhiteSpace(stripePriceId)
//                     && !string.Equals(stripePriceId, model.StripePriceId, StringComparison.OrdinalIgnoreCase))
//                 {
//                     lineItems.Add(new SessionLineItemOptions
//                     {
//                         Price = stripePriceId, // Replace with the actual Price ID of your service fee in Stripe
//                         Quantity = 1,
//                     });
//                 }
//                 else
//                 {
//                     _logger.LogInformation($"Skipping verification fee line item for plan price {model.StripePriceId} (isFreePlan={isFreePlan}).");
//                 }

//                 var options = new Stripe.Checkout.SessionCreateOptions
//                 {
//                     LineItems = lineItems,
//                     Customer = stripeCustomer.Id,
//                     AllowPromotionCodes = false,

//                     Mode = "subscription",

//                     ////Local url
//                     SuccessUrl = _configuration.GetValue<string>("PaymentUrl:PaymentSuccessURLWeb") + "?session_id={CHECKOUT_SESSION_ID}",
//                     CancelUrl = _configuration.GetValue<string>("PaymentUrl:PaymentFailUrl"),
//                     //var link = Url.Action("PaymentSuccess", "Membership", new {  session_id = "{CHECKOUT_SESSION_ID}" }, Request.Scheme);

//                 };
//                 var service2 = new SessionService();

//                 Session session = await service2.CreateAsync(options);
//                 Response.Statuscode = System.Net.HttpStatusCode.OK;
//                 Response.Message = Resources.FollowUrl;
//                 Response.Data = session.Url;
//                 return Response;
//                 //return View(userBilling);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex.Message);
//                 throw;

//             }
//         }


//         [HttpPost]
//         public async Task<Response> SubscribeToFreePlan(FreeMembershipRequest model)
//         {
//             try
//             {
//                 Response response = new Response();

//                 var user = await _Userservice.GetUserByID(model.UserId);

//                 if (user == null)
//                 {
//                     response.Status = Resources.FailureMsg;
//                     response.Message = Resources.RegisterYourself;
//                     response.Statuscode = System.Net.HttpStatusCode.NotFound;

//                     return response;
//                 }

//                 // Check whether user already has a membership
//                 var existingSubscription =
//                     await GetSubscriptionDetailByUserId(user.Id.ToString());

//                 if (existingSubscription != null)
//                 {
//                     response.Status = Resources.isSubscribed;
//                     response.Message = Resources.SubscriptionMsg;
//                     response.Statuscode = System.Net.HttpStatusCode.BadRequest;

//                     return response;
//                 }

//                 // Get membership plans
//                 var membershipPlans = await _Membership.GetMembershipPlans();

//                 var membershipPlan = membershipPlans
//                     .FirstOrDefault(x => x.Id == model.PlanId);

//                 if (membershipPlan == null)
//                 {
//                     response.Status = Resources.FailureMsg;
//                     response.Message = "Membership plan not found.";
//                     response.Statuscode = System.Net.HttpStatusCode.NotFound;

//                     return response;
//                 }

//                 // IMPORTANT:
//                 // Only allow this endpoint for a FREE plan.
//                 if (membershipPlan.PricePerYear != 0)
//                 {
//                     response.Status = Resources.FailureMsg;
//                     response.Message = "This is not a free membership plan.";
//                     response.Statuscode = System.Net.HttpStatusCode.BadRequest;

//                     return response;
//                 }

//                 // Create free membership record
//                 UserMembershipSubscriptions subscription =
//                     new UserMembershipSubscriptions();

//                 subscription.PlanId = membershipPlan.Id;
//                 subscription.Email = user.Email;

//                 subscription.PaymentStatus = "Paid";
//                 subscription.StripeStatus = "complete";

//                 subscription.StripeSubscriptionId = null;
//                 subscription.StripeCustomerID = null;
//                 subscription.StripePriceId = null;

//                 subscription.PeriodStartDate = DateTime.UtcNow;
//                 subscription.PeriodEndDate = DateTime.UtcNow.AddYears(1);

//                 var result =
//                     await AddPaymentTransactionDetails(subscription);

//                 if (result != null)
//                 {
//                     response.Status = Resources.SuccessMsg;
//                     response.Message = "Free membership activated successfully.";
//                     response.Statuscode = System.Net.HttpStatusCode.OK;
//                     response.Data = result;

//                     return response;
//                 }

//                 response.Status = Resources.FailureMsg;
//                 response.Message = "Unable to create membership.";
//                 response.Statuscode = System.Net.HttpStatusCode.InternalServerError;

//                 return response;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex.Message);
//                 throw;
//             }
//         }

//         [NonAction]
//         public Customer CreateCustomer(User _User)
//         {
//             AddressOptions address = new AddressOptions
//             {
//                 State = _User.State,
//                 Country = "US",
//                 City = _User.City,
//                 PostalCode = _User.ZipCode,
//                 Line1 = _User.BusinessAddress,
//                 Line2 = _User.BusinessAddress
//             };
//             ShippingOptions ShippingOptions = new ShippingOptions
//             {

//                 Address = address,
//                 Name = _User.FirstName + " " + _User.LastName,
//                 Phone = _User.PhoneNumber,

//             };

//             //craete a customer
//             var customerCreateOptions = new CustomerCreateOptions
//             {
//                 Name = _User.FirstName + " " + _User.LastName,
//                 Email = _User.Email,
//                 Address = address,
//                 // Source = sourceid,
//                 Shipping = ShippingOptions,


//             };

//             var service = new CustomerService();
//             var stripeCustomer = service.Create(customerCreateOptions);
//             return stripeCustomer;
//         }


//         /// <summary>
//         /// Gets subscription details by stripe customer Id
//         /// </summary>
//         /// <param name="StripeCustomerId"></param>
//         /// <returns></returns>
//         [HttpGet]
//         public async Task<UserMembershipSubscriptions> GetCustomerByStripCustomerId([FromForm] string StripeCustomerId)
//         {
//             try
//             {
//                 return await _service.GetSubscriptionByStripCustomerId(StripeCustomerId);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex.Message);
//                 throw;
//             }
//         }

//         [HttpPost]
//         public async Task<UserMembershipSubscriptions> PaymentSuccess(PaymentSuccessAPIModel Datamodel)
//         {
//             UserMembershipSubscriptions model = new UserMembershipSubscriptions();
//             string InvoiceNumber = string.Empty;
//             try
//             {

//                 // StripeConfiguration.ApiKey = _config["StripeConfigurationApiKey"];
//                 var sessionService = new SessionService();
//                 Session session = await sessionService.GetAsync(Datamodel.SessionId);

//                 var customerService = new CustomerService();
//                 Customer customer = await customerService.GetAsync(session.CustomerId);
//                 //if (session.StripeResponse.StatusCode)
//                 //{
//                 //}
//                 /// Get Stripe Response From Session
//                 string StripeStatus = session.Status;
//                 string StripeSubscriptionId = session.SubscriptionId;
//                 string PaymentStatus = session.PaymentStatus;
//                 string StripeCustomerId = session.CustomerId;
//                 string Email = customer.Email;

//                 var options = new InvoiceListOptions
//                 {
//                     Subscription = session.SubscriptionId

//                 };


//                 var service = new InvoiceService();
//                 StripeList<Invoice> invoices = await service.ListAsync(
//                   options);

//                 var subscriptionService = new SubscriptionService();
//                 var subscriptionResult = await subscriptionService.GetAsync(StripeSubscriptionId);
//                 /// Get Setripe invoices details from stripe invoice service


//                 DateTime PeriodStartDate = subscriptionResult.CurrentPeriodStart;
//                 DateTime PeriodEndDate = subscriptionResult.CurrentPeriodEnd;
//                 string priceid = subscriptionResult.Items.Data[0].Price.Id;
//                 model.PaymentStatus = PaymentStatus;
//                 model.StripeStatus = StripeStatus;
//                 //model.PlanId = planid;
//                 model.StripeSubscriptionId = StripeSubscriptionId;
//                 model.InvoiceNumber = InvoiceNumber;
//                 model.PeriodEndDate = PeriodEndDate;
//                 model.PeriodStartDate = PeriodStartDate;
//                 model.StripeCustomerID = StripeCustomerId;


//                 model.StripePriceId = priceid;
//                 model.InvoiceNumber = invoices.Data[0].Number;
//                 model.InvoiceUrl = invoices.Data[0].InvoicePdf;
//                 model.Email = Email;


//                 // Map Stripe Price ID to Membership Plan
//                 var membershipPlans = await _Membership.GetMembershipPlans();

//                 var membershipPlan = membershipPlans
//                     .FirstOrDefault(x =>
//                         x.AnnuallyStripePriceId == priceid ||
//                         x.MonthlyStripePriceId == priceid);


//                 if (membershipPlan != null)
//                 {
//                     model.PlanId = membershipPlan.Id;
//                 }
//                 else
//                 {
//                     model.PlanId = 0;
//                 }


//                 return await AddPaymentTransactionDetails(model);



//             }

//             catch (Exception ex)
//             {
//                 _logger.LogError(ex.Message);
//                 throw;
//             }

//         }
//         [HttpPost]
//         public async Task<Response> UpgradeSubscription(UpgradeSubscriptionRequestModel model)
//         {
//             Response response = new Response();
//             // var s = await CancelSubscription(model.StripesubId);



//             var options = new Stripe.Checkout.SessionCreateOptions
//             {


//                 LineItems = new List<SessionLineItemOptions>
//                     {
//                         new SessionLineItemOptions
//                         {
//                             Price = model.StripePriceId,
//                             Quantity = 1,

//                         },
//                     },
//                 Customer = model.StripeCusId,
//                 AllowPromotionCodes = false,

//                 Mode = "subscription",

//                 SuccessUrl = _configuration.GetValue<string>("PaymentUrl:PaymentSuccessURLWeb") + "?session_id={CHECKOUT_SESSION_ID}",
//                 CancelUrl = _configuration.GetValue<string>("PaymentUrl:PaymentFailUrl"),

//             };
//             var service2 = new SessionService();

//             Session session = await service2.CreateAsync(options);


//             var cancelSub = await CancelSubscription(model.StripesubId);
//             response.Statuscode = System.Net.HttpStatusCode.OK;
//             response.Status = Resources.SuccessMsg;
//             response.Data = session.Url;


//             return response;


//         }

//         [HttpPost]
//         public async Task<Response> CancelSubscription([FromForm] string subId)
//         {
//             try
//             {
//                 Response response = new Response();
//                 UserMembershipSubscriptions model = new UserMembershipSubscriptions();
//                 var service = new SubscriptionService();
//                 Subscription subscription = await service.GetAsync(subId);

//                 var items = new List<SubscriptionItemOptions> {
//                         new SubscriptionItemOptions {
//                             Id = subscription.Items.Data[0].Id,

//                         },
//                                 };

//                 var options = new SubscriptionUpdateOptions
//                 {
//                     CancelAtPeriodEnd = true,
//                     // ProrationBehavior = "always_invoice",
//                     Items = items,
//                 };
//                 subscription.CancelAtPeriodEnd = true;
//                 subscription = await service.UpdateAsync(subId, options);

//                 model.StripeSubscriptionId = subId;
//                 model.StripeStatus = "Cancelled";
//                 model.CancelledOn = DateTime.Now;

//                 var SSoptions = new SubscriptionScheduleListOptions
//                 {
//                     Limit = 10,
//                     Customer = subscription.CustomerId
//                 };
//                 var service2 = new SubscriptionScheduleService();
//                 StripeList<SubscriptionSchedule> subscriptionSchedules = service2.List(SSoptions);
//                 if (subscriptionSchedules.Data.Any())
//                 {
//                     //foreach (var item in subscriptionSchedules.Data)
//                     //{
//                     //    if (item.Status != "canceled")
//                     //    {
//                     //        var SubSchdservice = new SubscriptionScheduleService();
//                     //        SubSchdservice.Cancel(
//                     //          item.Id);
//                     //    }
//                     //}
//                 }

//                 var result = await UpdatePaymentTransactionInfo(model);
//                 if (result != null)
//                 {
//                     response.Status = Resources.SuccessMsg;
//                     response.Statuscode = System.Net.HttpStatusCode.OK;
//                     return response;
//                 }
//                 response.Status = Resources.FailureMsg;
//                 response.Statuscode = System.Net.HttpStatusCode.NotFound;
//                 return response;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex.Message);
//                 throw;
//             }


//         }

//         [HttpPost]
//         public async Task<Response> DowngradeSubscription(DowngradeSubscriptionRequestModel Model)
//         {
//             Response response = new Response();
//             var SUB = new SubscriptionService();
//             var res = SUB.Get(Model.StripesubId);
//             var endsAt = res.CurrentPeriodEnd;

//             //payment link generation.

//             var options2 = new Stripe.Checkout.SessionCreateOptions
//             {


//                 LineItems = new List<SessionLineItemOptions>
//                     {
//                         new SessionLineItemOptions
//                         {
//                             Price = Model.StripePriceId,
//                             Quantity = 1,

//                         },
//                     },
//                 Customer = Model.StripeCusId,
//                 AllowPromotionCodes = false,

//                 Mode = "subscription",

//                 ////Local url
//                 SuccessUrl = _configuration.GetValue<string>("PaymentUrl:PaymentSuccessURLWeb") + "?session_id={CHECKOUT_SESSION_ID}",
//                 CancelUrl = _configuration.GetValue<string>("PaymentUrl:PaymentFailUrl"),

//             };
//             var service2 = new SessionService();

//             Session session = await service2.CreateAsync(options2);
//             var cancelSub = await CancelSubscription(Model.StripesubId);
//             response.Statuscode = System.Net.HttpStatusCode.OK;
//             response.Status = Resources.SuccessMsg;
//             response.Data = session.Url;
//             return response;



//         }


//         [HttpPost]
//         public async Task<Response> CreateCustomPriceSubscription(CustomPlanViewModel Model)
//         {
//             Response response = new Response();

//             //payment link generation.
//             // StripeConfiguration.ApiKey = "sk_test_4eC39HqLyjWDarjtT1zdp7dc";
//             var user = await _Userservice.GetUserByID(Model.UserId);
//             if (user != null)
//             {
//                 long price = (long)Convert.ToDouble(Model.Price) * 100;

//                 string Interval = "month";
//                 if (Model.IsYearly)
//                 {
//                     //  price = Model.PriceYearly;
//                     Interval = "year";
//                 }
//                 else
//                 {
//                     // price = Model.PriceMonthly;
//                 }
//                 var options = new PriceCreateOptions
//                 {
//                     UnitAmount = price,
//                     Currency = "usd",
//                     Recurring = new PriceRecurringOptions
//                     {
//                         Interval = Interval,
//                     },
//                     Product = _configuration.GetValue<string>("Stripe:CustomMembershipProductId"),
//                 };
//                 var service = new PriceService();
//                 var Createresponse = service.Create(options);
//                 string body = string.Empty;
//                 body = Resources.CustomMembershipHtml;

//                 body = body.Replace("{URL}", _configuration.GetValue<string>("PaymentUrl:CustomPlanRequestURL") + user.Id);
//                 body = body.Replace("{ImagePath}", _configuration.GetValue<string>("PaymentUrl:CurrentDomain") + "images/Kopke-brand-logo.png");
//                 var html = body;
//                 var IsSent = _email.SendEmail(user.Email, Resources.CustomMembershipPlanEmailHeader, html);
//                 response.Statuscode = System.Net.HttpStatusCode.OK;
//                 response.Message = "Link sent to user email.";
//                 response.Status = Resources.SuccessMsg;
//                 response.Data = Createresponse;



//                 CustomZipcodesRequest customZipcodesRequest = new CustomZipcodesRequest();

//                 customZipcodesRequest.WebApp = Model.WebApp;
//                 customZipcodesRequest.MobileApp = Model.MobileApp;
//                 customZipcodesRequest.NumberOfCategories = Model.NumberOfCategories;
//                 customZipcodesRequest.NumberOfZipcodes = Model.NumberOfZipcodes;
//                 customZipcodesRequest.PriceMonthly = Model.Price;
//                 customZipcodesRequest.StripePriceYearly = Createresponse.Id;
//                 customZipcodesRequest.StripePriceMonthly = Createresponse.Id;
//                 customZipcodesRequest.PriceYearly = Model.Price;
//                 customZipcodesRequest.UserId = Model.UserId;


//                 await _Membership.UpdateCustomZipcodeRequest(customZipcodesRequest);
//             }

//             return response;

//         }

//     }

// }














using KopkeHome_FMRS_API.Properties;
using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.DataModel.PaymentAndSubscription;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.PaymentModels;
using KopkeHome_UtilityLayer;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace KopkeHome_FMRS_API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IAccount _Userservice;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _service;
        private readonly IEmailService _email;
        private readonly IMembership _Membership;

        public PaymentController(
            IMembership Membership,
            IEmailService email,
            IConfiguration configuration,
            IPaymentService service,
            ILogger<PaymentController> logger,
            IAccount Userservice)
        {
            _Membership = Membership;
            _service = service;
            _logger = logger;
            _Userservice = Userservice;
            _configuration = configuration;
            _email = email;

            StripeConfiguration.ApiKey =
                _configuration.GetValue<string>("Stripe:SecretKey");
        }

        // ============================================================
        // PAYMENT TRANSACTION
        // ============================================================

        [HttpPost]
        public async Task<UserMembershipSubscriptions>
            AddPaymentTransactionDetails(
                UserMembershipSubscriptions model)
        {
            try
            {
                return await _service.AddPaymentTransactionInfo(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error adding payment transaction details.");

                throw;
            }
        }

        [HttpPost]
        public async Task<UserMembershipSubscriptions>
            UpdatePaymentTransactionInfo(
                UserMembershipSubscriptions model)
        {
            try
            {
                return await _service.UpdatePaymentTransactionInfo(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating payment transaction details.");

                throw;
            }
        }

        // ============================================================
        // SUBSCRIPTION DETAILS
        // ============================================================

        [HttpPost]
        public async Task<UserMembershipSubscriptions>
            GetSubscriptionDetailByUserId(
                [FromForm] string UserId)
        {
            try
            {
                return await _service.GetSubscriptionsInfoByUserId(
                    Convert.ToInt32(UserId));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error getting subscription by user ID.");

                throw;
            }
        }

        [HttpGet]
        public async Task<MembershipPlanViewmodelApp>
            GetSubscriptionDetailByUserIdApp(
                string UserId)
        {
            try
            {
                return await _service.GetSubscriptionsInfoByUserIdApp(
                    Convert.ToInt32(UserId));
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error getting subscription app details.");

                throw;
            }
        }

        // ============================================================
        // CHECK SUBSCRIPTION
        // ============================================================

        [HttpPost]
        public async Task<Response>
            CheckUserHaveSubscriptionOrNotByEmail(
                [FromForm] string Email)
        {
            try
            {
                Response httpResponse = new Response
                {
                    Data = false
                };

                var userTask =
                    _Userservice.GetUserByEmail(Email);

                var user = await userTask;

                if (user != null)
                {
                    var plans =
                        await GetSubscriptionDetailByUserId(
                            user.Id.ToString());

                    if (plans != null)
                    {
                        httpResponse.Message =
                            Resources.SubscriptionMsg;

                        httpResponse.Data = true;

                        httpResponse.Statuscode =
                            System.Net.HttpStatusCode.BadRequest;
                    }
                    else
                    {
                        httpResponse.Message =
                            Resources.IsSubscription;

                        httpResponse.Statuscode =
                            System.Net.HttpStatusCode.NotFound;
                    }
                }
                else
                {
                    httpResponse.Message =
                        Resources.UserNotFound;

                    httpResponse.Statuscode =
                        System.Net.HttpStatusCode.NotFound;
                }

                return httpResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error checking user subscription.");

                throw;
            }
        }

        // ============================================================
        // NORMAL SUBSCRIPTION
        // ============================================================

        [HttpPost]
        public async Task<Response>
            SubscribeToAPlan(
                SubscribeToAPlanModel model)
        {
            try
            {
                Response response = new Response();

                if (model == null)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        "Invalid subscription request.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                if (string.IsNullOrWhiteSpace(
                    model.StripePriceId))
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        "Stripe Price ID is required.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                var user =
                    await _Userservice.GetUserByID(
                        model.UserID);

                if (user == null)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        Resources.RegisterYourself;

                    response.Statuscode =
                        System.Net.HttpStatusCode.NotFound;

                    return response;
                }

                var existingSubscription =
                    await GetSubscriptionDetailByUserId(
                        user.Id.ToString());

                if (existingSubscription != null)
                {
                    response.Message =
                        Resources.SubscriptionMsg;

                    response.Status =
                        Resources.isSubscribed;

                    response.Statuscode =
                        System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                var priceService =
                    new PriceService();

                Price price =
                    await priceService.GetAsync(
                        model.StripePriceId);

                if (price == null)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        "Stripe price was not found.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.NotFound;

                    return response;
                }

                var stripeCustomer =
                    CreateCustomer(user);

                var lineItems =
                    new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            Price =
                                model.StripePriceId,

                            Quantity = 1
                        }
                    };

                string verificationPriceId =
                    _configuration.GetValue<string>(
                        "Stripe:PriceId");

                bool isFreePlan =
                    !price.UnitAmount.HasValue ||
                    price.UnitAmount.Value == 0;

                if (!isFreePlan &&
                    !string.IsNullOrWhiteSpace(
                        verificationPriceId) &&
                    !string.Equals(
                        verificationPriceId,
                        model.StripePriceId,
                        StringComparison.OrdinalIgnoreCase))
                {
                    lineItems.Add(
                        new SessionLineItemOptions
                        {
                            Price =
                                verificationPriceId,

                            Quantity = 1
                        });
                }
                else
                {
                    _logger.LogInformation(
                        "Verification fee skipped. PriceId={PriceId}, IsFreePlan={IsFreePlan}",
                        model.StripePriceId,
                        isFreePlan);
                }

                var options =
                    new SessionCreateOptions
                    {
                        LineItems =
                            lineItems,

                        Customer =
                            stripeCustomer.Id,

                        AllowPromotionCodes =
                            false,

                        Mode =
                            "subscription",

                        SuccessUrl =
                            _configuration.GetValue<string>(
                                "PaymentUrl:PaymentSuccessURLWeb")
                            + "?session_id={CHECKOUT_SESSION_ID}",

                        CancelUrl =
                            _configuration.GetValue<string>(
                                "PaymentUrl:PaymentFailUrl")
                    };

                var sessionService =
                    new SessionService();

                Session session =
                    await sessionService.CreateAsync(
                        options);

                response.Statuscode =
                    System.Net.HttpStatusCode.OK;

                response.Message =
                    Resources.FollowUrl;

                response.Data =
                    session.Url;

                return response;
            }
            catch (StripeException ex)
            {
                _logger.LogError(
                    ex,
                    "Stripe error creating subscription Checkout.");

                return new Response
                {
                    Status =
                        Resources.FailureMsg,

                    Message =
                        ex.StripeError?.Message ??
                        ex.Message,

                    Statuscode =
                        System.Net.HttpStatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating subscription Checkout.");

                throw;
            }
        }

        // ============================================================
        // CUSTOM PLAN
        // ============================================================

        [HttpPost]
        public async Task<Response>
            SubscribeToAPlanCustom(
                SubscribeToAPlanModel model)
        {
            try
            {
                Response response = new Response();

                if (model == null)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        "Invalid subscription request.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                if (string.IsNullOrWhiteSpace(
                    model.StripePriceId))
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        "Stripe Price ID is required.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                var user =
                    await _Userservice.GetUserByID(
                        model.UserID);

                if (user == null)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        Resources.RegisterYourself;

                    response.Statuscode =
                        System.Net.HttpStatusCode.NotFound;

                    return response;
                }

                var plans =
                    await GetSubscriptionDetailByUserId(
                        user.Id.ToString());

                if (plans != null &&
                    plans.PlanId == 13)
                {
                    response.Message =
                        Resources.SubscriptionMsg;

                    response.Status =
                        Resources.isSubscribed;

                    response.Statuscode =
                        System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                var priceService =
                    new PriceService();

                Price price =
                    await priceService.GetAsync(
                        model.StripePriceId);

                if (price == null)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        "Stripe price was not found.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.NotFound;

                    return response;
                }

                var stripeCustomer =
                    CreateCustomer(user);

                var options =
                    new SessionCreateOptions
                    {
                        LineItems =
                            new List<SessionLineItemOptions>
                            {
                                new SessionLineItemOptions
                                {
                                    Price =
                                        model.StripePriceId,

                                    Quantity = 1
                                }
                            },

                        Customer =
                            stripeCustomer.Id,

                        AllowPromotionCodes =
                            false,

                        Mode =
                            "subscription",

                        SuccessUrl =
                            _configuration.GetValue<string>(
                                "PaymentUrl:PaymentSuccessCustomPlanURLWeb")
                            + "?session_id={CHECKOUT_SESSION_ID}",

                        CancelUrl =
                            _configuration.GetValue<string>(
                                "PaymentUrl:PaymentFailUrl")
                    };

                var sessionService =
                    new SessionService();

                Session session =
                    await sessionService.CreateAsync(
                        options);

                response.Statuscode =
                    System.Net.HttpStatusCode.OK;

                response.Message =
                    Resources.FollowUrl;

                response.Data =
                    session.Url;

                return response;
            }
            catch (StripeException ex)
            {
                _logger.LogError(
                    ex,
                    "Stripe error creating custom plan.");

                return new Response
                {
                    Status =
                        Resources.FailureMsg,

                    Message =
                        ex.StripeError?.Message ??
                        ex.Message,

                    Statuscode =
                        System.Net.HttpStatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating custom plan.");

                throw;
            }
        }

        // ============================================================
        // FREE PLAN
        // ============================================================

        [HttpPost]
        public async Task<Response>
            SubscribeToFreePlan(
                FreeMembershipRequest model)
        {
            try
            {
                Response response = new Response();

                if (model == null)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        "Invalid free membership request.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                var user =
                    await _Userservice.GetUserByID(
                        model.UserId);

                if (user == null)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        Resources.RegisterYourself;

                    response.Statuscode =
                        System.Net.HttpStatusCode.NotFound;

                    return response;
                }

                var existingSubscription =
                    await GetSubscriptionDetailByUserId(
                        user.Id.ToString());

                if (existingSubscription != null)
                {
                    response.Status =
                        Resources.isSubscribed;

                    response.Message =
                        Resources.SubscriptionMsg;

                    response.Statuscode =
                        System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                var membershipPlans =
                    await _Membership.GetMembershipPlans();

                var membershipPlan =
                    membershipPlans.FirstOrDefault(
                        x => x.Id == model.PlanId);

                if (membershipPlan == null)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        "Membership plan not found.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.NotFound;

                    return response;
                }

                if (membershipPlan.PricePerYear != 0)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        "This is not a free membership plan.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                UserMembershipSubscriptions subscription =
                    new UserMembershipSubscriptions
                    {
                        PlanId =
                            membershipPlan.Id,

                        Email =
                            user.Email,

                        PaymentStatus =
                            "Paid",

                        StripeStatus =
                            "complete",

                        StripeSubscriptionId =
                            null,

                        StripeCustomerID =
                            null,

                        StripePriceId =
                            null,

                        PeriodStartDate =
                            DateTime.UtcNow,

                        PeriodEndDate =
                            DateTime.UtcNow.AddYears(1)
                    };

                var result =
                    await AddPaymentTransactionDetails(
                        subscription);

                if (result != null)
                {
                    response.Status =
                        Resources.SuccessMsg;

                    response.Message =
                        "Free membership activated successfully.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.OK;

                    response.Data =
                        result;

                    return response;
                }

                response.Status =
                    Resources.FailureMsg;

                response.Message =
                    "Unable to create membership.";

                response.Statuscode =
                    System.Net.HttpStatusCode.InternalServerError;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error activating free plan.");

                throw;
            }
        }

        // ============================================================
        // STRIPE CUSTOMER
        // ============================================================

        [NonAction]
        public Customer CreateCustomer(User _User)
        {
            AddressOptions address =
                new AddressOptions
                {
                    State =
                        _User.State,

                    Country =
                        "US",

                    City =
                        _User.City,

                    PostalCode =
                        _User.ZipCode,

                    Line1 =
                        _User.BusinessAddress,

                    Line2 =
                        _User.BusinessAddress
                };

            ShippingOptions shippingOptions =
                new ShippingOptions
                {
                    Address =
                        address,

                    Name =
                        _User.FirstName +
                        " " +
                        _User.LastName,

                    Phone =
                        _User.PhoneNumber
                };

            CustomerCreateOptions customerCreateOptions =
                new CustomerCreateOptions
                {
                    Name =
                        _User.FirstName +
                        " " +
                        _User.LastName,

                    Email =
                        _User.Email,

                    Address =
                        address,

                    Shipping =
                        shippingOptions
                };

            var service =
                new CustomerService();

            return service.Create(
                customerCreateOptions);
        }

        // ============================================================
        // GET SUBSCRIPTION BY STRIPE CUSTOMER
        // ============================================================

        [HttpGet]
        public async Task<UserMembershipSubscriptions>
            GetCustomerByStripCustomerId(
                [FromQuery] string StripeCustomerId)
        {
            try
            {
                return await _service
                    .GetSubscriptionByStripCustomerId(
                        StripeCustomerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error getting Stripe customer subscription.");

                throw;
            }
        }

        // ============================================================
        // PAYMENT SUCCESS
        // ============================================================

        [HttpPost]
        public async Task<UserMembershipSubscriptions>
            PaymentSuccess(
                PaymentSuccessAPIModel Datamodel)
        {
            UserMembershipSubscriptions model =
                new UserMembershipSubscriptions();

            string invoiceNumber =
                string.Empty;

            try
            {
                if (Datamodel == null ||
                    string.IsNullOrWhiteSpace(
                        Datamodel.SessionId))
                {
                    throw new ArgumentException(
                        "Stripe Checkout session ID is required.");
                }

                var sessionService =
                    new SessionService();

                Session session =
                    await sessionService.GetAsync(
                        Datamodel.SessionId);

                if (session == null)
                {
                    throw new Exception(
                        "Stripe Checkout session not found.");
                }

                string oldSubscriptionId =
                    null;

                if (session.Metadata != null &&
                    session.Metadata.ContainsKey(
                        "OldSubscriptionId"))
                {
                    oldSubscriptionId =
                        session.Metadata[
                            "OldSubscriptionId"];
                }

                string changeType =
                    null;

                if (session.Metadata != null &&
                    session.Metadata.ContainsKey(
                        "ChangeType"))
                {
                    changeType =
                        session.Metadata[
                            "ChangeType"];
                }

                _logger.LogInformation(
                    "Processing Stripe Checkout success. Session={SessionId}, ChangeType={ChangeType}, OldSubscription={OldSubscriptionId}",
                    Datamodel.SessionId,
                    changeType,
                    oldSubscriptionId);

                string stripeCustomerId =
                    session.CustomerId;

                Customer customer =
                    null;

                if (!string.IsNullOrWhiteSpace(
                    stripeCustomerId))
                {
                    var customerService =
                        new CustomerService();

                    customer =
                        await customerService.GetAsync(
                            stripeCustomerId);
                }

                string email =
                    customer?.Email;

                if (string.IsNullOrWhiteSpace(
                    email))
                {
                    email =
                        session.CustomerDetails?.Email;
                }

                string stripeStatus =
                    session.Status;

                string stripeSubscriptionId =
                    session.SubscriptionId;

                string paymentStatus =
                    session.PaymentStatus;

                if (string.IsNullOrWhiteSpace(
                    stripeSubscriptionId))
                {
                    throw new Exception(
                        "Stripe Checkout completed without a subscription ID.");
                }

                var subscriptionService =
                    new SubscriptionService();

                Subscription subscriptionResult =
                    await subscriptionService.GetAsync(
                        stripeSubscriptionId);

                if (subscriptionResult == null)
                {
                    throw new Exception(
                        "Stripe subscription could not be found.");
                }

                DateTime periodStartDate =
                    subscriptionResult.CurrentPeriodStart;

                DateTime periodEndDate =
                    subscriptionResult.CurrentPeriodEnd;

                string priceId =
                    null;

                if (subscriptionResult.Items != null &&
                    subscriptionResult.Items.Data != null &&
                    subscriptionResult.Items.Data.Count > 0)
                {
                    var firstItem =
                        subscriptionResult.Items.Data[0];

                    if (firstItem?.Price != null)
                    {
                        priceId =
                            firstItem.Price.Id;
                    }
                }

                if (string.IsNullOrWhiteSpace(
                    priceId))
                {
                    _logger.LogWarning(
                        "Unable to determine Stripe Price ID from subscription {SubscriptionId}.",
                        stripeSubscriptionId);
                }

                try
                {
                    var invoiceOptions =
                        new InvoiceListOptions
                        {
                            Subscription =
                                stripeSubscriptionId,

                            Limit = 1
                        };

                    var invoiceService =
                        new InvoiceService();

                    StripeList<Invoice> invoices =
                        await invoiceService.ListAsync(
                            invoiceOptions);

                    if (invoices != null &&
                        invoices.Data != null &&
                        invoices.Data.Count > 0)
                    {
                        Invoice invoice =
                            invoices.Data[0];

                        invoiceNumber =
                            invoice.Number;

                        model.InvoiceUrl =
                            invoice.InvoicePdf;
                    }
                }
                catch (Exception invoiceEx)
                {
                    _logger.LogWarning(
                        invoiceEx,
                        "Unable to retrieve invoice for subscription {SubscriptionId}.",
                        stripeSubscriptionId);
                }

                model.PaymentStatus =
                    paymentStatus;

                model.StripeStatus =
                    stripeStatus;

                model.StripeSubscriptionId =
                    stripeSubscriptionId;

                model.InvoiceNumber =
                    invoiceNumber;

                model.PeriodEndDate =
                    periodEndDate;

                model.PeriodStartDate =
                    periodStartDate;

                model.StripeCustomerID =
                    stripeCustomerId;

                model.StripePriceId =
                    priceId;

                model.Email =
                    email;

                var membershipPlans =
                    await _Membership.GetMembershipPlans();

                var membershipPlan =
                    membershipPlans.FirstOrDefault(
                        x =>
                            x.AnnuallyStripePriceId ==
                                priceId ||
                            x.MonthlyStripePriceId ==
                                priceId);

                if (membershipPlan != null)
                {
                    model.PlanId =
                        membershipPlan.Id;
                }
                else
                {
                    model.PlanId =
                        0;

                    _logger.LogWarning(
                        "No membership plan found for Stripe Price {PriceId}.",
                        priceId);
                }

                var result =
                    await AddPaymentTransactionDetails(
                        model);

                if (result == null)
                {
                    throw new Exception(
                        "New subscription could not be saved to the database.");
                }

                if (!string.IsNullOrWhiteSpace(
                    oldSubscriptionId) &&
                    !string.Equals(
                        oldSubscriptionId,
                        stripeSubscriptionId,
                        StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        await CancelStripeSubscriptionAfterReplacement(
                            oldSubscriptionId);
                    }
                    catch (Exception cancelEx)
                    {
                        _logger.LogError(
                            cancelEx,
                            "New subscription {NewSubscriptionId} succeeded, but old subscription {OldSubscriptionId} could not be cancelled.",
                            stripeSubscriptionId,
                            oldSubscriptionId);
                    }
                }

                return result;
            }
            catch (StripeException ex)
            {
                _logger.LogError(
                    ex,
                    "Stripe error processing PaymentSuccess.");

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error processing Stripe PaymentSuccess.");

                throw;
            }
        }

        // ============================================================
        // SAFE UPGRADE
        // ============================================================

        [HttpPost]
        public async Task<Response>
            UpgradeSubscription(
                UpgradeSubscriptionRequestModel model)
        {
            try
            {
                if (model == null)
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Invalid upgrade request.",

                        Statuscode =
                            System.Net.HttpStatusCode.BadRequest
                    };
                }

                if (string.IsNullOrWhiteSpace(
                    model.StripePriceId))
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Stripe Price ID is required.",

                        Statuscode =
                            System.Net.HttpStatusCode.BadRequest
                    };
                }

                if (string.IsNullOrWhiteSpace(
                    model.StripeCusId))
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Stripe Customer ID is required.",

                        Statuscode =
                            System.Net.HttpStatusCode.BadRequest
                    };
                }

                if (string.IsNullOrWhiteSpace(
                    model.StripesubId))
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Current Stripe Subscription ID is required.",

                        Statuscode =
                            System.Net.HttpStatusCode.BadRequest
                    };
                }

                var priceService =
                    new PriceService();

                Price newPrice =
                    await priceService.GetAsync(
                        model.StripePriceId);

                if (newPrice == null)
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Stripe price was not found.",

                        Statuscode =
                            System.Net.HttpStatusCode.NotFound
                    };
                }

                var subscriptionService =
                    new SubscriptionService();

                Subscription oldSubscription =
                    await subscriptionService.GetAsync(
                        model.StripesubId);

                if (oldSubscription == null)
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Current Stripe subscription was not found.",

                        Statuscode =
                            System.Net.HttpStatusCode.NotFound
                    };
                }

                var options =
                    new SessionCreateOptions
                    {
                        LineItems =
                            new List<SessionLineItemOptions>
                            {
                                new SessionLineItemOptions
                                {
                                    Price =
                                        model.StripePriceId,

                                    Quantity =
                                        1
                                }
                            },

                        Customer =
                            model.StripeCusId,

                        AllowPromotionCodes =
                            false,

                        Mode =
                            "subscription",

                        SuccessUrl =
                            _configuration.GetValue<string>(
                                "PaymentUrl:PaymentSuccessURLWeb")
                            + "?session_id={CHECKOUT_SESSION_ID}",

                        CancelUrl =
                            _configuration.GetValue<string>(
                                "PaymentUrl:PaymentFailUrl"),

                        Metadata =
                            new Dictionary<string, string>
                            {
                                {
                                    "OldSubscriptionId",
                                    model.StripesubId
                                },
                                {
                                    "NewPlanId",
                                    model.PlanId.ToString()
                                },
                                {
                                    "ChangeType",
                                    "Upgrade"
                                }
                            }
                    };

                var sessionService =
                    new SessionService();

                Session session =
                    await sessionService.CreateAsync(
                        options);

                return new Response
                {
                    Statuscode =
                        System.Net.HttpStatusCode.OK,

                    Status =
                        Resources.SuccessMsg,

                    Message =
                        Resources.FollowUrl,

                    Data =
                        session.Url
                };
            }
            catch (StripeException ex)
            {
                _logger.LogError(
                    ex,
                    "Stripe error during upgrade.");

                return new Response
                {
                    Status =
                        Resources.FailureMsg,

                    Message =
                        ex.StripeError?.Message ??
                        ex.Message,

                    Statuscode =
                        System.Net.HttpStatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating upgrade Checkout session.");

                throw;
            }
        }

        // ============================================================
        // SAFE DOWNGRADE
        // ============================================================

        [HttpPost]
        public async Task<Response>
            DowngradeSubscription(
                DowngradeSubscriptionRequestModel model)
        {
            try
            {
                if (model == null)
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Invalid downgrade request.",

                        Statuscode =
                            System.Net.HttpStatusCode.BadRequest
                    };
                }

                if (string.IsNullOrWhiteSpace(
                    model.StripePriceId))
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Stripe Price ID is required.",

                        Statuscode =
                            System.Net.HttpStatusCode.BadRequest
                    };
                }

                if (string.IsNullOrWhiteSpace(
                    model.StripeCusId))
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Stripe Customer ID is required.",

                        Statuscode =
                            System.Net.HttpStatusCode.BadRequest
                    };
                }

                if (string.IsNullOrWhiteSpace(
                    model.StripesubId))
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Current Stripe Subscription ID is required.",

                        Statuscode =
                            System.Net.HttpStatusCode.BadRequest
                    };
                }

                var priceService =
                    new PriceService();

                Price newPrice =
                    await priceService.GetAsync(
                        model.StripePriceId);

                if (newPrice == null)
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Stripe price was not found.",

                        Statuscode =
                            System.Net.HttpStatusCode.NotFound
                    };
                }

                var subscriptionService =
                    new SubscriptionService();

                Subscription oldSubscription =
                    await subscriptionService.GetAsync(
                        model.StripesubId);

                if (oldSubscription == null)
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Current Stripe subscription was not found.",

                        Statuscode =
                            System.Net.HttpStatusCode.NotFound
                    };
                }

                var options =
                    new SessionCreateOptions
                    {
                        LineItems =
                            new List<SessionLineItemOptions>
                            {
                                new SessionLineItemOptions
                                {
                                    Price =
                                        model.StripePriceId,

                                    Quantity =
                                        1
                                }
                            },

                        Customer =
                            model.StripeCusId,

                        AllowPromotionCodes =
                            false,

                        Mode =
                            "subscription",

                        SuccessUrl =
                            _configuration.GetValue<string>(
                                "PaymentUrl:PaymentSuccessURLWeb")
                            + "?session_id={CHECKOUT_SESSION_ID}",

                        CancelUrl =
                            _configuration.GetValue<string>(
                                "PaymentUrl:PaymentFailUrl"),

                        Metadata =
                            new Dictionary<string, string>
                            {
                                {
                                    "OldSubscriptionId",
                                    model.StripesubId
                                },
                                {
                                    "NewPlanId",
                                    model.PlanId.ToString()
                                },
                                {
                                    "ChangeType",
                                    "Downgrade"
                                }
                            }
                    };

                var sessionService =
                    new SessionService();

                Session session =
                    await sessionService.CreateAsync(
                        options);

                return new Response
                {
                    Statuscode =
                        System.Net.HttpStatusCode.OK,

                    Status =
                        Resources.SuccessMsg,

                    Message =
                        Resources.FollowUrl,

                    Data =
                        session.Url
                };
            }
            catch (StripeException ex)
            {
                _logger.LogError(
                    ex,
                    "Stripe error during downgrade.");

                return new Response
                {
                    Status =
                        Resources.FailureMsg,

                    Message =
                        ex.StripeError?.Message ??
                        ex.Message,

                    Statuscode =
                        System.Net.HttpStatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating downgrade Checkout session.");

                throw;
            }
        }

        // ============================================================
        // CANCEL SUBSCRIPTION
        // ============================================================

        [HttpPost]
        public async Task<Response>
            CancelSubscription(
                [FromForm] string subId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(
                    subId))
                {
                    return new Response
                    {
                        Status =
                            Resources.FailureMsg,

                        Message =
                            "Subscription ID is required.",

                        Statuscode =
                            System.Net.HttpStatusCode.BadRequest
                    };
                }

                return await CancelStripeSubscriptionInternal(
                    subId,
                    true);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error cancelling subscription {SubscriptionId}.",
                    subId);

                throw;
            }
        }

        // ============================================================
        // INTERNAL CANCEL
        // ============================================================

        [NonAction]
        private async Task<Response>
            CancelStripeSubscriptionInternal(
                string subId,
                bool updateDatabase)
        {
            var response =
                new Response();

            if (string.IsNullOrWhiteSpace(
                subId))
            {
                response.Status =
                    Resources.FailureMsg;

                response.Message =
                    "Subscription ID is required.";

                response.Statuscode =
                    System.Net.HttpStatusCode.BadRequest;

                return response;
            }

            var service =
                new SubscriptionService();

            Subscription subscription =
                await service.GetAsync(
                    subId);

            if (subscription == null)
            {
                response.Status =
                    Resources.FailureMsg;

                response.Message =
                    "Stripe subscription not found.";

                response.Statuscode =
                    System.Net.HttpStatusCode.NotFound;

                return response;
            }

            if (subscription.CancelAtPeriodEnd != true)
            {
                var options =
                    new SubscriptionUpdateOptions
                    {
                        CancelAtPeriodEnd =
                            true
                    };

                subscription =
                    await service.UpdateAsync(
                        subId,
                        options);
            }

            if (updateDatabase)
            {
                UserMembershipSubscriptions model =
                    new UserMembershipSubscriptions
                    {
                        StripeSubscriptionId =
                            subId,

                        StripeStatus =
                            "Cancelled",

                        CancelledOn =
                            DateTime.Now
                    };

                var result =
                    await UpdatePaymentTransactionInfo(
                        model);

                if (result != null)
                {
                    response.Status =
                        Resources.SuccessMsg;

                    response.Statuscode =
                        System.Net.HttpStatusCode.OK;

                    response.Message =
                        "Subscription cancelled successfully.";

                    return response;
                }

                response.Status =
                    Resources.FailureMsg;

                response.Statuscode =
                    System.Net.HttpStatusCode.NotFound;

                response.Message =
                    "Subscription was cancelled in Stripe, but the local record could not be updated.";

                return response;
            }

            response.Status =
                Resources.SuccessMsg;

            response.Statuscode =
                System.Net.HttpStatusCode.OK;

            response.Message =
                "Subscription cancelled successfully.";

            return response;
        }

        // ============================================================
        // CANCEL OLD SUBSCRIPTION AFTER SUCCESSFUL REPLACEMENT
        // ============================================================

        [NonAction]
        private async Task
            CancelStripeSubscriptionAfterReplacement(
                string oldSubscriptionId)
        {
            if (string.IsNullOrWhiteSpace(
                oldSubscriptionId))
            {
                return;
            }

            var service =
                new SubscriptionService();

            Subscription oldSubscription =
                await service.GetAsync(
                    oldSubscriptionId);

            if (oldSubscription == null)
            {
                _logger.LogWarning(
                    "Old subscription {SubscriptionId} was not found.",
                    oldSubscriptionId);

                return;
            }

            if (oldSubscription.Status ==
                "canceled")
            {
                _logger.LogInformation(
                    "Old subscription {SubscriptionId} is already cancelled.",
                    oldSubscriptionId);
            }
            else if (oldSubscription.CancelAtPeriodEnd != true)
            {
                var options =
                    new SubscriptionUpdateOptions
                    {
                        CancelAtPeriodEnd =
                            true
                    };

                await service.UpdateAsync(
                    oldSubscriptionId,
                    options);
            }

            UserMembershipSubscriptions oldModel =
                new UserMembershipSubscriptions
                {
                    StripeSubscriptionId =
                        oldSubscriptionId,

                    StripeStatus =
                        "Cancelled",

                    CancelledOn =
                        DateTime.Now
                };

            await UpdatePaymentTransactionInfo(
                oldModel);

            _logger.LogInformation(
                "Old subscription {SubscriptionId} cancelled after successful replacement.",
                oldSubscriptionId);
        }

        // ============================================================
        // CREATE CUSTOM PRICE
        // ============================================================

        [HttpPost]
        public async Task<Response>
            CreateCustomPriceSubscription(
                CustomPlanViewModel Model)
        {
            try
            {
                Response response =
                    new Response();

                if (Model == null)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        "Invalid custom plan request.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                var user =
                    await _Userservice.GetUserByID(
                        Model.UserId);

                if (user == null)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        Resources.UserNotFound;

                    response.Statuscode =
                        System.Net.HttpStatusCode.NotFound;

                    return response;
                }

                // ====================================================
                // Model.Price is DOUBLE.
                // Do not use string.IsNullOrWhiteSpace() or
                // decimal.TryParse() here.
                // ====================================================

                if (Model.Price <= 0)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        "Invalid custom plan price.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                // Stripe expects the amount in the smallest
                // currency unit (cents for USD).
                long price =
                    (long)Math.Round(
                        Model.Price * 100d,
                        MidpointRounding.AwayFromZero);

                if (price <= 0)
                {
                    response.Status =
                        Resources.FailureMsg;

                    response.Message =
                        "Custom plan price must be greater than zero.";

                    response.Statuscode =
                        System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                string interval =
                    Model.IsYearly
                        ? "year"
                        : "month";

                var options =
                    new PriceCreateOptions
                    {
                        UnitAmount =
                            price,

                        Currency =
                            "usd",

                        Recurring =
                            new PriceRecurringOptions
                            {
                                Interval =
                                    interval
                            },

                        Product =
                            _configuration.GetValue<string>(
                                "Stripe:CustomMembershipProductId")
                    };

                var service =
                    new PriceService();

                var createResponse =
                    await service.CreateAsync(
                        options);

                string body =
                    Resources.CustomMembershipHtml;

                body =
                    body.Replace(
                        "{URL}",
                        _configuration.GetValue<string>(
                            "PaymentUrl:CustomPlanRequestURL")
                        + user.Id);

                body =
                    body.Replace(
                        "{ImagePath}",
                        _configuration.GetValue<string>(
                            "PaymentUrl:CurrentDomain")
                        + "images/Kopke-brand-logo.png");

                var html =
                    body;

                var isSent =
                    _email.SendEmail(
                        user.Email,
                        Resources.CustomMembershipPlanEmailHeader,
                        html);

                if (!isSent)
                {
                    _logger.LogWarning(
                        "Custom plan created successfully, but email could not be sent to {Email}.",
                        user.Email);
                }

                response.Statuscode =
                    System.Net.HttpStatusCode.OK;

                response.Message =
                    "Link sent to user email.";

                response.Status =
                    Resources.SuccessMsg;

                response.Data =
                    createResponse;

                CustomZipcodesRequest
                    customZipcodesRequest =
                        new CustomZipcodesRequest
                        {
                            WebApp =
                                Model.WebApp,

                            MobileApp =
                                Model.MobileApp,

                            NumberOfCategories =
                                Model.NumberOfCategories,

                            NumberOfZipcodes =
                                Model.NumberOfZipcodes,

                            PriceMonthly =
                                Model.Price,

                            StripePriceYearly =
                                createResponse.Id,

                            StripePriceMonthly =
                                createResponse.Id,

                            PriceYearly =
                                Model.Price,

                            UserId =
                                Model.UserId
                        };

                await _Membership
                    .UpdateCustomZipcodeRequest(
                        customZipcodesRequest);

                return response;
            }
            catch (StripeException ex)
            {
                _logger.LogError(
                    ex,
                    "Stripe error creating custom price subscription.");

                return new Response
                {
                    Status =
                        Resources.FailureMsg,

                    Message =
                        ex.StripeError?.Message ??
                        ex.Message,

                    Statuscode =
                        System.Net.HttpStatusCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating custom price subscription.");

                throw;
            }
        }
    }
}



