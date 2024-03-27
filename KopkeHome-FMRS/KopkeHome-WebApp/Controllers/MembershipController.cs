using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.APIRequestModels;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.DataModel.PaymentAndSubscription;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.PaymentModels;
using KopkeHome_UtilityLayer;
using KopkeHome_UtilityLayer.Session;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace KopkeHome_WebApp.Controllers
{
#nullable disable
    public class MembershipController : Controller
    {
        private readonly ILogger<MembershipController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public MembershipController(IConfiguration iConfig, ILogger<MembershipController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = iConfig;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

            StripeConfiguration.ApiKey = _configuration.GetValue<string>("Stripe:SecretKey");

        }


        /// <summary>
        /// contracrors plan
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ContractorsPlan()
        {
            MembershipPlanFromStrip model = new MembershipPlanFromStrip();
            try
            {
                // Fetching the logged-in user
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                    var content = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string, string>("email", HttpContext.Request.Cookies["Email"])
            });
                    var httpResponse = await client.PostAsync("GetUserByEmail", content);
                    string resultContent = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        model.LoggedInUser = JsonConvert.DeserializeObject<User>(resultContent);
                        HttpContext.Session.Set<User>("CurrentUser", model.LoggedInUser);
                    }
                    else
                    {
                        model.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");
                    }
                }

                // Checking if the current user already has a subscription plan
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");
                    var contentToSend = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string, string>("email", HttpContext.Request.Cookies["Email"])
            });

                    var httpResponse = await client.PostAsync("CheckUserHaveSubscriptionOrNotByEmail", contentToSend);

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var content = await httpResponse.Content.ReadAsStringAsync();
                        var response = JsonConvert.DeserializeObject<Response>(content);

                        if (response.Statuscode == System.Net.HttpStatusCode.BadRequest)
                        {
                            return RedirectToAction("selectzipcodesandcategories");
                        }
                    }
                }
                ProhzReferral referral = null;
                // Fetching Referrals
                List<ProhzReferral> referrals = new List<ProhzReferral>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");

                    // Assuming you have the memberId stored in a variable named memberId
                    int memberId = model.LoggedInUser.Id; // Or wherever you get the memberId from

                    var content = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("Id", memberId.ToString())
                });
                    var response = await client.PostAsync("GetReferralsById", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string resultContent = await response.Content.ReadAsStringAsync();
                        referral = JsonConvert.DeserializeObject<ProhzReferral>(resultContent);
                    }
                }

                // Fetching MembershipPlans
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Membership/");
                    var httpResponse = await client.GetAsync("GetMembershipPlans");

                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var content = await httpResponse.Content.ReadAsStringAsync();

                        model.Plans = JsonConvert.DeserializeObject<List<MembershipPlanViewmodel>>(content);
                        foreach (var item in model.Plans)
                        {
                            if (item.Title == "Bronze" && item.RoleId == Constant.Contractor)
                            {
                                model.PriceMonthlyBronzeID = item.MonthlyStripePriceId;
                                model.PriceYearlyBronzeID = item.AnnuallyStripePriceId;
                            }
                            else if (item.Title == "Silver" && item.RoleId == Constant.Contractor)
                            {
                                // Adjusting Silver plan if referrals are not empty
                                if (referral != null)
                                {
                                    // Set Silver plan price to 0 or adjust it accordingly
                                    // Assuming here you have properties named MonthlyStripePriceId and AnnuallyStripePriceId to adjust
                                    model.PriceMonthlySilverID = _configuration.GetValue<string>("Stripe:FreeID"); // Adjust this according to your logic
                                    model.PriceMonthlySilver = 0;
                                    model.PriceYearlySilverID = _configuration.GetValue<string>("Stripe:FreeID"); // Adjust this according to your logic
                                    model.priceYearlySilver = 0;
                                } else
                                {
                                    model.PriceMonthlySilverID = item.MonthlyStripePriceId;
                                    model.PriceYearlySilverID = item.AnnuallyStripePriceId;
                                    model.PriceMonthlySilver = Convert.ToInt32(item.PricePerMonth);
                                    model.priceYearlySilver = Convert.ToInt32(item.PricePerYear);





                                }

                            }
                            else if (item.Title == "Gold" && item.RoleId == Constant.Contractor)
                            {
                                model.PriceMonthlyGoldID = item.MonthlyStripePriceId;
                                model.PriceYearlyGoldID = item.AnnuallyStripePriceId;
                            }


                        }


                    }
                }

                // Fetching Custom Plan details by UserId
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Membership/");
                    var contentToSend = new FormUrlEncodedContent(new[]
                    {
                new KeyValuePair<string, string>("UserId", model.LoggedInUser.Id.ToString())
            });

                    var customMemberResponse = await client.PostAsync("GetCustomPlanDetailsByUserId", contentToSend);
                    if (customMemberResponse.IsSuccessStatusCode)
                    {
                        var customMemberResponsecontent = await customMemberResponse.Content.ReadAsStringAsync();
                        var CstmPlan = JsonConvert.DeserializeObject<CustomZipcodesRequest>(customMemberResponsecontent);
                        if (CstmPlan != null && CstmPlan.IsPlanCreated)
                        {
                            model.CustomPlan = CstmPlan;
                        }
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public IActionResult PaymentCard(string priceId, string planId)
        {
            HttpContext.Session.SetString("PlanId", planId);
            HttpContext.Session.SetString("PriceId", priceId);

            HttpContext.Session.SetString("Email", HttpContext.Request.Cookies["Email"]);
            return Json(1);
        }
        public IActionResult PaymentForCustomPlan(string priceId, string planId, string userid)
        {
            HttpContext.Session.SetString("PlanId", planId);
            HttpContext.Session.SetString("PriceId", priceId);
            HttpContext.Session.SetString("UserId", userid.ToString());
            HttpContext.Session.SetString("Email", HttpContext.Request.Cookies["Email"]);
            return Json(1);
        }
        /// <summary>
        /// For custom memebership request plan
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CustomPlanGateway()
        {
            try
            {

                SubscribeToAPlanModel model = new SubscribeToAPlanModel();



                model.StripePriceId = HttpContext.Session.GetString("PriceId");
                model.UserID = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");
                    var httpResponse = await client.PostAsJsonAsync<SubscribeToAPlanModel>("SubscribeToAPlanCustom", model);
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var paymentResponse = JsonConvert.DeserializeObject<Response>(content);
                        string Url = JsonConvert.SerializeObject(paymentResponse.Data);
                        Url = Regex.Replace(Url, "^\"|\"$", "");
                        if (Url == "null")
                        {
                            return Json("Already subscribed");
                        }
                        // Url = Url.Trim('"');
                        return Redirect(Url);

                    }
                    else
                    {
                        return Json(content);
                    }
                }

            }
            catch (Exception ex)
            {
                throw;

            }
        }
        /// <summary>
        /// For making Payment
        /// </summary>
        /// <param name="priceId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Payment()
        {
            try
            {

                SubscribeToAPlanModel model = new SubscribeToAPlanModel();
                BillingModel userBilling = new();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", HttpContext.Request.Cookies["Email"])
                    });
                    var httpResponse = await client.PostAsync("GetUserByEmail", content);
                    string resultContent = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        userBilling.User = JsonConvert.DeserializeObject<User>(resultContent);
                    }

                }
                model.StripePriceId = HttpContext.Session.GetString("PriceId");
                model.UserID = userBilling.User.Id;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");
                    var httpResponse = await client.PostAsJsonAsync<SubscribeToAPlanModel>("SubscribeToAPlan", model);
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var paymentResponse = JsonConvert.DeserializeObject<Response>(content);
                        string Url = JsonConvert.SerializeObject(paymentResponse.Data);
                        Url = Regex.Replace(Url, "^\"|\"$", "");
                        // Url = Url.Trim('"');
                        return Redirect(Url);

                    }
                    else
                    {
                        return Json(content);
                    }
                }

            }
            catch (Exception ex)
            {
                throw;

            }
        }
        /// <summary>
        /// For Subscription Payment 
        /// </summary>
        /// <param name="billingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Payment(BillingModel billingDetails)
        {
            try
            {


                if (ModelState.IsValid)
                {
                    //Create token from Card details
                    var token = CreateToken(billingDetails);
                    if (token.Id != null)
                    {
                        double daysToAddInIterval = 30;
                        if (billingDetails.Interval != null)
                        {
                            if (billingDetails.Interval == "month")
                            {
                                daysToAddInIterval = 30;
                            }
                            else if (billingDetails.Interval == "year")
                            {
                                daysToAddInIterval = 365;
                            }
                        }

                        ///Create Customer on Stripe
                        var stripeCustomer = CreateCustomer(billingDetails.User/*, token.Id*/);

                        var priceId = HttpContext.Session.GetString("PriceId");

                        //Create Subscription on Stripe
                        var subs = CreateSubscription(stripeCustomer.Id, priceId, billingDetails.AutoRenewal, daysToAddInIterval);

                        //Pay Subscription's Invoice on Stripe
                        var service = new InvoiceService();
                        var result = service.Pay(subs.LatestInvoice.Id);
                        string status = result.Status;


                        if (status == "paid") //If Subscription paid successfully - Add User, Subscription and Payment Details into DB
                        {
                            //Add User
                            //User newUser = new User
                            //{
                            //    Name = billingDetails.Name,
                            //    Address = billingDetails.Address,
                            //    Password = billingDetails.Password,
                            //    Email = billingDetails.Email,
                            //    PhoneNumber = billingDetails.PhoneNumber,
                            //    CustomerId = stripeCustomer.Id,
                            //};

                            // int userId = _userRegisterService.InsertUser(newUser);

                            //if (userId > 0)
                            //{
                            //Add Subscription
                            //UserSubscription userSubscription = new UserSubscription
                            //{
                            //    UserId = userId,
                            //    CustomerId = stripeCustomer.Id,
                            //    StripeId = token.Id,
                            //    ProductId = billingDetails.ProductId,
                            //    SubscriptionStatus = subs.Status,
                            //    StripePrice = Convert.ToDecimal(billingDetails.PriceInCent / 100),
                            //    Quantity = 1,
                            //    TrialEndsAt = DateTime.Now.AddDays(daysToAddInIterval),
                            //    SubscriptionId = subs.Id
                            //};

                            // int subscriptionId = _userRegisterService.AddSubscription(userSubscription);

                            //Add Payment
                            //Payment payment = new Payment
                            //{
                            //    UserId = userId,
                            //    CustomerId = stripeCustomer.Id,
                            //    CardNumber = billingDetails.CardNumber.Substring(billingDetails.CardNumber.Length - 4),
                            //    Amount = Convert.ToDecimal(billingDetails.PriceInCent / 100),
                            //    Status = 1,
                            //    TransactionId = result.Id,
                            //};
                            //int paymnetId = _userRegisterService.AddPayment(payment);
                            // billingDetails.UserId = userId;
                            // int billingId = _userRegisterService.AddBilling(billingDetails);
                            // }
                            HttpContext.Session.Clear();
                            return Json(new { status = "success" });
                        }
                        else if (status == "void")
                        {
                            return Json(new { status = "void" });
                        }
                        else if (status == "uncollectible")
                        {
                            return Json(new { status = "uncollectible" });
                        }
                        else if (status == "open")
                        {
                            return Json(new { status = "open" });
                        }
                        else if (status == "draft")
                        {
                            return Json(new { status = "draft" });
                        }
                    }
                    else if (token.Id == null)
                    {
                        return Json(new { status = "null" });
                    }
                }
                // _log.Info("Account Controller's Payment Method Ended..");
            }
            catch (StripeException ex)
            {
                // _log.Info(ex.Message);

                switch (ex.StripeError.Code)
                {
                    case "incorrect_number":
                        return Json(new { status = "incorrectnumber" });
                    case "card_error":
                        return Json(new { status = "carderror" });
                    case "incorrect_cvc":
                        return Json(new { status = "incorrectcvc" });
                    case "expired_card":
                        return Json(new { status = "expiredcard" });
                    case "insufficient_funds":
                        return Json(new { status = "insufficientfunds" });
                    case "invalid_expiry_year":
                        return Json(new { status = "invalidexpiryyear" });
                    case "invalid_expiry_month":
                        return Json(new { status = "invalidexpirymonth" });
                    case "no_account":
                        return Json(new { status = "noaccount" });
                    case "account_invalid":
                        return Json(new { status = "accountinvalid" });
                    case "debit_not_authorized":
                        return Json(new { status = "debitnotauthorized" });
                    case "balance_insufficient":
                        return Json(new { status = "balanceinsufficient" });
                    default:
                        return Json(new { status = ex.Message });
                }
            }
            return Ok();
        }


        /// <summary>
        /// for token
        /// </summary>
        /// <param name="billingDetails"></param>
        /// <returns></returns>
        public Token CreateToken(BillingModel billingDetails)
        {
            var tokenOptions = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = billingDetails.CardNumber,

                    ExpMonth = long.Parse(billingDetails.ExpiryMonth),
                    ExpYear = long.Parse(billingDetails.ExpiryYear),
                    Cvc = billingDetails.CVV,
                },
            };

            var tokenService = new TokenService();
            Token token = tokenService.Create(tokenOptions);

            return token;
        }


        /// <summary>
        /// create subscription
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="priceId"></param>
        /// <param name="isAutoRenewal"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public Subscription CreateSubscription(string customerId, string priceId, bool isAutoRenewal = false, double days = 30)
        {
            var subscriptionOptions = new SubscriptionCreateOptions
            {
                Customer = customerId,
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Price = priceId

                    },

                },

                PaymentBehavior = "default_incomplete",

            };

            subscriptionOptions.CollectionMethod = "charge_automatically";

            //If Auto Renewal is checked
            //if (isAutoRenewal)
            //{
            //    subscriptionOptions.CollectionMethod = "charge_automatically";
            //}
            //else
            //{
            //    subscriptionOptions.CancelAt = DateTime.Now.AddDays(days);
            //}

            subscriptionOptions.AddExpand("latest_invoice.payment_intent");
            var PaymentIntent = new PaymentIntentCreateOptions
            {
                Currency = "US",
                Customer = customerId

            };
            PaymentIntentService service = new PaymentIntentService();
            var PaymentIntent2 = service.Create(PaymentIntent);
            var serviceSubscription = new SubscriptionService();
            var stripeSubscription = serviceSubscription.Create(subscriptionOptions);
            return stripeSubscription;
        }

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
        /// homeowner plan
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> HomeOwnerPlan()
        {
            MembershipPlanFromStrip model = new MembershipPlanFromStrip();
            try
            {
                //model.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");
                if (model.LoggedInUser == null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                        var content = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("email", HttpContext.Request.Cookies["Email"])
                   });
                        var httpResponse = await client.PostAsync("GetUserByEmail", content);
                        string resultContent = await httpResponse.Content.ReadAsStringAsync();
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            var deserializedResultList = JsonConvert.DeserializeObject<User>(resultContent);
                            HttpContext.Session.Set<User>("CurrentUser", deserializedResultList);
                            model.LoggedInUser = deserializedResultList;
                        }
                    }
                }
                //checking if current user already have subscription plan or not
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");
                    var ContentsToSend = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("email",  HttpContext.Request.Cookies["Email"])
                        });


                    var httpResponse = await client.PostAsync("CheckUserHaveSubscriptionOrNotByEmail", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var response = JsonConvert.DeserializeObject<Response>(content);

                        if (response.Statuscode == System.Net.HttpStatusCode.BadRequest)
                        {
                            return RedirectToAction("signin", "user");
                            // return RedirectToAction("selectzipcodesandcategories");
                        }
                    }
                }

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Membership/");

                    //HTTP POST
                    var httpResponse = await client.GetAsync("GetMembershipPlans");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        model.Plans = JsonConvert.DeserializeObject<List<MembershipPlanViewmodel>>(content);
                        foreach (var item in model.Plans)
                        {

                            if (item.Title == "Gold" && item.RoleId == Constant.HomeOwner)
                            {
                                model.PriceMonthlyGoldID = item.MonthlyStripePriceId;
                                model.PriceYearlyGoldID = item.AnnuallyStripePriceId;
                            }
                            else if (item.Title == "Silver" && item.RoleId == Constant.HomeOwner)
                            {
                                model.PriceMonthlySilverID = item.MonthlyStripePriceId;
                                model.PriceYearlySilverID = item.AnnuallyStripePriceId;
                            }

                        }

                        return View(model);
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return View();
        }


        /// <summary>
        /// payment success for custom plan
        /// </summary>
        /// <param name="session_id"></param>
        /// <returns></returns>
        public async Task<IActionResult> PaymentSuccessCustomPlan(string session_id)
        {
            PaymentSuccessViewModel vm = new PaymentSuccessViewModel();

            PaymentSuccessAPIModel model = new PaymentSuccessAPIModel();

            UserMembershipSubscriptions UserSubmodel = new UserMembershipSubscriptions();

            try
            {

                model.SessionId = session_id;
                model.PlanId = Convert.ToInt32(HttpContext.Session.GetString("PlanId"));
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");
                    var httpResponse = await client.PostAsJsonAsync<PaymentSuccessAPIModel>("PaymentSuccess", model);
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        UserSubmodel = JsonConvert.DeserializeObject<UserMembershipSubscriptions>(content);

                        vm.DocUrl = UserSubmodel.InvoiceUrl;

                    }
                    else
                    {
                        return Json(content);
                    }
                }
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("Id", UserSubmodel.UserId.ToString())
                   });
                    var httpResponse = await client.PostAsync("GetUserById", content);
                    string resultContent = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var deserializedResultList = JsonConvert.DeserializeObject<User>(resultContent);
                        HttpContext.Session.Set<User>("CurrentUser", deserializedResultList);
                        HttpContext.Response.Cookies.Append("Email", deserializedResultList.Email);
                        vm.LoggedInUser = deserializedResultList;
                    }
                }
                return View("PaymentSuccess", vm);
            }

            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// payment success
        /// </summary>
        /// <param name="session_id"></param>
        /// <returns></returns>
        public async Task<IActionResult> PaymentSuccess(string session_id)
        {
            PaymentSuccessViewModel vm = new PaymentSuccessViewModel();


            vm.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");
            if (vm.LoggedInUser == null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", HttpContext.Request.Cookies["Email"])
                   });
                    var httpResponse = await client.PostAsync("GetUserByEmail", content);
                    string resultContent = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var deserializedResultList = JsonConvert.DeserializeObject<User>(resultContent);
                        HttpContext.Session.Set<User>("CurrentUser", deserializedResultList);
                        vm.LoggedInUser = deserializedResultList;
                    }
                }
            }

            PaymentSuccessAPIModel model = new PaymentSuccessAPIModel();

            //  UserMembershipSubscriptions model = new UserMembershipSubscriptions();

            try
            {

                model.SessionId = session_id;
                model.PlanId = Convert.ToInt32(HttpContext.Session.GetString("PlanId"));
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");
                    var httpResponse = await client.PostAsJsonAsync<PaymentSuccessAPIModel>("PaymentSuccess", model);
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        var paymentResponse = JsonConvert.DeserializeObject<UserMembershipSubscriptions>(content);
                        vm.DocUrl = paymentResponse.InvoiceUrl;
                        return View(vm);
                    }
                    else
                    {
                        return Json(content);
                    }
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }



        /// <summary>
        /// save custom zipcode request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveCustomZipcodesRequest(CustomZipcodesRequest model)
        {
            using (var request = new HttpClient())
            {
                var s = HttpContext.Session.Get<User>("CurrentUser");
                model.UserId = s.Id;
                request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/membership/");

                model.StripePriceMonthly = "";
                model.StripePriceYearly = "";
                var postTask = await request.PostAsJsonAsync<CustomZipcodesRequest>("SaveCustomZipcodeRequest", model);
                var result = await postTask.Content.ReadAsStringAsync();
                if (postTask.IsSuccessStatusCode)
                {
                    var deserializedResultList = JsonConvert.DeserializeObject<Response>(result);
                    if (deserializedResultList.Statuscode == HttpStatusCode.BadRequest)
                    {
                        return Json(0);
                    }
                    return Json(1);
                }
                return Json(2);
            }

        }

        /// <summary>
        /// selectzipcodesandcategories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> selectzipcodesandcategories()
        {
            //string tok = HttpContext.Request.Cookies["accessToken"];
            //var res = JwtTokenHandler.GetUserRole(tok);


            ZipcodesAndCategoriesViewModel model = new ZipcodesAndCategoriesViewModel();
            try
            {
                model.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");
                if (model.LoggedInUser == null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                        var content = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("email", HttpContext.Request.Cookies["Email"])
                   });
                        var httpResponse = await client.PostAsync("GetUserByEmail", content);
                        string resultContent = await httpResponse.Content.ReadAsStringAsync();
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            var deserializedResultList = JsonConvert.DeserializeObject<User>(resultContent);
                            HttpContext.Session.Set<User>("CurrentUser", deserializedResultList);
                            model.LoggedInUser = deserializedResultList;
                        }
                    }
                }

                using (var client = new HttpClient())
                {
                    string email = HttpContext.Session.GetString("Email");
                    if (email == null)
                    {
                        email = HttpContext.Request.Cookies["Email"];
                    }

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                    var ContentsToSend = new FormUrlEncodedContent(new[]
                      {
                        new KeyValuePair<string, string>("Email",email)
                        });
                    //HTTP POST
                    var UserResponse = await client.PostAsync("GetUserByEmail", ContentsToSend);
                    var Usercontent = await UserResponse.Content.ReadAsStringAsync();
                    var User = JsonConvert.DeserializeObject<User>(Usercontent);
                    if (User.RoleId == Constant.HomeOwner)
                    {
                        return RedirectToAction("GoToDashBoard");
                        //SINCE THIS USER IS TYPE HOME OWNER WE WE WONT NEED ANY ZIPCODES AND CATEGORIES
                        //THEN CALL API TO LOGIN INTO DASHBOARD USING SESSION STORED USER EMAIL AND PASSWORD.
                    }

                    var httpResponse = await client.PostAsync("GetZipcodesStatesAndCategories", ContentsToSend);
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        model = JsonConvert.DeserializeObject<ZipcodesAndCategoriesViewModel>(content);
                        model.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");
                    }

                }


                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// select zipcode and categories
        /// </summary>
        /// <param name="Zipcodes"></param>
        /// <param name="categories"></param>
        /// <param name="PlanId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> selectzipcodesandcategories(string[] Zipcodes, string[] categories, int PlanId)
        {

            //step-1 check zipcodes exist or not using email.
            MembershipZipcodesAndCategoriesRequestModel model = new MembershipZipcodesAndCategoriesRequestModel();
            var user = HttpContext.Session.Get<User>("CurrentUser");
            model.Zipcodes = Zipcodes;
            model.Categories = categories;
            model.UserId = user.Id;
            model.PlanId = PlanId;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Membership/");

                //HTTP POST


                var httpResponse = await client.PostAsJsonAsync<MembershipZipcodesAndCategoriesRequestModel>("SaveMembershipZipcodesAndCategories", model);
                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    return Json(1);
                }
                else
                {
                    return Json("Something went wrong.");
                }
            }

        }


        /// <summary>
        /// custom membership plan
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CustomMembershipPlan(int Id)
        {
            CustomPlanWebViewModel model = new CustomPlanWebViewModel();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");

                var ContentsToSend = new FormUrlEncodedContent(new[]
            {
                        new KeyValuePair<string, string>("Id", Id.ToString())
                });
                var httpResponse = await client.PostAsync("GetUserById", ContentsToSend);

                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<User>(content);

                    model.LoggedInUser = result;

                }
            }


            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                var ContentsToSend = new FormUrlEncodedContent(new[]
            {
                        new KeyValuePair<string, string>("Id", Id.ToString())
                });
                var httpResponse = await client.PostAsync("GetCustomReqByUserId", ContentsToSend);

                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {

                    var result = JsonConvert.DeserializeObject<CustomZipcodesRequest>(content);

                    model.Descrption = result.Descrption;
                    model.NumberOfCategories = result.NumberOfCategories;
                    model.Price = Convert.ToInt32(result.PriceYearly);
                    model.NumberOfZipcodes = result.NumberOfZipcodes;
                    model.StripePriceMonthly = result.StripePriceMonthly;
                    model.StripePriceYearly = result.StripePriceYearly;
                    model.MobileApp = result.MobileApp;
                    model.WebApp = result.WebApp;
                    model.UserId = result.UserId;
                    model.IsYearly = result.IsYearly;
                }

            }
            return View(model);
        }

        /// <summary>
        /// for go to dashboard
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GoToDashBoard()
        {

            var creds = HttpContext.Session.Get<SignIn>("CurrentUserCreds");

            if (creds != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");

                    //HTTP POST
                    var httpResponse = await client.PostAsJsonAsync<SignIn>("SignIn", creds);
                    var content = httpResponse.Content.ReadAsStringAsync().Result;
                    var deserialized = JsonConvert.DeserializeObject<Response>(content);
                    //if ok
                    if (deserialized?.Statuscode == HttpStatusCode.OK)
                    {
                        //if check businessprofiles with roleid
                        if (deserialized.Code == Constant.Contractor)
                        {
                            return RedirectToAction("ContractorProfile", "BusinessProfile");
                        }
                        else if (deserialized?.Code == Constant.SubContractor || deserialized?.Code == Constant.IndependentContractor)
                        {

                            return RedirectToAction("OtherContractorsProfile", "BusinessProfile");
                        }
                        else if (deserialized?.Code == 5)
                        {//contractors

                            return RedirectToAction("ContractorsPlan", "Membership");
                        }
                        else if (deserialized?.Code == 6)
                        {
                            //homeowner
                            return RedirectToAction("HomeOwnerPlan", "Membership");
                        }
                        else
                        {
                            var dataemail = new FormUrlEncodedContent(new[]
                                {
                                        new KeyValuePair<string, string>("email", creds.Email)
                                    });
                            var Response = await client.PostAsync("GetUserByEmail", dataemail);
                            string resultContent = await Response.Content.ReadAsStringAsync();

                            if (httpResponse.IsSuccessStatusCode)
                            {
                                HttpContext.Session.Remove("CurrentUser");
                                var currentuser = JsonConvert.DeserializeObject<User>(resultContent);
                                HttpContext.Session.Set<User>("CurrentUser", currentuser);
                                if (currentuser.IsDocumentsVerified == false && currentuser.RoleId != Constant.HomeOwner)
                                {
                                    return RedirectToAction("DocumentsNotVerified", "Home");
                                }

                            }

                            // HttpContext.Response.Cookies.Delete("accessToken");

                            HttpContext.Response.Cookies.Append("accessToken", deserialized.Data.ToString());

                            return RedirectToAction("Index", "Dashboard");
                        }

                    }

                }

            }

            return RedirectToAction("signin", "user");
        }


        [HttpPost]
        public async Task<ActionResult> Create()
        {
            //var domain = Configuration["AppSettings:WebBaseUrl"];
            //var priceOptions = new PriceListOptions
            //{
            //    Product = Request.Form["lookup_key"]
            //};
            //string teamId = _protector.Unprotect(Request.Form["Team_key"]);
            //var metatData = new Dictionary<string, string>
            //    {
            //        { "TeamId", teamId }
            //    };
            //var customerDetails = await Payment.GetCustomerDetails(Convert.ToInt32(teamId)).ConfigureAwait(false);
            //var teamDetails = await TeamBAL.GetByTeamId(Convert.ToInt32(teamId)).ConfigureAwait(false);
            //var priceService = new PriceService();
            //StripeList<Price> prices = priceService.List(priceOptions);

            //var options = new Stripe.Checkout.SessionCreateOptions
            //{
            //    LineItems = new List<SessionLineItemOptions>
            //        {
            //            new SessionLineItemOptions
            //            {
            //                Price = prices.Data[0].Id,
            //                Quantity = 1,
            //            },
            //        },
            //    SubscriptionData = new SessionSubscriptionDataOptions
            //    {
            //        Metadata = metatData,
            //        Description = teamDetails.Data.TeamName,

            //    },
            //    Metadata = metatData,
            //    Customer = customerDetails.CustomerId,
            //    AllowPromotionCodes = true,
            //    Mode = "subscription",
            //    SuccessUrl = domain + "payment/success?session_id={CHECKOUT_SESSION_ID}",
            //    CancelUrl = domain + "payment/cancel",
            //};
            //var service = new Stripe.Checkout.SessionService();
            //Stripe.Checkout.Session session = service.Create(options);
            //Response.Headers.Add("Location", session.Url);
            //return new StatusCodeResult(303);
            return View();
        }
    }


}
