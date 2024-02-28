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
                var options = new Stripe.Checkout.SessionCreateOptions
                {


                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            Price = model.StripePriceId,
                            Quantity = 1,

                        },
                        new SessionLineItemOptions
                        {
                            Price = "price_1OoaNqILheuScUW2dxLO3CsB", // Replace with the actual Price ID of your service fee in Stripe
                            Quantity = 1,
                        },
                    },
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

                model.PlanId = Datamodel.PlanId;

                return await AddPaymentTransactionDetails(model);



            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        [HttpPost]
        public async Task<Response> UpgradeSubscription(UpgradeSubscriptionRequestModel model)
        {
            Response response = new Response();
            // var s = await CancelSubscription(model.StripesubId);



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
                Customer = model.StripeCusId,
                AllowPromotionCodes = false,

                Mode = "subscription",

                SuccessUrl = _configuration.GetValue<string>("PaymentUrl:PaymentSuccessURLWeb") + "?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = _configuration.GetValue<string>("PaymentUrl:PaymentFailUrl"),

            };
            var service2 = new SessionService();

            Session session = await service2.CreateAsync(options);


            var cancelSub = await CancelSubscription(model.StripesubId);
            response.Statuscode = System.Net.HttpStatusCode.OK;
            response.Status = Resources.SuccessMsg;
            response.Data = session.Url;


            return response;


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

        [HttpPost]
        public async Task<Response> DowngradeSubscription(DowngradeSubscriptionRequestModel Model)
        {
            Response response = new Response();
            var SUB = new SubscriptionService();
            var res = SUB.Get(Model.StripesubId);
            var endsAt = res.CurrentPeriodEnd;

            //payment link generation.

            var options2 = new Stripe.Checkout.SessionCreateOptions
            {


                LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            Price = Model.StripePriceId,
                            Quantity = 1,

                        },
                    },
                Customer = Model.StripeCusId,
                AllowPromotionCodes = false,

                Mode = "subscription",

                ////Local url
                SuccessUrl = _configuration.GetValue<string>("PaymentUrl:PaymentSuccessURLWeb") + "?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = _configuration.GetValue<string>("PaymentUrl:PaymentFailUrl"),

            };
            var service2 = new SessionService();

            Session session = await service2.CreateAsync(options2);
            var cancelSub = await CancelSubscription(Model.StripesubId);
            response.Statuscode = System.Net.HttpStatusCode.OK;
            response.Status = Resources.SuccessMsg;
            response.Data = session.Url;
            return response;



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



