using KopkeHome_DataAccessLayer.Repository;
using KopkeHome_FMRS_API.Properties;
using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;
using KopkeHome_UtilityLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Stripe;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
namespace KopkeHome_FMRS_API.Controllers
{

#nullable disable

    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount service;
        private readonly IMembership _membership;
        private readonly IEmailService _email;
        private readonly IRepository<VerifyOTP> _OtpRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IMembership membership, IConfiguration configuration, IAccount _service, IEmailService email, IRepository<VerifyOTP> OtpRepository, ILogger<AccountController> logger)
        {
            _membership = membership;
            service = _service;
            _email = email;
            _OtpRepository = OtpRepository;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// This method registers the user type home owner
        /// </summary>
        /// <param name="userDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> BasicInfoHomeOwner(HomeOwnerViewModel userDataModel)
        {
            try
            {
                Response response = new Response();
                if (!ModelState.IsValid)
                {
                    response.Error = false;
                    response.Status = Resources.InvalidModel;
                    response.Statuscode = HttpStatusCode.BadRequest;
                    response.Message = Resources.InvalidModel;

                    return response;
                }
                //
                bool isEmailExist = await service.CheckEmailExist(userDataModel.EmailHo);
                if (!isEmailExist)
                {
                    string status = await service.SendOtp(userDataModel.EmailHo, userDataModel.FirstNameHo);
                    if (status == "Otp Sent")
                    {
                        UserViewModel user = new UserViewModel()
                        {
                            RoleId = userDataModel.RoleId,
                            FirstName = userDataModel.FirstNameHo,
                            LastName = userDataModel.LastNameHo,
                            Email = userDataModel.EmailHo,

                            PhoneNumberOffice = userDataModel.PhoneNumberOfficeHo,
                            PhoneNumber = userDataModel.PhoneNumberHo,
                            City = userDataModel.CityHo,
                            ZipCode = userDataModel.ZipCodeHo,
                            State = userDataModel.StateHo,
                            BusinessAddress = userDataModel.BusinessAddressHo,
                            Password = userDataModel.PasswordHo,
                            ConfirmPassword = userDataModel.ConfirmPasswordHo,
                            Message = userDataModel.MessageHo,
                            MessageCode = userDataModel.MessageCodeHo,
                            IsRegistrationSuccess = userDataModel.IsRegistrationSuccessHo,
                            MemberReferralId = userDataModel.MemberReferralIdHo,
                            SalesAssociate=userDataModel.SalesAssociateHo, 
                            HeardAboutProhzFrom=userDataModel.HeardAboutProhzFromHo,
                        };
                        await service.BasicInfo(user);
                        response.Error = false;
                        response.Status = Resources.SUCCESS;
                        response.Statuscode = HttpStatusCode.OK;
                        response.Message = Resources.REGS_SUCCESS;
                        response.Data = userDataModel;
                        return response;

                        // return Ok(new
                        // {
                        //     statuscode = 200,
                        //     status = "Success",
                        //     message = "OK_TEST",
                        //     data = userDataModel.EmailHo
                        // });
                    }
                    else
                    {
                        response.Error = true;
                        response.Status = Resources.NOT_FOUND;
                        response.Statuscode = HttpStatusCode.NotFound;
                        response.Message = Resources.ALREADY_REG;
                        return response;
                    }
                }
                else
                {
                    response.Error = true;
                    response.Status = Resources.NOT_FOUND;
                    response.Statuscode = HttpStatusCode.AlreadyReported;
                    response.Message = Message.EmailAlreadyExist;
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }


        /// <summary>
        /// This method sends forgotpassword email to user.
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="html"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> ForgotPasswordSendMail([FromForm] string Email, [FromForm] string html)
        {

            Response response = new Response();
            var IsSent = _email.SendEmail(Email, Resources.ForgotPasswordResetLinkSubject, html);
            if (IsSent)
            {
                response.Message = Resources.SuccessMsg;
                response.Statuscode = HttpStatusCode.OK;
                return response;
            }
            response.Message = Resources.FailureMsg;
            response.Statuscode = HttpStatusCode.BadRequest;
            return response;

        }
        /// <summary>
        /// This method generates token of forgot password for user.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> ForgotPassword([FromForm] string email)
        {

            Response response = new Response();
            var user = await service.GetUserByEmail(email);
            if (user == null)
            {
                response.Message = Resources.EmailIsNotRegistered;
                response.Statuscode = HttpStatusCode.NotFound;

                return response;
            }


            var token = await service.GeneratePasswordResetTokenAsync(user);
            if (token != null)
            {
                response.Statuscode = HttpStatusCode.OK;
                response.Message = Resources.SuccessMsg;
                response.Data = token;
            }

            return response;

        }

        [HttpPost]
        public async Task<Response> ForgotPasswordApp([FromForm] string email)
        {

            Response response = new Response();
            var user = await service.GetUserByEmail(email);
            if (user == null)
            {
                response.Message = Resources.EmailIsNotRegistered;
                response.Statuscode = HttpStatusCode.NotFound;
                response.Status = "Error";

                return response;
            }


            
            if (user != null)
            {

                string weburl = _configuration.GetSection("PaymentUrl")["CurrentDomain"];
                
                using (var httpClient = new HttpClient())
                {
                    var data = await httpClient.GetAsync(weburl + "User/GenerateLinkForgotPassword?Email="+email);
                   if(data.StatusCode == HttpStatusCode.OK)
                    {
                        response.Statuscode = HttpStatusCode.OK;
                        response.Message = Resources.SuccessMsg;
                        response.Data = "";
                    }
                    else
                    {
                        response.Statuscode = HttpStatusCode.NotFound;
                        response.Message = Resources.NOT_FOUND;
                        response.Data = "";
                    }
                }

               
            }

            return response;

        }
        /// <summary>
        /// This method changes users password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<Response> ResetPassword(ForgotpasswordViewModel model)
        {
            return service.ResetPasswordAsync(model.Token, model.Email, model.Password);
        }
        /// <summary>
        /// THis method registers user of type contractors
        /// </summary>
        /// <param name="userDataModel"></param>
        /// <returns></returns>
        [HttpPost]
        // public async Task<Response> BasicInfo(UserViewModel userDataModel)
        public async Task<Response> BasicInfo([FromForm] UserViewModel userDataModel)
        {
            try
            {
                Response response = new Response();
                if (!ModelState.IsValid)
                {
                    response.Error = false;
                    response.Status = Resources.InvalidModel;
                    response.Statuscode = HttpStatusCode.BadRequest;
                    response.Message = Resources.InvalidModel;

                    return response;
                }

                // Check if the provided referral ID matches the one in the database
                bool isReferralValid = await service.CheckReferralId(userDataModel.MemberReferralId);
                if (!isReferralValid)
                {
                    response.Error = true;
                    response.Status = Resources.INVALID_REFERRAL;
                    response.Statuscode = HttpStatusCode.BadRequest;
                    response.Message = Resources.INVALID_REFERRAL;
                    return response;
                }


                bool isEmailExist = await service.CheckEmailExist(userDataModel.Email);
                if (!isEmailExist)
                {
                    string status = await service.SendOtp(userDataModel.Email, userDataModel.FirstName);
                    if (status == "Otp Sent")
                    {
                        await service.BasicInfo(userDataModel);

                        response.Error = false;
                        response.Status = Resources.SUCCESS;
                        response.Statuscode = HttpStatusCode.OK;
                        response.Message = Resources.REGS_SUCCESS;
                        response.Data = userDataModel;
                        return response;

                        // return Ok(new
                        // {
                        //     statuscode = 200,
                        //     status = "Success",
                        //     message = "OK_TEST",
                        //     data = userDataModel.EmailHo
                        // });
                    }
                    else
                    {
                        response.Error = true;
                        response.Status = Resources.NOT_FOUND;
                        response.Statuscode = HttpStatusCode.NotFound;
                        response.Message = Resources.ALREADY_REG;
                        return response;
                    }
                }
                else
                {
                    response.Error = true;
                    response.Status = Resources.NOT_FOUND;
                    response.Statuscode = HttpStatusCode.AlreadyReported;
                    response.Message = Message.EmailAlreadyExist;
                    return response;

                }

            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }
        /// <summary>
        /// This method verify OTP provided by user during email verfication.
        /// </summary>
        /// <param name="emailVerficationModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> VerifyOTP(EmailVerficationModel emailVerficationModel)
        {
            try
            {
                Response response = new Response();
                var result = await service.ChechVerificatrionCode(emailVerficationModel.VerificationCode, emailVerficationModel.Email);
                if (result)
                {

                    response.Error = false;
                    response.Status = Resources.SUCCESS;
                    response.Message = Resources.VER_SUCCESS;
                    response.Statuscode = HttpStatusCode.OK;

                }
                else
                {
                    response.Error = true;
                    response.Status = Resources.NOT_FOUND;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = Resources.ALREADY_REG;
                    return BadRequest(response);

                }
                response.Data = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// This method gets list of states
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<State>> GetStates()
        {
            try
            {

                var states = await service.GetStateList();
                return states;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        /// <summary>
        /// This method gets list of states
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<City>> GetCitiesList([FromForm] string Id)
        {
            try
            {

                var Cities = await service.GetCitesList(Convert.ToInt32(Id));
                return Cities;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }


        /// <summary>
        /// used for signin into application
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> SignIn(SignIn userData)
        {
            try
            {
                Response response = new Response();
                if (!ModelState.IsValid)
                {
                    response.Error = false;
                    response.Status = Resources.FailureMsg;
                    response.Statuscode = HttpStatusCode.BadRequest;
                    response.Message = Resources.InvalidModel;

                    return response;
                }
                var isAuthenticate = await service.SignIn(userData);
                User isUserExist = (User)isAuthenticate.Data;
                //var isUserExist = SomeType typed = (SomeType)obj;

                if (isAuthenticate.Statuscode == System.Net.HttpStatusCode.OK)
                {

                    if (isUserExist.RoleId != Constant.Admin)
                    {


                        //Checking starts here.

                        if (isUserExist.RoleId != Constant.HomeOwner)
                        {
                            if (isUserExist.RoleId == Constant.Contractor)
                            {
                                var BusinessProfileExiest = await service.CheckBusinessProfile(isUserExist);
                                if (BusinessProfileExiest == null)
                                {
                                    response.Error = false;
                                    response.Status = Resources.SuccessMsg;
                                    response.Statuscode = HttpStatusCode.OK;
                                    response.Code = Constant.Contractor;
                                    return response;
                                }


                            }
                            else if (isUserExist.RoleId == Constant.SubContractor || isUserExist.RoleId == Constant.IndependentContractor)
                            {
                                var BusinessProfileExiest = await service.CheckBusinessProfileSub(isUserExist);
                                if (BusinessProfileExiest == null)
                                {
                                    response.Error = false;
                                    response.Status = Resources.SuccessMsg;
                                    response.Statuscode = HttpStatusCode.OK;
                                    response.Code = Constant.SubContractor;
                                    return response;
                                }
                            }

                        }
                        //cheking subscription plan
                        var subs = await service.CheckUserSubscriptions(isUserExist);
                        
                        //var subsPlan = await service.CheckUserCurrentPlan(isUserExist);
                        
                        if (subs == null)
                        {
                            var type = await service.GetUserByEmail(isUserExist.Email);
                            if (type.RoleId != Constant.HomeOwner)
                            {//contrators
                                response.Error = false;
                                response.Status = Resources.SuccessMsg;
                                response.Statuscode = HttpStatusCode.OK;
                                response.Code = 5;
                                return response;

                            }
                            else
                            {//homeowners
                                response.Error = false;
                                response.Status = Resources.SuccessMsg;
                                response.Statuscode = HttpStatusCode.OK;
                                response.Code = 6;
                                return response;
                            }


                        }
                        ////cheking Select zipcodes and categories

                        var subsCats = await service.CheckUserCategories(isUserExist);
                        var subszipcodes = await service.CheckUserZipcodes(isUserExist);
                        var ZipsAndCatsAreThere = await service.CheckZipsAndCatsOnUpgradeOrDowngrade(isUserExist);
                        bool IsZipIsExist = false;
                        if (subsCats == true && subszipcodes == true)
                        {
                            IsZipIsExist = true;
                        }
                        if (IsZipIsExist == false || ZipsAndCatsAreThere == false)
                        {
                            var type = await service.GetUserByEmail(isUserExist.Email);
                            if (type.RoleId != Constant.HomeOwner && type.RoleId != Constant.Admin)
                            {
                                //contrators
                                response.Error = false;
                                response.Status = Resources.SuccessMsg;
                                response.Statuscode = HttpStatusCode.OK;
                                response.Code = 7;
                                return response;
                            }
                        }
                    }
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", isUserExist.Id.ToString()),
                    new Claim("FirstName", isUserExist.FirstName),
                    new Claim("LastName", isUserExist.LastName),
                    new Claim("UserName", isUserExist.UserName),
                    new Claim("Role", isUserExist.RoleId.ToString()),
                    new Claim("Email", isUserExist.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    var vtokern = new JwtSecurityTokenHandler().WriteToken(token);
                    response.Data = vtokern;





                    response.Error = false;
                    response.Status = Resources.SuccessMsg;
                    response.Statuscode = HttpStatusCode.OK;
                    response.Message = Resources.ValidUser;
                    if (isUserExist.RoleId == Constant.Admin)
                    {
                        response.Code = 8;
                    }

                    return response;

                }
                else
                {
                    response.Error = true;
                    response.Status = Resources.NOT_FOUND;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = isAuthenticate.Message;
                    return response;

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }



        //this signin api use only for Mobile app.

        [HttpPost]
        public async Task<Response> SignInApp(SignIn userData)
        {
            try
            {
                Response response = new Response();
                if (!ModelState.IsValid)
                {
                    response.Error = false;
                    response.Status = Resources.FailureMsg;
                    response.Statuscode = HttpStatusCode.BadRequest;
                    response.Message = Resources.InvalidModel;

                    return response;
                }
                var isAuthenticate = await service.SignIn(userData);
                User isUserExist = (User)isAuthenticate.Data;
                //var isUserExist = SomeType typed = (SomeType)obj;

                if (isAuthenticate.Statuscode == System.Net.HttpStatusCode.OK)
                {

                    if (isUserExist.RoleId != Constant.Admin)
                    {


                        //Checking starts here.

                        if (isUserExist.RoleId != Constant.HomeOwner)
                        {
                            if (isUserExist.RoleId == Constant.Contractor)
                            {
                                var BusinessProfileExiest = await service.CheckBusinessProfile(isUserExist);
                                if (BusinessProfileExiest == null)
                                {
                                    response.Error = false;
                                    response.Status = Resources.SuccessMsg;
                                    response.Statuscode = HttpStatusCode.OK;
                                    response.Code = Constant.Contractor;
                                    return response;
                                }


                            }
                            else if (isUserExist.RoleId == Constant.SubContractor || isUserExist.RoleId == Constant.IndependentContractor)
                            {
                                var BusinessProfileExiest = await service.CheckBusinessProfileSub(isUserExist);
                                if (BusinessProfileExiest == null)
                                {
                                    response.Error = false;
                                    response.Status = Resources.SuccessMsg;
                                    response.Statuscode = HttpStatusCode.OK;
                                    response.Code = Constant.SubContractor;
                                    return response;
                                }
                            }

                        }
                        //cheking subscription plan
                        var subs = await service.CheckUserSubscriptions(isUserExist);

                        var subsPlan = await service.CheckUserCurrentPlan(isUserExist);
                        
                            if (subs.PlanId == subsPlan.Id && subsPlan.PhoneApp == false)
                            {
                                response.Message = Resources.Messase_LoginR;
                                response.Error = false;
                                response.Status = Resources.SuccessMsg;
                                response.Statuscode = HttpStatusCode.BadRequest;
                                return response;

                            }
                        
                        if (subs == null)
                        {
                            var type = await service.GetUserByEmail(isUserExist.Email);
                            if (type.RoleId != Constant.HomeOwner)
                            {//contrators
                                response.Error = false;
                                response.Status = Resources.SuccessMsg;
                                response.Statuscode = HttpStatusCode.OK;
                                response.Code = 5;
                                return response;

                            }
                            else
                            {//homeowners
                                response.Error = false;
                                response.Status = Resources.SuccessMsg;
                                response.Statuscode = HttpStatusCode.OK;
                                response.Code = 6;
                                return response;
                            }


                        }
                        ////cheking Select zipcodes and categories

                        var subsCats = await service.CheckUserCategories(isUserExist);
                        var subszipcodes = await service.CheckUserZipcodes(isUserExist);
                        var ZipsAndCatsAreThere = await service.CheckZipsAndCatsOnUpgradeOrDowngrade(isUserExist);
                        bool IsZipIsExist = false;
                        if (subsCats == true && subszipcodes == true)
                        {
                            IsZipIsExist = true;
                        }
                        if (IsZipIsExist == false || ZipsAndCatsAreThere == false)
                        {
                            var type = await service.GetUserByEmail(isUserExist.Email);
                            if (type.RoleId != Constant.HomeOwner && type.RoleId != Constant.Admin)
                            {
                                //contrators
                                response.Error = false;
                                response.Status = Resources.SuccessMsg;
                                response.Statuscode = HttpStatusCode.OK;
                                response.Code = 7;
                                return response;
                            }
                        }
                    }
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", isUserExist.Id.ToString()),
                    new Claim("FirstName", isUserExist.FirstName),
                    new Claim("LastName", isUserExist.LastName),
                    new Claim("UserName", isUserExist.UserName),
                    new Claim("Role", isUserExist.RoleId.ToString()),
                    new Claim("Email", isUserExist.Email)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    var vtokern = new JwtSecurityTokenHandler().WriteToken(token);
                    response.Data = vtokern;





                    response.Error = false;
                    response.Status = Resources.SuccessMsg;
                    response.Statuscode = HttpStatusCode.OK;
                    response.Message = Resources.ValidUser;
                    response.WorkStatus = isUserExist.WorkStatus;
                    response.IsDocumentVerified = isUserExist.IsDocumentsVerified;
                    if (isUserExist.RoleId == 4)
                    {
                        response.IsDocumentVerified = true;
                    }
                    if (isUserExist.RoleId == Constant.Admin)
                    {
                        response.Code = 8;
                    }

                    return response;

                }
                else
                {
                    response.Error = true;
                    response.Status = Resources.NOT_FOUND;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = isAuthenticate.Message;
                    return response;

                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }
        /// <summary>
        /// Resets password after user is logged in.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> ChangePassword(ChangePasswordModel model)
        {
            try
            {
                return await service.ChangePassword(model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        /// <summary>
        /// Gets user by email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<User> GetUserByEmail([FromForm] string email)
        {
            try
            {
                return await service.GetUserByEmail(email);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        [HttpPost]
        public async Task<User> GetUserById([FromForm] string Id)
        {
            try
            {
                return await service.GetUserByID(Convert.ToInt32(Id));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        [HttpPost]
        public async Task<ProhzReferral> GetReferralsById([FromForm] string Id)
        {
            try
            {
                return await service.GetReferralsById(Convert.ToInt32(Id));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// Updates basic info of contractor
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> UpdateBasicInfo(UpdateBasicInfo model)
        {
            try
            {
                Response response = new Response();
                if (!ModelState.IsValid)
                {
                    response.Error = false;
                    response.Status = Resources.InvalidModel;
                    response.Statuscode = HttpStatusCode.BadRequest;
                    response.Message = Resources.InvalidModel;

                    return response;
                }


                var user = await service.UpdateBasicInfo(model);
                if (user != null)
                {
                    response.Error = false;
                    response.Status = Resources.SUCCESS;
                    response.Statuscode = HttpStatusCode.OK;
                    response.Message = Resources.REGS_SUCCESS;
                    response.Data = user;
                    return response;
                }
                else
                {
                    response.Error = false;
                    response.Status = Resources.FailureMsg;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = Resources.UserNotFound;
                    response.Data = null;
                    return response;
                }




            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }

        [HttpPost]
        public async Task<Response> UpdateBasicInfoApp([FromForm]UpdateBasicInfo model)
        {
            try
            {
                Response response = new Response();
                if (!ModelState.IsValid)
                {
                    response.Error = false;
                    response.Status = Resources.InvalidModel;
                    response.Statuscode = HttpStatusCode.BadRequest;
                    response.Message = Resources.InvalidModel;

                    return response;
                }


                var user = await service.UpdateBasicInfoApp(model);
                if (user != null)
                {
                    response.Error = false;
                    response.Status = Resources.SUCCESS;
                    response.Statuscode = HttpStatusCode.OK;
                    response.Message = Resources.DataUpdated;
                    response.Data = user;
                    return response;
                }
                else
                {
                    response.Error = false;
                    response.Status = Resources.FailureMsg;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = Resources.UserNotFound;
                    response.Data = null;
                    return response;
                }




            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }
        [HttpPost]
        public async Task<User> UserWorkStatus(WorkStatusViewModel model)
        {
            try
            {

                return await service.UserWorkStatus(model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }



        }
        /// <summary>
        /// Updates basic info of home owner.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> UpdateBasicInfoHomeOwner(UpdateBasicInfoHomeOwner model)
        {
            try
            {
                Response response = new Response();
                if (!ModelState.IsValid)
                {
                    response.Error = false;
                    response.Status = Resources.InvalidModel;
                    response.Statuscode = HttpStatusCode.BadRequest;
                    response.Message = Resources.InvalidModel;

                    return response;
                }


                var user = await service.UpdateBasicInfoHomeOwner(model);
                if (user != null)
                {
                    response.Error = false;
                    response.Status = Resources.SUCCESS;
                    response.Statuscode = HttpStatusCode.OK;
                    response.Message = Resources.REGS_SUCCESS;
                    response.Data = user;
                    return response;
                }
                else
                {
                    response.Error = false;
                    response.Status = Resources.FailureMsg;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = Resources.UserNotFound;
                    response.Data = null;
                    return response;
                }




            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }
        [HttpPost]
        public async Task<Response> UpdateBasicInfoHomeOwnerApp([FromForm]UpdateBasicInfoHomeOwner model)
        {
            try
            {
                Response response = new Response();
                if (!ModelState.IsValid)
                {
                    response.Error = false;
                    response.Status = Resources.InvalidModel;
                    response.Statuscode = HttpStatusCode.BadRequest;
                    response.Message = Resources.InvalidModel;

                    return response;
                }


                var user = await service.UpdateBasicInfoHomeOwner(model);
                if (user != null)
                {
                    response.Error = false;
                    response.Status = Resources.SUCCESS;
                    response.Statuscode = HttpStatusCode.OK;
                    response.Message = Resources.DataUpdated;
                    response.Data = user;
                    return response;
                }
                else
                {
                    response.Error = false;
                    response.Status = Resources.FailureMsg;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = Resources.UserNotFound;
                    response.Data = null;
                    return response;
                }




            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }

        [HttpPost]
        public async Task<ZipcodesAndCategoriesViewModel> GetZipcodesStatesAndCategories([FromForm] string Email)
        {
            try
            {  //dsaszxczxc
                var user = await GetUserByEmail(Email);
                var ZipsandCats = await service.StatesCategoriesAndStatesList(user.Id);
                return ZipsandCats;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }

        [HttpPost]
        public async Task<Response> CheckContractorsOnZipcode([FromForm] string zipcode)
        {
            try
            {
                Response response = new Response();
                var contractors = await service.CheckContractorsOnZipcode(Convert.ToInt32(zipcode));
                if (contractors)
                {
                    response.Error = false;
                    response.Status = Resources.SuccessMsg;
                    response.Statuscode = HttpStatusCode.OK;
                    response.Message = Resources.More10No;
                    response.Data = true;
                    return response;
                }
                else
                {
                    response.Error = false;
                    response.Status = Resources.FailureMsg;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = Resources.Less10No;
                    response.Data = false;
                    return response;
                }




            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

       [HttpPost]
       public async Task<Response> DeleteUser(int id)
        {
            try
            {
                Response response = new Response();
                bool data = await service.DeleteUser(id);
                if (data)
                {
                    response.Error = false;
                    response.Status = Resources.SuccessMsg;
                    response.Statuscode = HttpStatusCode.OK;
                    response.Message = Resources.UserDeleted;
                    response.Data = true;
                    return response;
                }
                else
                {
                    response.Error = false;
                    response.Status = Resources.FailureMsg;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = Resources.UserIsDelete;
                    response.Data = false;
                    return response;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

