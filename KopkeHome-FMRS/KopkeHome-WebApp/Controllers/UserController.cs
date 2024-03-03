using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;
using KopkeHome_UtilityLayer;
using KopkeHome_UtilityLayer.Session;
using KopkeHome_WebApp.WebUtility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;


namespace KopkeHome_WebApp.Controllers
{
#nullable disable
    public class UserController : Controller
    {

        private readonly IConfiguration _configuration;
        private IWebHostEnvironment _Env;
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
      

        public UserController(IConfiguration iConfig, ILogger<UserController> logger, IWebHostEnvironment _environment, IHttpContextAccessor httpContextAccessor)    
        {
            _Env = _environment;
            _configuration = iConfig;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            
        }

        public IActionResult SignUp(string data)
        {
            try
            {
                UserViewModel model = new UserViewModel();
                var States = StateList();
                model.States = States.Result;
                ViewBag.homedata = data;
                return View();
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [HttpGet]
        public async Task<List<State>> StateList()
        {
            List<State> states = new List<State>();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");

                    //HTTP POST
                    var httpResponse = await client.GetAsync("GetStates");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var deserializedResultList = JsonConvert.DeserializeObject<List<State>>(content);


                        return deserializedResultList;
                    }
                    else
                    {
                        states.Add(new State
                        {
                            StateName = "No states found",
                            StateId = 0

                        });
                        return states;
                    }
                }



            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        [HttpGet]
        public async Task<List<GetZipCodesByCityNameViewModel>> GetzipCodeListbyCityName(string CityName, string StateName)
        {
            List<GetZipCodesByCityNameViewModel> cities = new List<GetZipCodesByCityNameViewModel>();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/membership/");
                    var ContentsToSend = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("CityName", CityName),
                        new KeyValuePair<string, string>("StateName", StateName)
                });
                    var httpResponse = await client.PostAsync("GetZipCodesByCityName", ContentsToSend);
                    //var httpResponse = await client.GetAsync("GetCitiesList");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        cities = JsonConvert.DeserializeObject<List<GetZipCodesByCityNameViewModel>>(content);



                    }

                }

                return cities;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        [HttpGet]
        public async Task<List<City>> GetCitiesList(int id)
        {
            List<City> cities = new List<City>();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                    var ContentsToSend = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("id", id.ToString())
                });
                    var httpResponse = await client.PostAsync("GetCitiesList", ContentsToSend);
                    //var httpResponse = await client.GetAsync("GetCitiesList");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        cities = JsonConvert.DeserializeObject<List<City>>(content);

                    }

                }

                return cities;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> HomeOwnerSignUp(HomeOwnerViewModel HomeOwnerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpContext.Response.Cookies.Append("RoleId", HomeOwnerViewModel.RoleId.ToString());
                    HttpContext.Response.Cookies.Append("Email", HomeOwnerViewModel.EmailHo);
                    SignIn signIn = new SignIn();
                    signIn.Email = HomeOwnerViewModel.EmailHo;
                    signIn.IsRememberme = false;
                    signIn.Password = HomeOwnerViewModel.ConfirmPasswordHo;

                    //sets creds
                    HttpContext.Session.Remove("CurrentUserCreds");
                    HttpContext.Session.Set<SignIn>("CurrentUserCreds", signIn);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");

                        //HTTP POST
                        var httpResponse = await client.PostAsJsonAsync<HomeOwnerViewModel>("BasicInfoHomeOwner", HomeOwnerViewModel);
                        var content = await httpResponse.Content.ReadAsStringAsync();
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            return Json(content);
                        }
                        else
                        {
                            return Json(content);
                        }
                    }

                }

                return RedirectToAction("BusinessProfile", "User");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                ChangePasswordModel changePasswordModel = new ChangePasswordModel();
                changePasswordModel.Email = model.Email;
                changePasswordModel.OldPassword = model.OldPassword;
                changePasswordModel.NewPassword = model.NewPassword;
                changePasswordModel.ConfirmNewPassword = model.ConfirmNewPassword;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                    var httpResponse = await client.PostAsJsonAsync<ChangePasswordModel>("ChangePassword", changePasswordModel);
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        return Json(content);
                    }
                    else
                    {
                        return Json(content);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(UserViewModel userViewModel)
        {
            try
            {
                // ModelState.Remove("MemberReferralId");
                if (ModelState.IsValid)
                {
                    HttpContext.Response.Cookies.Append("RoleId", userViewModel.RoleId.ToString());
                    HttpContext.Response.Cookies.Append("Email", userViewModel.Email);

                    SignIn signIn = new SignIn();
                    signIn.Email = userViewModel.Email;
                    signIn.IsRememberme = false;
                    signIn.Password = userViewModel.ConfirmPassword;

                    //sets creds
                    HttpContext.Session.Remove("CurrentUserCreds");
                    HttpContext.Session.Set<SignIn>("CurrentUserCreds", signIn);
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                        var httpResponse = await client.PostAsJsonAsync<UserViewModel>("BasicInfo", userViewModel);
                        var content = await httpResponse.Content.ReadAsStringAsync();
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            return Json(content);
                        }
                        else
                        {
                            return Json(content);
                        }
                    }

                }

                return RedirectToAction("signup", "User");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            ViewBag.Email = HttpContext.Request.Cookies["EmailRM"];
            ViewBag.Password = HttpContext.Request.Cookies["Password"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatusAsync(WorkStatusViewModel model)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                var httpResponse = await client.PostAsJsonAsync<WorkStatusViewModel>("UserWorkStatus", model);
                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var Users = JsonConvert.DeserializeObject<User>(content);
                    HttpContext.Session.Set<User>("CurrentUser", Users);
                    return Json(1);
                }
                else
                {
                    return Json(0);
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyOTPs(string Verificationcode, string Email)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    EmailVerficationModel VerficationModel = new EmailVerficationModel()
                    {
                        VerificationCode = Verificationcode,
                        Email = Email
                    };
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");

                        //HTTP POST
                        var postTask = await client.PostAsJsonAsync<EmailVerficationModel>("VerifyOTP", VerficationModel);



                        if (postTask.IsSuccessStatusCode)
                        {
                            return Json(HttpContext.Request.Cookies["RoleId"]);
                        }
                        else
                        {
                            //string InvalidVerificationCode = "InvalidVerificationCodes";
                            return Json(0);

                        }
                    }

                }
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GenerateLinkForgotPassword(string Email)
        {


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                var ContentsToSend = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("email", Email)
                });
                var httpResponse = await client.PostAsync("ForgotPassword", ContentsToSend);
                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {

                    var _response = JsonConvert.DeserializeObject<Response>(content);
                    if (_response.Data == null)
                    {
                        return Json(content);

                    }
                    string token = _response.Data.ToString();


                    var link = Url.Action("Changeforgotpassword", "User", new { token, email = Email }, Request.Scheme);

                    if (link != null)
                    {
                        var pathToFile = _Env.WebRootPath + Path.DirectorySeparatorChar.ToString() + "Templates"
                            + Path.DirectorySeparatorChar.ToString()
                            + "EmailTemplates"
                            + Path.DirectorySeparatorChar.ToString()
                            + "ForgotPasswordEmailTemplate.html";

                        var path = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + _httpContextAccessor.HttpContext.Request.PathBase;
                        var pathtoLogo = path + Url.Content("/images" + "/Kopke-brand-logo.png");

                        // var pathtoLogo = "https://photos.app.goo.gl/uTaFJ9amkBthJbxr7";
                        string body = string.Empty;
                        using (StreamReader reader = new StreamReader(pathToFile))
                        {
                            body = reader.ReadToEnd();
                        }
                        body = body.Replace("{URL}", link);
                        body = body.Replace("{ImagePath}", pathtoLogo);
                        var html = body;
                        using (var request = new HttpClient())
                        {
                            request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");

                            var SendMail = new FormUrlEncodedContent(new[]
                            {
                        new KeyValuePair<string, string>("html", html),
                        new KeyValuePair<string, string>("Email", Email)

                        });
                            var response = await request.PostAsync("ForgotPasswordSendMail", SendMail);
                            var result = await response.Content.ReadAsStringAsync();
                            if (response.IsSuccessStatusCode)
                            {
                                return Json(result);
                            }

                        }

                    }
                }

            }

            return Json(0);



        }

        public IActionResult forgotpassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Changeforgotpassword(string email, string token)
        {
            ForgotpasswordViewModel model = new ForgotpasswordViewModel();
            model.Email = email;
            model.Token = token;

            return View(model);
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeforgotpasswordOfUser(ForgotpasswordViewModel Model)
        {


            using (var request = new HttpClient())
            {
                request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");


                var postTask = await request.PostAsJsonAsync<ForgotpasswordViewModel>("ResetPassword", Model);
                var result = await postTask.Content.ReadAsStringAsync();
                if (postTask.IsSuccessStatusCode)
                {
                    return Json(result);
                }

            }



            return View();
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckContractorsOnZipcode(string zipcode)
        {


            using (var request = new HttpClient())
            {
                request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");
                var ContentsToSend = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("zipcode", zipcode)
                    });


                var httpResponse = await request.PostAsync("CheckContractorsOnZipcode", ContentsToSend);

                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {
                    var deserializedResultList = JsonConvert.DeserializeObject<Response>(content);
                    return Json(deserializedResultList.Data);
                }
                else
                {
                    return Json(0);
                }


            }
        }
        public IActionResult HomeOwner()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignIn signIn)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    //sets creds
                    // HttpContext.Session.Remove("CurrentUserCreds");
                    HttpContext.Session.Set<SignIn>("CurrentUserCreds", signIn);


                    HttpContext.Response.Cookies.Append("Email", signIn.Email);
                    if (signIn.IsRememberme)
                    {
                        CookieOptions option = new CookieOptions();
                        option.Expires = DateTime.Now.AddDays(30);
                        //HttpContext.Response.Cookies.Append("token", jwtToken, option);
                        //option.ExpireTimeSpan = TimeSpan.FromHours(24);

                        HttpContext.Response.Cookies.Append("EmailRM", signIn.Email, option);
                        HttpContext.Response.Cookies.Append("Password", signIn.Password, option);

                    }
                    else
                    {
                        HttpContext.Response.Cookies.Delete("EmailRM");
                        HttpContext.Response.Cookies.Delete("Password");
                    }
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Account/");

                        //HTTP POST
                        var httpResponse = await client.PostAsJsonAsync<SignIn>("SignIn", signIn);
                        var content = httpResponse.Content.ReadAsStringAsync().Result;
                        var deserialized = JsonConvert.DeserializeObject<Response>(content);
                        //if ok
                        if (deserialized?.Statuscode == HttpStatusCode.OK)
                        {

                            if (deserialized.Code == 8)
                            {
                                var dataemail = new FormUrlEncodedContent(new[]
                                    {
                                        new KeyValuePair<string, string>("email", signIn.Email)
                                    });
                                var Response = await client.PostAsync("GetUserByEmail", dataemail);
                                string resultContent = await Response.Content.ReadAsStringAsync();

                                if (httpResponse.IsSuccessStatusCode)
                                {
                                    HttpContext.Session.Remove("CurrentUser");

                                    var currentuser = JsonConvert.DeserializeObject<User>(resultContent);
                                    HttpContext.Session.Set<User>("CurrentUser", currentuser);


                                }
                                HttpContext.Response.Cookies.Append("accessToken", deserialized.Data.ToString());
                                return RedirectToAction("dashboard", "admin");
                            }


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
                            else if (deserialized?.Code == 7)
                            {
                                //User did't selected zipcodes and categories
                                //redirecting the user into seleczipcode and categories page.
                                return RedirectToAction("selectzipcodesandcategories", "Membership");
                            }
                            else
                            {
                                var dataemail = new FormUrlEncodedContent(new[]
                                    {
                                        new KeyValuePair<string, string>("email", signIn.Email)
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

                                HttpContext.Response.Cookies.Append("accessToken", deserialized.Data.ToString());
                                //if user is admin

                                return RedirectToAction("Index", "Dashboard");
                            }

                        }
                        else
                        {

                            ModelState.AddModelError("Password", deserialized.Message);
                        }
                        return View(signIn);


                    }



                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return RedirectToAction("signIn");
        }


        [HttpPost]
        public  string SaveImage([FromBody] ImageModel file)
        {
            ImageModel img=new ImageModel();
            //var Encryptedfilename = string.Empty;
            try
            {
                if (file != null)
                {
                    var filePath = Path.Combine(_Env.WebRootPath, "Uploads");
                    IFormFile fromFile;
                    using (var ms = new MemoryStream(file.ImageData))
                    {
                        fromFile = new FormFile(ms, 0, ms.Length, file.Name, file.FileName);
                        
                        if (file != null)
                        {
                            if (!Directory.Exists(filePath))
                            {
                                Directory.CreateDirectory(filePath);
                            }

                            string fileName = Path.GetFileName(fromFile.FileName);
                            //Encryptedfilename = Guid.NewGuid() + fileName;
                            using (FileStream stream = new FileStream(Path.Combine(filePath, file.Encryptedfilename), FileMode.Create))
                            {
                                fromFile.CopyTo(stream);

                            }
                        }
                        
                    }
                    return "Success";
                }
                else
                {
                    return "File not updated";
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
