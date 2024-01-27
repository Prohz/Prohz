
using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.AdminViewModels;
using KopkeHome_ModelLayer.ViewModels.PaymentModels;
using KopkeHome_UtilityLayer.Session;
using KopkeHome_WebApp.WebUtility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KopkeHome_WebApp.Controllers
{

#nullable disable

    [ValidateSession]
    [CustomAuthorize("Admin")]
    public class AdminController : Controller
    {
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment _Env;
        private readonly ILogger<UserController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User _CurrentUser;

        public AdminController(IConfiguration iConfig, ILogger<UserController> logger, IWebHostEnvironment _environment, IHttpContextAccessor httpContextAccessor)
        {
            _Env = _environment;
            _configuration = iConfig;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

        }
        /// <summary>
        /// This is the dashbaord index page of admin.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Dashboard()
        {
            _CurrentUser = HttpContext.Session.Get<User>("CurrentUser");
            HttpContext.Response.Cookies.Append("CurrentUserName", _CurrentUser.FirstName + " " + _CurrentUser.LastName);
            HttpContext.Response.Cookies.Append("CurrentUserProfilePic", _CurrentUser.ProfilePicture);
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                //HTTP POST
                var httpResponse = await client.GetAsync("GetDashboardStatus");

                var content = await httpResponse.Content.ReadAsStringAsync();
                if (httpResponse.IsSuccessStatusCode)
                {

                    var deserializedResultList = JsonConvert.DeserializeObject<AdminDashboardViewModel>(content);

                    //details = deserializedResultList;
                    return View(deserializedResultList);
                }

            }
            return View();
        }


        /// <summary>
        /// Get Al lProhzSales Person
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> SalesAssociate()
        {
            List<ProhzSalesAssciates> details = new List<ProhzSalesAssciates>();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    //HTTP POST
                    var httpResponse = await client.GetAsync("GetAllProhzSalesPerson");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var deserializedResultList = JsonConvert.DeserializeObject<List<ProhzSalesAssciates>>(content);

                        details = deserializedResultList;
                        return View(details);
                    }

                }

                return View(details);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        /// <summary>
        /// Gets the custom membership plans
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CustomMembershipPlanRequests()
        {
            List<CustomMembershipPlanRequestViewModel> details = new List<CustomMembershipPlanRequestViewModel>();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    //HTTP POST
                    var httpResponse = await client.GetAsync("CustomMembershipPlanRequestsList");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var deserializedResultList = JsonConvert.DeserializeObject<List<CustomMembershipPlanRequestViewModel>>(content);

                        details = deserializedResultList.OrderBy(x => x.CustomZipcodesRequest.IsPlanCreated == false).ToList();
                        return View(details);
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
        /// <summary>
        /// gets FAQ by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetFAQById(int Id)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    var ContentsToSend = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("Id", Id.ToString())
                });
                    var httpResponse = await client.PostAsync("GetFAQById", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var result = JsonConvert.DeserializeObject<FAQ>(content);

                        return Json(result);
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
        /// Gets category by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetCategoryById(int Id)
        {

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    var ContentsToSend = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("Id", Id.ToString())
                });
                    var httpResponse = await client.PostAsync("GetCategoryById", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var result = JsonConvert.DeserializeObject<Categories>(content);

                        return Json(result);
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
        /// gets Custom membership request by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetCustomReqById(int Id)
        {

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    var ContentsToSend = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("Id", Id.ToString())
                });
                    var httpResponse = await client.PostAsync("GetCustomReqById", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var result = JsonConvert.DeserializeObject<CustomZipcodesRequest>(content);


                        return Json(result);
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
        /// Deletes FAQ
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteFAQ(int Id)
        {



            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    var ContentsToSend = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("Id", Id.ToString())
                });
                    var httpResponse = await client.PostAsync("DeleteFAQ", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var result = JsonConvert.DeserializeObject<Response>(content);
                        if (result.Statuscode == System.Net.HttpStatusCode.OK)
                        {
                            return Json(1);
                        }
                        else
                        {
                            return Json(0);
                        }

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
        /// Deletes category
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteCategory(int Id)
        {



            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    var ContentsToSend = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("Id", Id.ToString())
                });
                    var httpResponse = await client.PostAsync("DeleteCategory", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var result = JsonConvert.DeserializeObject<Response>(content);
                        if (result.Statuscode == System.Net.HttpStatusCode.OK)
                        {
                            return Json(1);
                        }
                        else
                        {
                            return Json(0);
                        }

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
        /// deletes promo video
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeletePromoVideo(int Id)
        {



            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    var ContentsToSend = new FormUrlEncodedContent(new[]
                {
                        new KeyValuePair<string, string>("Id", Id.ToString())
                });
                    var httpResponse = await client.PostAsync("DeletePromoVideo", ContentsToSend);

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var result = JsonConvert.DeserializeObject<Response>(content);
                        if (result.Statuscode == System.Net.HttpStatusCode.OK)
                        {
                            return Json(1);
                        }
                        else
                        {
                            return Json(0);
                        }

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
        /// saves video
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddVideo()
        {
            Common common = new Common(_Env);
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                var savepath = common.SaveFile(file);
                PromoVideos model = new PromoVideos();
                model.OriginalName = file.FileName;
                model.FilePath = savepath;
                model.FileName = savepath;
                using (var request = new HttpClient())
                {
                    request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");


                    var postTask = await request.PostAsJsonAsync<PromoVideos>("AddPromoVideo", model);
                    var result = await postTask.Content.ReadAsStringAsync();
                    if (postTask.IsSuccessStatusCode)
                    {
                        return Json(1);
                    }

                }
            }
            if (Request.Form.Files.Count() == 0)
            {
                return Json(2);
            }
            return Json(0);
        }


        /// <summary>
        /// view the prohz legal file
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> ProhzLegalFiles()
        {


            List<ProhzLegalFiles> ProhzLegalFiles = new List<ProhzLegalFiles>();

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

                        ProhzLegalFiles = JsonConvert.DeserializeObject<List<ProhzLegalFiles>>(content);


                        return View(ProhzLegalFiles);
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

        /// <summary>
        /// Add Prohz legal files
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProhzLegalFiles(LegalFilesViewModel smodel)
        {
            string _Path = string.Empty;
            
            Common common = new Common(_Env);
           
            if (smodel.PrevFile != null)
            {
                bool IsDeleted = common.DeleteFile(smodel.PrevFile);
                if (IsDeleted)
                {
                    if (smodel.LegalDocFiles != null)
                    {
                        _Path = common.SaveFile(smodel.LegalDocFiles);
                    }
                }
                else
                {
                    return RedirectToAction("ProhzLegalFiles");

                }
            }


            ProhzLegalFiles model = new ProhzLegalFiles();
            model.Name = smodel.LegalDocFiles.FileName;
            model.FilePath = _Path;
            model.FileType = smodel.Type;
            using (var request = new HttpClient())
            {
                request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");


                var postTask = await request.PostAsJsonAsync<ProhzLegalFiles>("AddLegalFiles", model);
                var result = await postTask.Content.ReadAsStringAsync();
                if (postTask.IsSuccessStatusCode)
                {
                    return RedirectToAction("ProhzLegalFiles");
                }

            }

            return RedirectToAction("ProhzLegalFiles");
        }

        /// <summary>
        /// Gets all promo videos
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> PromoVideos()
        {

            List<PromoVideos> FAQ = new List<PromoVideos>();

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

                        FAQ = JsonConvert.DeserializeObject<List<PromoVideos>>(content);


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

        /// <summary>
        /// gets all categories
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Categories()
        {

            List<Categories> Categories = new List<Categories>();

            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    //HTTP POST
                    var httpResponse = await client.GetAsync("GetAllCategories");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        Categories = JsonConvert.DeserializeObject<List<Categories>>(content);


                        return View(Categories);
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
        /// gets all FAQ
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// gets all documnets for verification 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DocumentsVerification()
        {

            try
            {
                List<DocumentsVerificationViewModels> details = new List<DocumentsVerificationViewModels>();
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");

                    //HTTP POST
                    var httpResponse = await client.GetAsync("GetDocumntsListForVerification");

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    if (httpResponse.IsSuccessStatusCode)
                    {

                        var deserializedResultList = JsonConvert.DeserializeObject<List<DocumentsVerificationViewModels>>(content);

                        details = deserializedResultList;
                        return View(details);
                    }

                }
                return View();
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        /// <summary>
        /// Updates documnets Status
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateDocumentsStatus([FromBody] DocumentsVerificationStatusUpdateViewModel Model)
        {


            using (var request = new HttpClient())
            {
                request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");


                var postTask = await request.PostAsJsonAsync<DocumentsVerificationStatusUpdateViewModel>("UpdateDocumentVerification", Model);
                var result = await postTask.Content.ReadAsStringAsync();
                if (postTask.IsSuccessStatusCode)
                {
                    return Json(1);
                }

            }


            return Json(0);
        }


        /// <summary>
        /// Updates FAQ
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateFAQ(FAQ Model)
        {


            using (var request = new HttpClient())
            {
                request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");


                var postTask = await request.PostAsJsonAsync<FAQ>("UpdateFAQ", Model);
                var result = await postTask.Content.ReadAsStringAsync();
                if (postTask.IsSuccessStatusCode)
                {
                    return Json(1);
                }

            }

            return Json(0);
        }


        /// <summary>
        /// Updates category
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(Categories Model)
        {


            using (var request = new HttpClient())
            {
                request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");


                var postTask = await request.PostAsJsonAsync<Categories>("UpdateCategory", Model);
                var result = await postTask.Content.ReadAsStringAsync();
                if (postTask.IsSuccessStatusCode)
                {
                    return Json(1);
                }

            }


            return Json(0);
        }


        /// <summary>
        /// saves categroy
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCategory(Categories Model)
        {


            using (var request = new HttpClient())
            {
                request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");


                var postTask = await request.PostAsJsonAsync<Categories>("AddCategory", Model);
                var result = await postTask.Content.ReadAsStringAsync();
                var deserializedResult = JsonConvert.DeserializeObject<Response>(result);
                if (deserializedResult.Message== "Category already exist")
                {
                    return Json(2);
                }
                
                if (postTask.IsSuccessStatusCode)
                {
                    return Json(1);
                }

            }


            return Json(0);
        }

        /// <summary>
        /// Saves FAQ
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddFAQ(FAQ Model)
        {


            using (var request = new HttpClient())
            {
                request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Admin/");


                var postTask = await request.PostAsJsonAsync<FAQ>("AddFAQ", Model);
                var result = await postTask.Content.ReadAsStringAsync();
                if (postTask.IsSuccessStatusCode)
                {
                    return Json(1);
                }

            }



            return Json(0);
        }

        /// <summary>
        /// Saves custom plan
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> AddCreateCustomPlan(CustomPlanViewModel Model)
        {


            using (var request = new HttpClient())
            {
                request.BaseAddress = new Uri(_configuration.GetValue<string>("WebApi:API_URL") + "/Payment/");


                var postTask = await request.PostAsJsonAsync<CustomPlanViewModel>("CreateCustomPriceSubscription", Model);
                var result = await postTask.Content.ReadAsStringAsync();
                if (postTask.IsSuccessStatusCode)
                {
                    return Json(1);
                }

            }

            return Json(0);
        }
    }
}
