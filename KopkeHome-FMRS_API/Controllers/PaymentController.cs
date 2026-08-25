





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

        public PaymentController(IMembership Membership, IEmailService email, IConfiguration configuration, IPaymentService service, ILogger<PaymentController> logger, IAccount Userservice)
        {
            _Membership = Membership;
            _service = service;
            _logger = logger;
            _Userservice = Userservice;
            _configuration = configuration;
            _email = email;
            StripeConfiguration.ApiKey = _configuration.GetValue<string>("Stripe:SecretKey");
        }

        [HttpPost]
        public async Task<UserMembershipSubscriptions> AddPaymentTransactionDetails(UserMembershipSubscriptions model)
        {
            try
            {



                return await _service.AddPaymentTransactionInfo(model);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }

        }
        [HttpPost]
        public async Task<UserMembershipSubscriptions> UpdatePaymentTransactionInfo(UserMembershipSubscriptions model)
        {
            try
            {



                return await _service.UpdatePaymentTransactionInfo(model);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }

        }
        [HttpPost]
        public async Task<UserMembershipSubscriptions> GetSubscriptionDetailByUserId([FromForm] string UserId)
        {
            try
            {
                return await _service.GetSubscriptionsInfoByUserId(Convert.ToInt32(UserId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        [HttpGet]
        public async Task<MembershipPlanViewmodelApp> GetSubscriptionDetailByUserIdApp(string UserId)
        {
            try
            {
                return await _service.GetSubscriptionsInfoByUserIdApp(Convert.ToInt32(UserId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        [HttpPost]
        public async Task<Response> CheckUserHaveSubscriptionOrNotByEmail([FromForm] string Email)
        {
            try
            {
                Response httpResponse = new Response();

                httpResponse.Data = false;
                var user = _Userservice.GetUserByEmail(Email);
                if (user != null)
                {
                    var plans = await GetSubscriptionDetailByUserId(user.Result.Id.ToString());

                    if (plans != null)
                    {
                        httpResponse.Message = Resources.SubscriptionMsg;
                        httpResponse.Data = true;
                        httpResponse.Statuscode = System.Net.HttpStatusCode.BadRequest;
                    }
                    else
                    {
                        httpResponse.Message = Resources.IsSubscription;

                        httpResponse.Statuscode = System.Net.HttpStatusCode.NotFound;
                    }
                }
                else
                {
                    httpResponse.Message = Resources.UserNotFound;

                    httpResponse.Statuscode = System.Net.HttpStatusCode.NotFound;
                }

                return httpResponse;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// subscribe to a custom plan
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<Response> SubscribeToAPlanCustom(SubscribeToAPlanModel model)
        {
            try
            {
                Response Response = new Response();

                BillingModel userBilling = new();


                var user = await _Userservice.GetUserByID(model.UserID);
                if (user == null)
                {
                    Response.Status = Resources.FailureMsg;
                    Response.Message = Resources.RegisterYourself;
                    Response.Statuscode = System.Net.HttpStatusCode.NotFound;
                    return Response;
                }
                else
                {
                    var plans = await GetSubscriptionDetailByUserId(user.Id.ToString());

                    if (plans != null)
                    {
                        if (plans.PlanId==13)
                        {
                            Response.Message = Resources.SubscriptionMsg;
                            Response.Status = Resources.isSubscribed;
                            Response.Statuscode = System.Net.HttpStatusCode.BadRequest;
                            return Response;
                        }
                        
                    }
                }

                userBilling.User = user;

                //  User's Billing Details
                userBilling.BillingName = userBilling.User.FirstName + " " + userBilling.User.LastName;
                userBilling.BillingEmail = userBilling.User.Email;
                userBilling.BillingAddress = userBilling.User.BusinessAddress;
                userBilling.BillingPhoneNumber = userBilling.User.PhoneNumber;




                var service = new PriceService();
                Price price = service.Get(model.StripePriceId);
                userBilling.Interval = price.Recurring.Interval;
                userBilling.PriceInCent = price.UnitAmount;
                userBilling.PriceInDollar = Convert.ToDecimal(price.UnitAmountDecimal / 100).ToString("0.00");
                userBilling.Currency = price.Currency;
                userBilling.ProductId = price.ProductId;


                //creating token step-1
                //  var token = CreateToken(userBilling);
                var stripeCustomer = CreateCustomer(userBilling.User);
                var priceService = new PriceService();
                var CurrentDomain = _configuration.GetValue<string>("Stripe:SecretKey");
                
                var options = new Stripe.Checkout.SessionCreateOptions
                {


                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            Price = model.StripePriceId,
                            Quantity = 1,

                        },
                    },
                    Customer = stripeCustomer.Id,
                    AllowPromotionCodes = false,

                    Mode = "subscription",

                    ////Local url
                    SuccessUrl = _configuration.GetValue<string>("PaymentUrl:PaymentSuccessCustomPlanURLWeb") + "?session_id={CHECKOUT_SESSION_ID}",
                    CancelUrl = _configuration.GetValue<string>("PaymentUrl:PaymentFailUrl"),
                    //var link = Url.Action("PaymentSuccess", "Membership", new {  session_id = "{CHECKOUT_SESSION_ID}" }, Request.Scheme);

                };
                var service2 = new SessionService();

                Session session = await service2.CreateAsync(options);
                Response.Statuscode = System.Net.HttpStatusCode.OK;
                Response.Message = Resources.FollowUrl;
                Response.Data = session.Url;
                return Response;
                //return View(userBilling);
            }
            catch (Exception ex)
            {
                throw;

            }
        }
        /// <summary>
        /// This method subscribe a plan.
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="priceId"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<Response> SubscribeToAPlan(SubscribeToAPlanModel model)
        {
            try
            {
                Response Response = new Response();

                BillingModel userBilling = new();


                var user = await _Userservice.GetUserByID(model.UserID);
                if (user == null)
                {
                    Response.Status = Resources.FailureMsg;
                    Response.Message = Resources.RegisterYourself;
                    Response.Statuscode = System.Net.HttpStatusCode.NotFound;
                    return Response;
                }
                else
                {
                    var plans = await GetSubscriptionDetailByUserId(user.Id.ToString());

                    if (plans != null)
                    {
                        Response.Message = Resources.SubscriptionMsg;
                        Response.Status = Resources.isSubscribed;
                        Response.Statuscode = System.Net.HttpStatusCode.BadRequest;
                        return Response;
                    }
                }

                userBilling.User = user;

                //  User's Billing Details
                userBilling.BillingName = userBilling.User.FirstName + " " + userBilling.User.LastName;
                userBilling.BillingEmail = userBilling.User.Email;
                userBilling.BillingAddress = userBilling.User.BusinessAddress;
                userBilling.BillingPhoneNumber = userBilling.User.PhoneNumber;




                var service = new PriceService();
                Price price = service.Get(model.StripePriceId);
                userBilling.Interval = price.Recurring.Interval;
                userBilling.PriceInCent = price.UnitAmount;
                userBilling.PriceInDollar = Convert.ToDecimal(price.UnitAmountDecimal / 100).ToString("0.00");
                userBilling.Currency = price.Currency;
                userBilling.ProductId = price.ProductId;


                //creating token step-1
                //  var token = CreateToken(userBilling);
                var stripeCustomer = CreateCustomer(userBilling.User);
                var priceService = new PriceService();
                var CurrentDomain = _configuration.GetValue<string>("Stripe:SecretKey");
                var stripePriceId = _configuration.GetValue<string>("Stripe:PriceId");

                System.Diagnostics.Debug.WriteLine($"Plan: {model.StripePriceId}");
                System.Diagnostics.Debug.WriteLine($"Verification: {stripePriceId}");

                Console.WriteLine($"Plan: {model.StripePriceId}");
                Console.WriteLine($"Verification: {stripePriceId}");

                // Build the line items list, starting with the plan's own price.
                var lineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = model.StripePriceId,
                        Quantity = 1,
                    },
                };

                // Only add the $99 verification/service-fee line item when:
                //   1. A fee price is actually configured, and
                //   2. The selected plan is NOT a free plan (UnitAmount > 0), and
                //   3. The fee price is not the same Stripe Price as the plan itself
                //      (guards against the "duplicate recurring price" Stripe error).
                bool isFreePlan = price.UnitAmount is null or 0;

                if (!isFreePlan
                    && !string.IsNullOrWhiteSpace(stripePriceId)
                    && !string.Equals(stripePriceId, model.StripePriceId, StringComparison.OrdinalIgnoreCase))
                {
                    lineItems.Add(new SessionLineItemOptions
                    {
                        Price = stripePriceId, // Replace with the actual Price ID of your service fee in Stripe
                        Quantity = 1,
                    });
                }
                else
                {
                    _logger.LogInformation($"Skipping verification fee line item for plan price {model.StripePriceId} (isFreePlan={isFreePlan}).");
                }

                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    LineItems = lineItems,
                    Customer = stripeCustomer.Id,
                    AllowPromotionCodes = false,

                    Mode = "subscription",

                    ////Local url
                    SuccessUrl = _configuration.GetValue<string>("PaymentUrl:PaymentSuccessURLWeb") + "?session_id={CHECKOUT_SESSION_ID}",
                    CancelUrl = _configuration.GetValue<string>("PaymentUrl:PaymentFailUrl"),
                    //var link = Url.Action("PaymentSuccess", "Membership", new {  session_id = "{CHECKOUT_SESSION_ID}" }, Request.Scheme);

                };
                var service2 = new SessionService();

                Session session = await service2.CreateAsync(options);
                Response.Statuscode = System.Net.HttpStatusCode.OK;
                Response.Message = Resources.FollowUrl;
                Response.Data = session.Url;
                return Response;
                //return View(userBilling);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }


        [HttpPost]
        public async Task<Response> SubscribeToFreePlan(FreeMembershipRequest model)
        {
            try
            {
                Response response = new Response();

                var user = await _Userservice.GetUserByID(model.UserId);

                if (user == null)
                {
                    response.Status = Resources.FailureMsg;
                    response.Message = Resources.RegisterYourself;
                    response.Statuscode = System.Net.HttpStatusCode.NotFound;

                    return response;
                }

                // Check whether user already has a membership
                var existingSubscription =
                    await GetSubscriptionDetailByUserId(user.Id.ToString());

                if (existingSubscription != null)
                {
                    response.Status = Resources.isSubscribed;
                    response.Message = Resources.SubscriptionMsg;
                    response.Statuscode = System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                // Get membership plans
                var membershipPlans = await _Membership.GetMembershipPlans();

                var membershipPlan = membershipPlans
                    .FirstOrDefault(x => x.Id == model.PlanId);

                if (membershipPlan == null)
                {
                    response.Status = Resources.FailureMsg;
                    response.Message = "Membership plan not found.";
                    response.Statuscode = System.Net.HttpStatusCode.NotFound;

                    return response;
                }

                // IMPORTANT:
                // Only allow this endpoint for a FREE plan.
                if (membershipPlan.PricePerYear != 0)
                {
                    response.Status = Resources.FailureMsg;
                    response.Message = "This is not a free membership plan.";
                    response.Statuscode = System.Net.HttpStatusCode.BadRequest;

                    return response;
                }

                // Create free membership record
                UserMembershipSubscriptions subscription =
                    new UserMembershipSubscriptions();

                subscription.PlanId = membershipPlan.Id;
                subscription.Email = user.Email;

                subscription.PaymentStatus = "Paid";
                subscription.StripeStatus = "complete";

                subscription.StripeSubscriptionId = null;
                subscription.StripeCustomerID = null;
                subscription.StripePriceId = null;

                subscription.PeriodStartDate = DateTime.UtcNow;
                subscription.PeriodEndDate = DateTime.UtcNow.AddYears(1);

                var result =
                    await AddPaymentTransactionDetails(subscription);

                if (result != null)
                {
                    response.Status = Resources.SuccessMsg;
                    response.Message = "Free membership activated successfully.";
                    response.Statuscode = System.Net.HttpStatusCode.OK;
                    response.Data = result;

                    return response;
                }

                response.Status = Resources.FailureMsg;
                response.Message = "Unable to create membership.";
                response.Statuscode = System.Net.HttpStatusCode.InternalServerError;

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [NonAction]
        public Customer CreateCustomer(User _User)
        {
            AddressOptions address = new AddressOptions
            {
                State = _User.State,
                Country = "US",
                City = _User.City,
                PostalCode = _User.ZipCode,
                Line1 = _User.BusinessAddress,
                Line2 = _User.BusinessAddress
            };
            ShippingOptions ShippingOptions = new ShippingOptions
            {

                Address = address,
                Name = _User.FirstName + " " + _User.LastName,
                Phone = _User.PhoneNumber,

            };

            //craete a customer
            var customerCreateOptions = new CustomerCreateOptions
            {
                Name = _User.FirstName + " " + _User.LastName,
                Email = _User.Email,
                Address = address,
                // Source = sourceid,
                Shipping = ShippingOptions,


            };

            var service = new CustomerService();
            var stripeCustomer = service.Create(customerCreateOptions);
            return stripeCustomer;
        }


        /// <summary>
        /// Gets subscription details by stripe customer Id
        /// </summary>
        /// <param name="StripeCustomerId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserMembershipSubscriptions> GetCustomerByStripCustomerId([FromForm] string StripeCustomerId)
        {
            try
            {
                return await _service.GetSubscriptionByStripCustomerId(StripeCustomerId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<UserMembershipSubscriptions> PaymentSuccess(PaymentSuccessAPIModel Datamodel)
        {
            UserMembershipSubscriptions model = new UserMembershipSubscriptions();
            string InvoiceNumber = string.Empty;
            try
            {

                // StripeConfiguration.ApiKey = _config["StripeConfigurationApiKey"];
                var sessionService = new SessionService();
                Session session = await sessionService.GetAsync(Datamodel.SessionId);

                var customerService = new CustomerService();
                Customer customer = await customerService.GetAsync(session.CustomerId);
                //if (session.StripeResponse.StatusCode)
                //{
                //}
                /// Get Stripe Response From Session
                string StripeStatus = session.Status;
                string StripeSubscriptionId = session.SubscriptionId;
                string PaymentStatus = session.PaymentStatus;
                string StripeCustomerId = session.CustomerId;
                string Email = customer.Email;

                var options = new InvoiceListOptions
                {
                    Subscription = session.SubscriptionId

                };


                var service = new InvoiceService();
                StripeList<Invoice> invoices = await service.ListAsync(
                  options);

                var subscriptionService = new SubscriptionService();
                var subscriptionResult = await subscriptionService.GetAsync(StripeSubscriptionId);
                /// Get Setripe invoices details from stripe invoice service


                DateTime PeriodStartDate = subscriptionResult.CurrentPeriodStart;
                DateTime PeriodEndDate = subscriptionResult.CurrentPeriodEnd;
                string priceid = subscriptionResult.Items.Data[0].Price.Id;
                model.PaymentStatus = PaymentStatus;
                model.StripeStatus = StripeStatus;
                //model.PlanId = planid;
                model.StripeSubscriptionId = StripeSubscriptionId;
                model.InvoiceNumber = InvoiceNumber;
                model.PeriodEndDate = PeriodEndDate;
                model.PeriodStartDate = PeriodStartDate;
                model.StripeCustomerID = StripeCustomerId;


                model.StripePriceId = priceid;
                model.InvoiceNumber = invoices.Data[0].Number;
                model.InvoiceUrl = invoices.Data[0].InvoicePdf;
                model.Email = Email;


                // Map Stripe Price ID to Membership Plan
                var membershipPlans = await _Membership.GetMembershipPlans();

                var membershipPlan = membershipPlans
                    .FirstOrDefault(x =>
                        x.AnnuallyStripePriceId == priceid ||
                        x.MonthlyStripePriceId == priceid);


                if (membershipPlan != null)
                {
                    model.PlanId = membershipPlan.Id;
                }
                else
                {
                    model.PlanId = 0;
                }


                return await AddPaymentTransactionDetails(model);



            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        // [HttpPost]
        // public async Task<Response> UpgradeSubscription(UpgradeSubscriptionRequestModel model)
        // {
        //     Response response = new Response();
        //     // var s = await CancelSubscription(model.StripesubId);



        //     var options = new Stripe.Checkout.SessionCreateOptions
        //     {


        //         LineItems = new List<SessionLineItemOptions>
        //             {
        //                 new SessionLineItemOptions
        //                 {
        //                     Price = model.StripePriceId,
        //                     Quantity = 1,

        //                 },
        //             },
        //         Customer = model.StripeCusId,
        //         AllowPromotionCodes = false,

        //         Mode = "subscription",

        //         SuccessUrl = _configuration.GetValue<string>("PaymentUrl:PaymentSuccessURLWeb") + "?session_id={CHECKOUT_SESSION_ID}",
        //         CancelUrl = _configuration.GetValue<string>("PaymentUrl:PaymentFailUrl"),

        //     };
        //     var service2 = new SessionService();

        //     Session session = await service2.CreateAsync(options);


        //     var cancelSub = await CancelSubscription(model.StripesubId);
        //     response.Statuscode = System.Net.HttpStatusCode.OK;
        //     response.Status = Resources.SuccessMsg;
        //     response.Data = session.Url;


        //     return response;


        // }

        [HttpPost]
        public async Task<Response> UpgradeSubscription(
            UpgradeSubscriptionRequestModel model)
        {
            try
            {
                Response response = new Response();

                if (string.IsNullOrWhiteSpace(model.StripesubId))
                {
                    response.Status = Resources.FailureMsg;
                    response.Message = "Stripe subscription ID is required.";
                    response.Statuscode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                if (string.IsNullOrWhiteSpace(model.StripePriceId))
                {
                    response.Status = Resources.FailureMsg;
                    response.Message = "Stripe price ID is required.";
                    response.Statuscode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var subscriptionService = new SubscriptionService();

                // Get the existing subscription.
                var subscription =
                    await subscriptionService.GetAsync(model.StripesubId);

                if (subscription == null)
                {
                    response.Status = Resources.FailureMsg;
                    response.Message = "Stripe subscription not found.";
                    response.Statuscode = System.Net.HttpStatusCode.NotFound;
                    return response;
                }

                if (subscription.Items == null ||
                    subscription.Items.Data == null ||
                    !subscription.Items.Data.Any())
                {
                    response.Status = Resources.FailureMsg;
                    response.Message = "Subscription does not contain a subscription item.";
                    response.Statuscode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                var currentItem = subscription.Items.Data[0];

                // Make sure the requested plan is actually different.
                if (string.Equals(
                        currentItem.Price.Id,
                        model.StripePriceId,
                        StringComparison.OrdinalIgnoreCase))
                {
                    response.Status = Resources.FailureMsg;
                    response.Message = "The selected plan is already active.";
                    response.Statuscode = System.Net.HttpStatusCode.BadRequest;
                    return response;
                }

                // Remove any pending cancellation.
                // This is important if the user previously clicked Cancel.
                var updateOptions = new SubscriptionUpdateOptions
                {
                    CancelAtPeriodEnd = false,

                    Items = new List<SubscriptionItemOptions>
                    {
                        new SubscriptionItemOptions
                        {
                            Id = currentItem.Id,
                            Price = model.StripePriceId
                        }
                    },

                    // Upgrade takes effect immediately.
                    // Stripe calculates the prorated amount.
                    ProrationBehavior = "always_invoice"
                };

                var updatedSubscription =
                    await subscriptionService.UpdateAsync(
                        model.StripesubId,
                        updateOptions);

                if (updatedSubscription == null)
                {
                    response.Status = Resources.FailureMsg;
                    response.Message = "Unable to update Stripe subscription.";
                    response.Statuscode =
                        System.Net.HttpStatusCode.InternalServerError;

                    return response;
                }

                // Update membership information in our database.
                var membership = new UserMembershipSubscriptions
                {
                    StripeSubscriptionId = updatedSubscription.Id,
                    StripeCustomerID = updatedSubscription.CustomerId,
                    StripePriceId = model.StripePriceId,
                    StripeStatus = updatedSubscription.Status,
                    PaymentStatus = "Paid",
                    PlanId = Convert.ToInt32(model.PlanId)
                };

                var dbResult =
                    await UpdatePaymentTransactionInfo(membership);

                response.Status = Resources.SuccessMsg;
                response.Message = "Subscription upgraded successfully.";
                response.Statuscode = System.Net.HttpStatusCode.OK;

                // No Checkout URL is required anymore.
                response.Data = new
                {
                    SubscriptionId = updatedSubscription.Id,
                    PriceId = model.StripePriceId,
                    PlanId = model.PlanId,
                    Status = updatedSubscription.Status,
                    CurrentPeriodStart = updatedSubscription.CurrentPeriodStart,
                    CurrentPeriodEnd = updatedSubscription.CurrentPeriodEnd
                };

                return response;
            }
            catch (StripeException ex)
            {
                _logger.LogError(ex, "Stripe error while upgrading subscription.");

                Response response = new Response
                {
                    Status = Resources.FailureMsg,
                    Message = ex.StripeError?.Message ?? ex.Message,
                    Statuscode = System.Net.HttpStatusCode.BadRequest
                };

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while upgrading subscription.");

                Response response = new Response
                {
                    Status = Resources.FailureMsg,
                    Message = "Unable to upgrade subscription.",
                    Statuscode = System.Net.HttpStatusCode.InternalServerError
                };

                return response;
            }
        }

        [HttpPost]
        public async Task<Response> CancelSubscription([FromForm] string subId)
        {
            try
            {
                Response response = new Response();
                UserMembershipSubscriptions model = new UserMembershipSubscriptions();
                var service = new SubscriptionService();
                Subscription subscription = await service.GetAsync(subId);

                var items = new List<SubscriptionItemOptions> {
                        new SubscriptionItemOptions {
                            Id = subscription.Items.Data[0].Id,

                        },
                                };

                var options = new SubscriptionUpdateOptions
                {
                    CancelAtPeriodEnd = true,
                    // ProrationBehavior = "always_invoice",
                    Items = items,
                };
                subscription.CancelAtPeriodEnd = true;
                subscription = await service.UpdateAsync(subId, options);

                model.StripeSubscriptionId = subId;
                model.StripeStatus = "Cancelled";
                model.CancelledOn = DateTime.Now;

                var SSoptions = new SubscriptionScheduleListOptions
                {
                    Limit = 10,
                    Customer = subscription.CustomerId
                };
                var service2 = new SubscriptionScheduleService();
                StripeList<SubscriptionSchedule> subscriptionSchedules = service2.List(SSoptions);
                if (subscriptionSchedules.Data.Any())
                {
                    //foreach (var item in subscriptionSchedules.Data)
                    //{
                    //    if (item.Status != "canceled")
                    //    {
                    //        var SubSchdservice = new SubscriptionScheduleService();
                    //        SubSchdservice.Cancel(
                    //          item.Id);
                    //    }
                    //}
                }

                var result = await UpdatePaymentTransactionInfo(model);
                if (result != null)
                {
                    response.Status = Resources.SuccessMsg;
                    response.Statuscode = System.Net.HttpStatusCode.OK;
                    return response;
                }
                response.Status = Resources.FailureMsg;
                response.Statuscode = System.Net.HttpStatusCode.NotFound;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }

        // [HttpPost]
        // public async Task<Response> DowngradeSubscription(DowngradeSubscriptionRequestModel Model)
        // {
        //     Response response = new Response();
        //     var SUB = new SubscriptionService();
        //     var res = SUB.Get(Model.StripesubId);
        //     var endsAt = res.CurrentPeriodEnd;

        //     //payment link generation.

        //     var options2 = new Stripe.Checkout.SessionCreateOptions
        //     {


        //         LineItems = new List<SessionLineItemOptions>
        //             {
        //                 new SessionLineItemOptions
        //                 {
        //                     Price = Model.StripePriceId,
        //                     Quantity = 1,

        //                 },
        //             },
        //         Customer = Model.StripeCusId,
        //         AllowPromotionCodes = false,

        //         Mode = "subscription",

        //         ////Local url
        //         SuccessUrl = _configuration.GetValue<string>("PaymentUrl:PaymentSuccessURLWeb") + "?session_id={CHECKOUT_SESSION_ID}",
        //         CancelUrl = _configuration.GetValue<string>("PaymentUrl:PaymentFailUrl"),

        //     };
        //     var service2 = new SessionService();

        //     Session session = await service2.CreateAsync(options2);
        //     var cancelSub = await CancelSubscription(Model.StripesubId);
        //     response.Statuscode = System.Net.HttpStatusCode.OK;
        //     response.Status = Resources.SuccessMsg;
        //     response.Data = session.Url;
        //     return response;



        // }


    [HttpPost]
    public async Task<Response> DowngradeSubscription(
        DowngradeSubscriptionRequestModel model)
    {
        try
        {
            Response response = new Response();

            if (string.IsNullOrWhiteSpace(model.StripesubId))
            {
                response.Status = Resources.FailureMsg;
                response.Message = "Stripe subscription ID is required.";
                response.Statuscode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }

            if (string.IsNullOrWhiteSpace(model.StripePriceId))
            {
                response.Status = Resources.FailureMsg;
                response.Message = "Stripe price ID is required.";
                response.Statuscode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }

            var subscriptionService = new SubscriptionService();

            var subscription =
                await subscriptionService.GetAsync(model.StripesubId);

            if (subscription == null)
            {
                response.Status = Resources.FailureMsg;
                response.Message = "Stripe subscription not found.";
                response.Statuscode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            if (subscription.Items == null ||
                subscription.Items.Data == null ||
                !subscription.Items.Data.Any())
            {
                response.Status = Resources.FailureMsg;
                response.Message = "Subscription does not contain a subscription item.";
                response.Statuscode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }

            var currentItem = subscription.Items.Data[0];

            if (string.Equals(
                    currentItem.Price.Id,
                    model.StripePriceId,
                    StringComparison.OrdinalIgnoreCase))
            {
                response.Status = Resources.FailureMsg;
                response.Message = "The selected plan is already active.";
                response.Statuscode = System.Net.HttpStatusCode.BadRequest;
                return response;
            }

            /*
            * DOWNGRADE:
            *
            * Do NOT change the current subscription immediately.
            *
            * Keep the current plan until CurrentPeriodEnd.
            *
            * At CurrentPeriodEnd Stripe switches the subscription
            * to the requested lower-price plan.
            */

            var scheduleService = new SubscriptionScheduleService();

            SubscriptionSchedule schedule;

            // Check whether the subscription already has a schedule.
            if (!string.IsNullOrWhiteSpace(subscription.ScheduleId))
            {
                schedule =
                    await scheduleService.GetAsync(subscription.ScheduleId);
            }
            else
            {
                var scheduleOptions =
                    new SubscriptionScheduleCreateOptions
                    {
                        FromSubscription = subscription.Id
                    };

                schedule =
                    await scheduleService.CreateAsync(scheduleOptions);
            }

            var phase = new SubscriptionSchedulePhaseOptions
            {
                StartDate = subscription.CurrentPeriodStart,
                EndDate = subscription.CurrentPeriodEnd,

                Items = new List<SubscriptionSchedulePhaseItemOptions>
                {
                    new SubscriptionSchedulePhaseItemOptions
                    {
                        Price = currentItem.Price.Id,
                        Quantity = currentItem.Quantity ?? 1
                    }
                }
            };

            var downgradePhase = new SubscriptionSchedulePhaseOptions
            {
                StartDate = subscription.CurrentPeriodEnd,

                Items = new List<SubscriptionSchedulePhaseItemOptions>
                {
                    new SubscriptionSchedulePhaseItemOptions
                    {
                        Price = model.StripePriceId,
                        Quantity = 1
                    }
                }
            };

            var scheduleUpdateOptions =
                new SubscriptionScheduleUpdateOptions
                {
                    EndBehavior = "release",

                    Phases = new List<SubscriptionSchedulePhaseOptions>
                    {
                        phase,
                        downgradePhase
                    }
                };

            var updatedSchedule =
                await scheduleService.UpdateAsync(
                    schedule.Id,
                    scheduleUpdateOptions);

            if (updatedSchedule == null)
            {
                response.Status = Resources.FailureMsg;
                response.Message = "Unable to schedule subscription downgrade.";
                response.Statuscode =
                    System.Net.HttpStatusCode.InternalServerError;

                return response;
            }

            response.Status = Resources.SuccessMsg;
            response.Message =
                "Downgrade scheduled successfully. Your current plan will remain active until the end of the current billing period.";

            response.Statuscode = System.Net.HttpStatusCode.OK;

            response.Data = new
            {
                SubscriptionId = subscription.Id,
                ScheduleId = updatedSchedule.Id,
                CurrentPlanPriceId = currentItem.Price.Id,
                NewPlanPriceId = model.StripePriceId,
                EffectiveDate = subscription.CurrentPeriodEnd,
                PlanId = model.PlanId
            };

            return response;
        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Stripe error while scheduling downgrade.");

            return new Response
            {
                Status = Resources.FailureMsg,
                Message = ex.StripeError?.Message ?? ex.Message,
                Statuscode = System.Net.HttpStatusCode.BadRequest
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while scheduling subscription downgrade.");

            return new Response
            {
                Status = Resources.FailureMsg,
                Message = "Unable to schedule subscription downgrade.",
                Statuscode = System.Net.HttpStatusCode.InternalServerError
            };
        }
    }

        [HttpPost]
        public async Task<Response> CreateCustomPriceSubscription(CustomPlanViewModel Model)
        {
            Response response = new Response();

            //payment link generation.
            // StripeConfiguration.ApiKey = "sk_test_4eC39HqLyjWDarjtT1zdp7dc";
            var user = await _Userservice.GetUserByID(Model.UserId);
            if (user != null)
            {
                long price = (long)Convert.ToDouble(Model.Price) * 100;

                string Interval = "month";
                if (Model.IsYearly)
                {
                    //  price = Model.PriceYearly;
                    Interval = "year";
                }
                else
                {
                    // price = Model.PriceMonthly;
                }
                var options = new PriceCreateOptions
                {
                    UnitAmount = price,
                    Currency = "usd",
                    Recurring = new PriceRecurringOptions
                    {
                        Interval = Interval,
                    },
                    Product = _configuration.GetValue<string>("Stripe:CustomMembershipProductId"),
                };
                var service = new PriceService();
                var Createresponse = service.Create(options);
                string body = string.Empty;
                body = Resources.CustomMembershipHtml;

                body = body.Replace("{URL}", _configuration.GetValue<string>("PaymentUrl:CustomPlanRequestURL") + user.Id);
                body = body.Replace("{ImagePath}", _configuration.GetValue<string>("PaymentUrl:CurrentDomain") + "images/Kopke-brand-logo.png");
                var html = body;
                var IsSent = _email.SendEmail(user.Email, Resources.CustomMembershipPlanEmailHeader, html);
                response.Statuscode = System.Net.HttpStatusCode.OK;
                response.Message = "Link sent to user email.";
                response.Status = Resources.SuccessMsg;
                response.Data = Createresponse;



                CustomZipcodesRequest customZipcodesRequest = new CustomZipcodesRequest();

                customZipcodesRequest.WebApp = Model.WebApp;
                customZipcodesRequest.MobileApp = Model.MobileApp;
                customZipcodesRequest.NumberOfCategories = Model.NumberOfCategories;
                customZipcodesRequest.NumberOfZipcodes = Model.NumberOfZipcodes;
                customZipcodesRequest.PriceMonthly = Model.Price;
                customZipcodesRequest.StripePriceYearly = Createresponse.Id;
                customZipcodesRequest.StripePriceMonthly = Createresponse.Id;
                customZipcodesRequest.PriceYearly = Model.Price;
                customZipcodesRequest.UserId = Model.UserId;


                await _Membership.UpdateCustomZipcodeRequest(customZipcodesRequest);
            }

            return response;

        }

    }

}