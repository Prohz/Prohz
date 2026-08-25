﻿using KopkeHome_ModelLayer;
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

        public PaymentController(
            IConfiguration iConfig,
            ILogger<PaymentController> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = iConfig;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

            StripeConfiguration.ApiKey =
                _configuration.GetValue<string>("Stripe:SecretKey");
        }

        // ============================================================
        // UPGRADE SUBSCRIPTION
        // ============================================================
        [HttpPost]
        public async Task<IActionResult> UpgradeSubscription(
            [FromForm] string StripeSubscriptionId,
            [FromForm] string StripeCusId,
            [FromForm] string StripePriceId,
            [FromForm] string PlanId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(StripeSubscriptionId))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Stripe subscription ID is missing."
                    });
                }

                if (string.IsNullOrWhiteSpace(StripeCusId))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Stripe customer ID is missing."
                    });
                }

                if (string.IsNullOrWhiteSpace(StripePriceId))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Stripe price ID is missing."
                    });
                }

                if (string.IsNullOrWhiteSpace(PlanId))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Plan ID is missing."
                    });
                }

                // Save selected plan in session
                HttpContext.Session.Remove("PlanId");
                HttpContext.Session.SetString("PlanId", PlanId);

                // IMPORTANT:
                // Do NOT change UpgradeSubscriptionRequestModel.
                // Map the frontend parameter StripeSubscriptionId
                // to the existing model property StripesubId.
                UpgradeSubscriptionRequestModel model =
                    new UpgradeSubscriptionRequestModel();

                model.StripesubId = StripeSubscriptionId;
                model.StripePriceId = StripePriceId;
                model.PlanId = PlanId;
                model.StripeCusId = StripeCusId;

                string apiUrl =
                    _configuration.GetValue<string>("WebApi:API_URL");

                if (string.IsNullOrWhiteSpace(apiUrl))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Web API URL is not configured."
                    });
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress =
                        new Uri(apiUrl.TrimEnd('/') + "/Payment/");

                    var httpResponse =
                        await client.PostAsJsonAsync(
                            "UpgradeSubscription",
                            model);

                    string content =
                        await httpResponse.Content.ReadAsStringAsync();

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        _logger.LogError(
                            "UpgradeSubscription API failed. Status: {StatusCode}, Response: {Response}",
                            httpResponse.StatusCode,
                            content);

                        return Json(new
                        {
                            success = false,
                            message = GetApiErrorMessage(content)
                        });
                    }

                    if (string.IsNullOrWhiteSpace(content))
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Empty response received from payment API."
                        });
                    }

                    Response paymentResponse = null;

                    try
                    {
                        paymentResponse =
                            JsonConvert.DeserializeObject<Response>(content);
                    }
                    catch (Exception jsonEx)
                    {
                        _logger.LogError(
                            jsonEx,
                            "Unable to deserialize UpgradeSubscription API response: {Content}",
                            content);

                        return Json(new
                        {
                            success = false,
                            message = "Invalid payment API response."
                        });
                    }

                    if (paymentResponse == null ||
                        paymentResponse.Data == null)
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Stripe Checkout URL was not returned."
                        });
                    }

                    string checkoutUrl =
                        JsonConvert.SerializeObject(paymentResponse.Data);

                    checkoutUrl =
                        Regex.Replace(
                            checkoutUrl,
                            "^\"|\"$",
                            "");

                    if (string.IsNullOrWhiteSpace(checkoutUrl))
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Stripe Checkout URL is empty."
                        });
                    }

                    _logger.LogInformation(
                        "Stripe checkout session created successfully for PlanId: {PlanId}",
                        PlanId);

                    // IMPORTANT:
                    // Frontend expects res.data
                    return Json(new
                    {
                        success = true,
                        data = checkoutUrl
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error in PaymentController.UpgradeSubscription");

                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }


        // ============================================================
        // CANCEL SUBSCRIPTION
        // ============================================================
        [HttpGet]
        public async Task<IActionResult> CancelSubscription(string subId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(subId))
                {
                    return Json(0);
                }

                using (var request = new HttpClient())
                {
                    string apiUrl =
                        _configuration.GetValue<string>("WebApi:API_URL");

                    request.BaseAddress =
                        new Uri(apiUrl.TrimEnd('/') + "/Payment/");

                    var SendsubId =
                        new FormUrlEncodedContent(
                            new[]
                            {
                                new KeyValuePair<string, string>(
                                    "subId",
                                    subId)
                            });

                    var response =
                        await request.PostAsync(
                            "CancelSubscription",
                            SendsubId);

                    string result =
                        await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(1);
                    }

                    _logger.LogError(
                        "CancelSubscription API failed: {Result}",
                        result);

                    return Json(0);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error in PaymentController.CancelSubscription");

                return Json(0);
            }
        }


        // ============================================================
        // DOWNGRADE SUBSCRIPTION
        // ============================================================
        [HttpGet]
        public async Task<IActionResult> DowngradeSubscription(
            string subId,
            string CusId,
            string PriceID,
            string PlanId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(subId) ||
                    string.IsNullOrWhiteSpace(CusId) ||
                    string.IsNullOrWhiteSpace(PriceID) ||
                    string.IsNullOrWhiteSpace(PlanId))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Required subscription information is missing."
                    });
                }

                HttpContext.Session.Remove("PlanId");
                HttpContext.Session.SetString("PlanId", PlanId);

                DowngradeSubscriptionRequestModel model =
                    new DowngradeSubscriptionRequestModel();

                model.StripesubId = subId;
                model.StripePriceId = PriceID;
                model.PlanId = PlanId;
                model.StripeCusId = CusId;
                model.Email = HttpContext.Request.Cookies["Email"];
                model.Email = HttpContext.Request.Cookies["Email"];

                using (var client = new HttpClient())
                {
                    string apiUrl =
                        _configuration.GetValue<string>("WebApi:API_URL");

                    client.BaseAddress =
                        new Uri(apiUrl.TrimEnd('/') + "/Payment/");

                    var httpResponse =
                        await client.PostAsJsonAsync(
                            "DowngradeSubscription",
                            model);

                    string content =
                        await httpResponse.Content.ReadAsStringAsync();

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        _logger.LogError(
                            "DowngradeSubscription API failed. Status: {StatusCode}, Response: {Response}",
                            httpResponse.StatusCode,
                            content);

                        return Json(new
                        {
                            success = false,
                            message = GetApiErrorMessage(content)
                        });
                    }

                    var paymentResponse =
                        JsonConvert.DeserializeObject<Response>(content);

                    if (paymentResponse == null ||
                        paymentResponse.Data == null)
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Downgrade response did not contain a valid URL."
                        });
                    }

                    string url =
                        JsonConvert.SerializeObject(
                            paymentResponse.Data);

                    url =
                        Regex.Replace(
                            url,
                            "^\"|\"$",
                            "");

                    return Json(new
                    {
                        success = true,
                        data = url
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error in PaymentController.DowngradeSubscription");

                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }


        // ============================================================
        // HELPER
        // ============================================================
        private string GetApiErrorMessage(string content)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(content))
                {
                    var response =
                        JsonConvert.DeserializeObject<Response>(content);

                    if (response != null &&
                        response.Data != null)
                    {
                        return response.Data.ToString();
                    }

                    dynamic error =
                        JsonConvert.DeserializeObject<dynamic>(content);

                    if (error != null)
                    {
                        if (error.message != null)
                            return error.message.ToString();

                        if (error.Message != null)
                            return error.Message.ToString();
                    }
                }
            }
            catch
            {
                // Ignore JSON parsing errors and return raw response below.
            }

            return string.IsNullOrWhiteSpace(content)
                ? "Payment API returned an unknown error."
                : content;
        }
    }
}