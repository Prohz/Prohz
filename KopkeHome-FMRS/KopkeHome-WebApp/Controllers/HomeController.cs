using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;
using KopkeHome_UtilityLayer.Session;
using KopkeHome_WebApp.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System.Diagnostics;

namespace KopkeHome_WebApp.Controllers
{
#nullable disable

    // [Authorize(Roles = "Contractor")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment Environment;


        private readonly IConfiguration _configuration;

        private readonly IStringLocalizer<HomeController> _localizer;
        
        public HomeController(IWebHostEnvironment _environment, ILogger<HomeController> logger, IConfiguration iConfig,
            IStringLocalizer<HomeController> localizer)
        {
            Environment = _environment;
            _logger = logger;

            _configuration = iConfig;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var res = _localizer["Welcome"];
            //KopkeHome-WebApp.Resources.KopkeHome_WebApp.Controllers.HomeController
            return View();
        }

        public async Task<IActionResult> PromoVideos()
        {

            List<PromoVideos> vid = new List<PromoVideos>();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    //HTTP POST
                    var httpResponse = await client.GetAsync("GetAllPromoVideos");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        vid = JsonConvert.DeserializeObject<List<PromoVideos>>(content);

                        var singlke = vid.FirstOrDefault();
                        return Json(singlke);
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


        public async Task<IActionResult> ProhzLegalFiles()
        {

            List<ProhzLegalFiles> vid = new List<ProhzLegalFiles>();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    //HTTP POST
                    var httpResponse = await client.GetAsync("GetAllProhzLegalFiles");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        vid = JsonConvert.DeserializeObject<List<ProhzLegalFiles>>(content);

                      //  var singlke = vid.FirstOrDefault();
                        return Json(vid);
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

        public IActionResult ProhzHistory()
        {//done
            return View();
        }
        public IActionResult ContactUs()
        {//done
            return View();
        }
        public IActionResult Sponsors()
        {//done
            return View();
        }
        public IActionResult ValueProposition()
        {//done
            return View();
        }
        public IActionResult MissionStatement()
        {//done
            return View();
        }
        public IActionResult CoreValues()
        {//done
            return View();
        }
        public IActionResult Definitions()
        {//D
            return View();
        }
        public IActionResult HowDoesItWork()
        {//DONE
            return View();
        }
        public async Task<IActionResult> FAQ()
        {

            List<FAQ> FAQ = new List<FAQ>();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    //HTTP POST
                    var httpResponse = await client.GetAsync("GetAllFAQ");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        FAQ = JsonConvert.DeserializeObject<List<FAQ>>(content);


                        return View(FAQ);
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
        [HttpPost]
        public IActionResult SetAppLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }

        public IActionResult SetCulture(string id = "en")
        {
            string culture = id;
            Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
           );

            ViewData["Message"] = "Culture set to " + culture;

            return View("Privacy");
        }
        public IActionResult DocumentsNotVerified()
        {
            BaseViewModel model = new BaseViewModel();
            model.LoggedInUser = HttpContext.Session.Get<User>("CurrentUser");

            return View(model);
        }
        public IActionResult Privacy()
        {


            return View();
        }

        public async Task<IActionResult> TermsAndConditions()
        {

           

            try
            {

                return View();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
           
        }
        public IActionResult UnAuthorize()
        {


            return Json("UnAuthorize");
        }
        public IActionResult Paymentfailed()
        {
            //

            return View("PaymentFaild");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}