using AutoMapper;
using KopkeHome_BusinessLayer.Interface;
using KopkeHome_DataAccessLayer;
using KopkeHome_DataAccessLayer.Repository;
using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.PaymentAndSubscription;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;
using KopkeHome_UtilityLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Stripe;
using System.Text;

namespace KopkeHome_BusinessLayer.Services
{

#pragma warning disable
    public class AccountService : IAccount
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<User> _iRepository;
        private readonly IRepository<ProhzReferral> _iRefferal;
        private readonly IEmailService _email;
        private readonly ApplicationDbContext _dbContext;
        private readonly IRepository<VerifyOTP> _OtpRepository;
        private readonly IMapper Mapper;
        private readonly ILogger<AccountService> _logger;
        private readonly IConfiguration _configuration;
        public AccountService(IConfiguration iConfig, ILogger<AccountService> logger, IRepository<ProhzReferral> referral, UserManager<User> userManager, IRepository<User> repository, IEmailService email,
             ApplicationDbContext dbContext, IRepository<VerifyOTP> OtpRepository, IMapper mapper)
        {
            _configuration = iConfig;
            _userManager = userManager;
            _iRepository = repository;
            _email = email;
            _dbContext = dbContext;
            this.Mapper = mapper;
            _OtpRepository = OtpRepository;
            _logger = logger;
            _iRefferal = referral;

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }


        /// <summary>
        /// Resets password after user is logged in.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> ChangePassword(ChangePasswordModel model)
        {
            try
            {

                Response response = new Response();


                // var user = await _userManager.FindByEmailAsync(model.Email);

                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return new Response
                    {
                        Error = true,
                        Statuscode = System.Net.HttpStatusCode.NotFound,
                        Message = "User not found"
                    };
                }

                // var isOld = await _userManager.CheckPasswordAsync(user, model.NewPassword);

                var isOld = await _userManager.CheckPasswordAsync(user, model.OldPassword);

                if (isOld)
                {

                    // response.Error = true;
                    response.Error = false;
                    response.Statuscode = System.Net.HttpStatusCode.AlreadyReported;
                    response.Message = ResourceBusinessLayer.SamePassword;
                    return response;
                }

                var isChanged = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (isChanged.Succeeded)
                {
                    response.Error = true;
                    response.Statuscode = System.Net.HttpStatusCode.OK;
                    response.Message = ResourceBusinessLayer.PasswordChanged;

                }
                else
                {
                    response.Error = true;
                    response.Statuscode = System.Net.HttpStatusCode.BadRequest;
                    response.Message = ResourceBusinessLayer.IncorrectPassword;

                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Signin user
        /// </summary>
        /// <param name="ISignIn"></param>
        /// <returns></returns>
        public async Task<Response> SignIn(SignIn ISignIn)
        {
            try
            {
                Response response = new Response();
                User _user = new User();

                var result = await _iRepository.FindAllByCondition(a => a.Email.Equals(ISignIn.Email) && a.IsEmailVerified == true && a.IsDeleted == false);
                if (result.Any())
                {
                    _user = await _userManager.FindByEmailAsync(ISignIn.Email);

                    if (_user == null)
                    {
                        response.Statuscode = System.Net.HttpStatusCode.NotFound;
                        response.Status = "Incorrect email address.";
                        response.Message = "Incorrect email address.";
                        return response;
                    }

                    var isAuthenticated = await _userManager.CheckPasswordAsync(_user, ISignIn.Password);
                    response.WorkStatus = _user.WorkStatus;

                    if (isAuthenticated)
                    {
                        response.Data = _user;
                        response.Statuscode = System.Net.HttpStatusCode.OK;
                        response.Status = "success";
                        return response;
                    }
                    response.Data = _user;
                    response.Statuscode = System.Net.HttpStatusCode.NotFound;
                    response.Status = "Incorrect password.";
                    response.Message = "Incorrect password.";
                    return response;

                }
                response.Data = _user;
                response.Statuscode = System.Net.HttpStatusCode.NotFound;
                response.Status = "Incorrect email address.";
                response.Message = "Incorrect email address.";
                return response;





            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<UserMembershipSubscriptions> CheckUserSubscriptions(User userModel)
        {
            UserMembershipSubscriptions model = new UserMembershipSubscriptions();
            try
            {
                if (userModel != null)
                {

                    var UserSubs = await _dbContext.UserMembershipSubscriptions.Where(x => x.UserId.Equals(userModel.Id) && x.IsActive == true).FirstOrDefaultAsync();
                    if (UserSubs != null)
                    {
                        var service = new SubscriptionService();
                        Subscription StripeSubscriptionDetails = await service.GetAsync(UserSubs.StripeSubscriptionId);
                        //15-12-2022 + 2 months extention
                        if (StripeSubscriptionDetails.CurrentPeriodEnd > UserSubs.PeriodEndDate)
                        {
                            //When plan is auto deducted the money/auto renewd.

                            UserSubs.PeriodEndDate = StripeSubscriptionDetails.CurrentPeriodEnd;
                            await _dbContext.SaveChangesAsync();

                        }

                        if (UserSubs.StripeStatus.Contains("Cancelled") && UserSubs.PeriodEndDate <= DateTime.Now)
                        {
                            return null;
                        }
                        model = UserSubs;
                    }
                    else
                    {
                        return null;
                    }

                }
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }


        //Get current plan for get silve pricedetails for homeowner subscription.
        public async Task<MembershipBenifitsPlan> CheckUserCurrentPlan(User userModel)
        {
            MembershipBenifitsPlan model = new MembershipBenifitsPlan();
            try
            {
                if (userModel != null)
                {

                    var UserSubs = await _dbContext.MembershipBenefitsPlan.Where(x => x.PricePerMonth == 25 && x.PricePerYear == 50).FirstOrDefaultAsync();
                    if (UserSubs != null)
                    {
                        model = UserSubs;
                    }
                    else
                    {
                        return null;
                    }

                }
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Check if subcontractor/independent business profile exist.
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<BusinessProfileSubContractor> CheckBusinessProfileSub(User userModel)
        {
            BusinessProfileSubContractor BusinessProfile = new BusinessProfileSubContractor();
            try
            {
                if (userModel != null)
                {
                    if (userModel.RoleId == Constant.SubContractor || userModel.RoleId == Constant.IndependentContractor)
                    {
                        BusinessProfile = await _dbContext.BusinessProfileSubContractors.Where(x => x.UserId.Equals(userModel.Id)).FirstOrDefaultAsync();
                    }
                }
                return BusinessProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }



        /// <summary>
        /// Checks if contractor business profile exist.
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public async Task<BusinessProfileDataModel> CheckBusinessProfile(User userModel)
        {
            BusinessProfileDataModel BusinessProfile = new BusinessProfileDataModel();

            try
            {
                if (userModel != null)
                {
                    if (userModel.RoleId == Constant.Contractor)
                    {


                        BusinessProfile = await _dbContext.BusinessProfile.Where(x => x.UserId.Equals(userModel.Id)).FirstOrDefaultAsync();

                    }

                }
                return BusinessProfile;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        /// <summary>
        /// Creates users basic info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<User> BasicInfo(UserViewModel model)
        {
            try
            {

                IdentityResult result = new IdentityResult();


                long UniqueId = await GenerateUniquememberID(model.State);
                // User model = this.Mapper.Map<User>(userViewModel);

                User user = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumberOffice = model.PhoneNumberOffice,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = true,
                    Email = model.Email,
                    BusinessName = model.BusinessName,
                    City = model.City,
                    BusinessAddress = model.BusinessAddress,
                    ZipCode = model.ZipCode,
                    State = model.State,
                    UserName = model.Email,
                    RoleId = model.RoleId,
                    CreatedOn = DateTime.Now,
                    UniqueMemberId = UniqueId,
                    WorkStatus = 1,
                    WorkStatusModifiedOn = DateTime.Now,
                    HeardAboutProhzFrom = model.HeardAboutProhzFrom,

                };

                var emp = await _dbContext.User.Where(x => x.Email.Equals(user.Email)).FirstOrDefaultAsync();
                //if user does't varify its email then its just updates
                if (emp != null)
                {

                    emp.FirstName = model.FirstName;
                    emp.LastName = model.LastName;
                    emp.PhoneNumberOffice = model.PhoneNumberOffice;
                    emp.PhoneNumber = model.PhoneNumber;
                    emp.EmailConfirmed = true;
                    emp.Email = model.Email;
                    emp.BusinessName = model.BusinessName;
                    emp.City = model.City;
                    emp.BusinessAddress = model.BusinessAddress;
                    emp.ZipCode = model.ZipCode;
                    emp.State = model.State;
                    emp.UserName = model.Email;
                    emp.RoleId = model.RoleId;
                    emp.ModifiedOn = DateTime.Now;
                    emp.WorkStatus = 1;
                    emp.WorkStatusModifiedOn = DateTime.Now;
                    emp.HeardAboutProhzFrom = model.HeardAboutProhzFrom;
                    result = await _userManager.UpdateAsync(emp);
                }
                else
                {
                    result = await _userManager.CreateAsync(user, model.Password);

                }


                if (result.Errors.Any())
                {
                    model.MessageCode = result.Errors.FirstOrDefault()?.Code;
                    model.Message = result.Errors.FirstOrDefault()?.Description;

                }
                if (result.Succeeded)
                {
                    var userId = await _dbContext.User.Where(x => x.Email == model.Email).FirstOrDefaultAsync();
                    if (model.MemberReferralId != null)
                    {

                        var save = await AddReferralClaims(userId.Id, model.SalesAssociate, Convert.ToInt64(model.MemberReferralId));

                    }
                    if (model.SalesAssociate != null)
                    {
                        var saveSales = await ProhzSalesAssciates(model.SalesAssociate, userId.UniqueMemberId, userId.Email, userId.FirstName + " " + userId.LastName);
                    }
                    // var str = UniqueId.ToString().Remove(0, 4);

                    var uid = UniqueId.ToString();
                    var str = uid.Length > 4 ? uid.Substring(4) : uid;

                    var mID = await _dbContext.UniqueMemberId.FirstOrDefaultAsync();

                    mID.MemberId = (long)Convert.ToDouble(str);
                    _dbContext.UniqueMemberId.Update(mID);
                    await _dbContext.SaveChangesAsync();
                    model.IsRegistrationSuccess = true;

                }
                return emp;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> ProhzSalesAssciates(string SalesAssciatesName, long MemberId, string MemberEmail, string MemberName)
        {
            bool Flag = false;
            ProhzSalesAssciates model = new ProhzSalesAssciates();
            model.JoinedMemberMemberId = MemberId;
            model.JoinedMemberName = MemberName;
            model.SalesPersonName = SalesAssciatesName;
            model.JoinedMemberEmail = MemberEmail;
            model.JoinedOn = DateTime.Now;
            model.IsRegistred = false;
            var chechk = await _dbContext.ProhzSalesAssciates.Where(x => x.JoinedMemberMemberId == MemberId).FirstOrDefaultAsync();
            if (chechk != null)
            {
                chechk.JoinedMemberMemberId = MemberId;
                chechk.JoinedMemberName = MemberName;
                chechk.SalesPersonName = SalesAssciatesName;
                chechk.JoinedMemberEmail = MemberEmail;
                chechk.JoinedOn = DateTime.Now;
                chechk.IsRegistred = false;
                await _dbContext.SaveChangesAsync();
                Flag = true;
            }
            else
            {
                var save = await _dbContext.ProhzSalesAssciates.AddAsync(model);
                if (save != null)
                {
                    await _dbContext.SaveChangesAsync();
                    Flag = true;
                }
            }




            return Flag;
        }
        public async Task<bool> AddReferralClaims(int UserId, string ProhzSales, long MemberId)
        {
            bool Flag = false;
            ProhzReferral model = new ProhzReferral();
            model.UserId = UserId;
            model.MemberId = MemberId;
            model.SalesPersonName = ProhzSales;
            model.IsRegistrationComplete = false;
            var save = await _dbContext.ProhzReferral.AddAsync(model);
            if (save != null)
            {
                await _dbContext.SaveChangesAsync();
                Flag = true;
            }


            return Flag;
        }

        /// <summary>
        /// Creates users basic info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<User> UserWorkStatus(WorkStatusViewModel model)
        {
            try
            {

                IdentityResult result = new IdentityResult();

                var emp = await _dbContext.User.FindAsync(model.UserId);

                if (emp != null)
                {

                    emp.WorkStatus = model.radioWorkStatus;
                    emp.WorkStatusModifiedOn = DateTime.Now;
                    result = await _userManager.UpdateAsync(emp);
                }


                return emp;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Sends OTP during email verfication
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<string> SendOtp(string email, string name)
        {
            try
            {
                string status = string.Empty;

                var item = _dbContext.VerifyOTP.Where(x => x.Email.Equals(email)).FirstOrDefault();
                if (item != null)
                {
                    _dbContext.VerifyOTP.Remove(item);
                    // _dbContext.SaveChanges();
                    await _dbContext.SaveChangesAsync();
                }

                VerifyOTP oTP = new()
                {
                    Email = email,
                    VerificationCode = _email.GenerateOTPForAuthentication(),
                    ExpiryDate = DateTime.Now.AddMinutes(10),
                    CreatedDate = DateTime.Now
                };
                await _OtpRepository.Add(oTP);


                StringBuilder Body = new();
                Body.Append("Hello " + name + "," + "</br></br>");
                Body.Append("Please use the following verification code to verify the email address with Prohz.</br></br>");
                Body.Append("Verification code:  " + oTP.VerificationCode + "</br></br>");
                Body.Append("Thanks</br>Prohz Team");
                _email.SendEmail(email, "Verification code to verify the email address – Prohz", Body.ToString());

                status = "Otp Sent";

                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        /// <summary>
        /// Checks verfication codes.
        /// </summary>
        /// <param name="Otp"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<bool> ChechVerificatrionCode(string Otp, string Email)
        {
            try
            {

                bool status = false;
                string skip = _configuration.GetSection("Settings")["skipOTP"];


                if (skip == "true")
                {

                    var userList = await _OtpRepository.FindAllByCondition(a => a.Status == false && a.Email == Email);



                    if (userList.Count() != 0)
                    {

                        var expirytime = (userList[0].ExpiryDate - DateTime.Now).Minutes;
                        if (expirytime > 0)
                        {

                            userList[0].Status = true;
                            await _OtpRepository.Update(userList[0]);
                            var emp = _dbContext.User.Where(x => x.Email.Equals(Email)).FirstOrDefault();
                            if (emp != null)
                            {
                                emp.IsEmailVerified = true;

                                await _userManager.UpdateAsync(emp);
                                StringBuilder Body = new();
                                Body.Append("Congratulations " + emp.FirstName + "," + "</br></br>");
                                Body.Append("Thank you for registering with Prohz.</br></br>");
                                Body.Append("Thanks</br>Prohz Team");

                                _email.SendEmail(Email, "Welcome from Prohz", Body.ToString());
                            }

                        }
                        status = true;
                    }

                    return status;


                }
                else
                {








                    var userList = await _OtpRepository.FindAllByCondition(a => a.VerificationCode == (Otp) && a.Status == false && a.Email == Email);



                    if (userList.Count() != 0)
                    {

                        var expirytime = (userList[0].ExpiryDate - DateTime.Now).Minutes;
                        if (expirytime > 0)
                        {

                            userList[0].Status = true;
                            await _OtpRepository.Update(userList[0]);
                            var emp = _dbContext.User.Where(x => x.Email.Equals(Email)).FirstOrDefault();
                            if (emp != null)
                            {
                                emp.IsEmailVerified = true;

                                await _userManager.UpdateAsync(emp);
                                StringBuilder Body = new();
                                Body.Append("Congratulations " + emp.FirstName + "," + "</br></br>");
                                Body.Append("Thank you for registering with Prohz.</br></br>");
                                Body.Append("Thanks</br>Prohz Team");

                                _email.SendEmail(Email, "Welcome from Prohz", Body.ToString());
                            }

                        }
                        status = true;
                    }

                    return status;
                }
            }
            catch (Exception EX)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets list of states
        /// </summary>
        /// <returns></returns>
        public async Task<List<State>> GetStateList()
        {
            try
            {

                var StateList = await _dbContext.State.ToListAsync();

                return StateList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets list of city
        /// </summary>
        /// <returns></returns>
        public async Task<List<City>> GetCitesList(int Id)
        {
            try
            {

                var CitesList = await _dbContext.City.Where(x => x.StateId == Id).ToListAsync();

                return CitesList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }




        /// <summary>
        /// Updates home owners basic info
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<User> UpdateBasicInfoHomeOwner(UpdateBasicInfoHomeOwner model)
        {
            try
            {

                var emp = _dbContext.User.Find(model.Id);

                if (emp != null)
                {
                    if (emp.RoleId == Constant.HomeOwner)
                    {
                        if (model.ProfilePictureApp != null)
                        {
                            ImageModel imageData = new ImageModel();
                            byte[] fileByteArray;
                            using (MemoryStream item = new MemoryStream())
                            {
                                model.ProfilePictureApp.CopyTo(item);
                                fileByteArray = item.ToArray(); //2nd change here
                            }
                            string weburl = _configuration.GetSection("PaymentUrl")["CurrentDomain"];
                            imageData.Encryptedfilename = Guid.NewGuid() + model.ProfilePictureApp.FileName;
                            var content = new StringContent(JsonConvert.SerializeObject(new ImageModel { ImageData = fileByteArray, FileName = model.ProfilePictureApp.FileName, Name = model.ProfilePictureApp.Name, Encryptedfilename = imageData.Encryptedfilename }), Encoding.UTF8, "application/json");

                            using (var httpClient = new HttpClient())
                            {
                                var response = await httpClient.PostAsync(weburl + "User/SaveImage", content);
                                response.EnsureSuccessStatusCode();

                                emp.ProfilePicture = imageData.Encryptedfilename;

                            }

                        }

                        emp.FirstName = model.FirstName;
                        emp.LastName = model.LastName;
                        emp.PhoneNumberOffice = model.PhoneNumberOffice;
                        emp.PhoneNumber = model.PhoneNumber;
                        emp.City = model.City;
                        emp.ZipCode = model.ZipCode;
                        emp.State = model.State;
                        if (!string.IsNullOrEmpty(model.ProfilePicture))
                        {
                            emp.ProfilePicture = model.ProfilePicture;
                        }
                        emp.ModifiedOn = DateTime.Now;
                        var result = await _userManager.UpdateAsync(emp);

                    }

                }


                var user = _dbContext.User.Find(model.Id);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates basic info of contractors
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<User> UpdateBasicInfo(UpdateBasicInfo model)
        {
            try
            {

                var emp = _dbContext.User.Find(model.Id);

                if (emp != null)
                {
                    if (model.ProfilePictureApp != null)
                    {
                        ImageModel imageData = new ImageModel();
                        byte[] fileByteArray;
                        using (MemoryStream item = new MemoryStream())
                        {
                            model.ProfilePictureApp.CopyTo(item);
                            fileByteArray = item.ToArray(); //2nd change here
                        }
                        string weburl = _configuration.GetSection("PaymentUrl")["CurrentDomain"];
                        imageData.Encryptedfilename = Guid.NewGuid() + model.ProfilePictureApp.FileName;
                        var content = new StringContent(JsonConvert.SerializeObject(new ImageModel { ImageData = fileByteArray, FileName = model.ProfilePictureApp.FileName, Name = model.ProfilePictureApp.Name, Encryptedfilename = imageData.Encryptedfilename }), Encoding.UTF8, "application/json");

                        using (var httpClient = new HttpClient())
                        {
                            var response = await httpClient.PostAsync(weburl + "User/SaveImage", content);
                            response.EnsureSuccessStatusCode();

                            emp.ProfilePicture = imageData.Encryptedfilename;

                        }

                    }
                    if (emp.RoleId == Constant.HomeOwner)
                    {
                        emp.FirstName = model.FirstName;
                        emp.LastName = model.LastName;
                        emp.PhoneNumberOffice = model.PhoneNumberOffice;
                        emp.PhoneNumber = model.PhoneNumber;
                        emp.City = model.City;
                        emp.ZipCode = model.ZipCode;
                        emp.State = model.State;
                        if (!string.IsNullOrEmpty(model.ProfilePicture))
                        {
                            emp.ProfilePicture = model.ProfilePicture;
                        }
                        emp.ModifiedOn = DateTime.Now;
                        var result = await _userManager.UpdateAsync(emp);

                    }
                    else
                    {



                        emp.FirstName = model.FirstName;
                        emp.LastName = model.LastName;
                        emp.PhoneNumberOffice = model.PhoneNumberOffice;
                        emp.PhoneNumber = model.PhoneNumber;
                        emp.BusinessName = model.BusinessName;
                        emp.City = model.City;
                        emp.BusinessAddress = model.BusinessAddress;
                        emp.ZipCode = model.ZipCode;
                        emp.State = model.State;
                        if (!string.IsNullOrEmpty(model.ProfilePicture))
                        {
                            emp.ProfilePicture = model.ProfilePicture;
                        }
                        emp.ModifiedOn = DateTime.Now;
                        var result = await _userManager.UpdateAsync(emp);
                    }



                }


                var user = _dbContext.User.Find(model.Id);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<User> UpdateBasicInfoApp(UpdateBasicInfo model)
        {
            try
            {

                var emp = _dbContext.User.Find(model.Id);

                if (emp != null)
                {
                    if (model.ProfilePictureApp != null)
                    {
                        ImageModel imageData = new ImageModel();
                        byte[] fileByteArray;
                        using (MemoryStream item = new MemoryStream())
                        {
                            model.ProfilePictureApp.CopyTo(item);
                            fileByteArray = item.ToArray(); //2nd change here
                        }
                        string weburl = _configuration.GetSection("PaymentUrl")["CurrentDomain"];
                        imageData.Encryptedfilename = Guid.NewGuid() + model.ProfilePictureApp.FileName;
                        var content = new StringContent(JsonConvert.SerializeObject(new ImageModel { ImageData = fileByteArray, FileName = model.ProfilePictureApp.FileName, Name = model.ProfilePictureApp.Name, Encryptedfilename = imageData.Encryptedfilename }), Encoding.UTF8, "application/json");

                        using (var httpClient = new HttpClient())
                        {
                            var response = await httpClient.PostAsync(weburl + "User/SaveImage", content);
                            response.EnsureSuccessStatusCode();

                            emp.ProfilePicture = imageData.Encryptedfilename;

                        }

                    }
                    if (emp.RoleId == Constant.HomeOwner)
                    {
                        emp.FirstName = model.FirstName;
                        emp.LastName = model.LastName;
                        emp.PhoneNumberOffice = model.PhoneNumberOffice;
                        emp.PhoneNumber = model.PhoneNumber;
                        emp.City = model.City;
                        emp.ZipCode = model.ZipCode;
                        emp.State = model.State;
                        emp.ModifiedOn = DateTime.Now;
                        var result = await _userManager.UpdateAsync(emp);

                    }
                    else
                    {



                        emp.FirstName = model.FirstName;
                        emp.LastName = model.LastName;
                        emp.PhoneNumberOffice = model.PhoneNumberOffice;
                        emp.PhoneNumber = model.PhoneNumber;
                        emp.BusinessName = model.BusinessName;
                        emp.City = model.City;
                        emp.BusinessAddress = model.BusinessAddress;
                        emp.ZipCode = model.ZipCode;
                        emp.State = model.State;
                        emp.ModifiedOn = DateTime.Now;
                        var result = await _userManager.UpdateAsync(emp);
                    }



                }


                var user = _dbContext.User.Find(model.Id);
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }






        /// <summary>
        /// Gets user by email
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<User> GetUserByEmail(string Email)
        {
            try
            {

                var user = await _dbContext.User.Where(a => a.Email.Equals(Email) && a.IsEmailVerified == true && a.IsDeleted == false).FirstOrDefaultAsync();
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Checks if email exist or not.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> CheckEmailExist(string email)
        {
            try
            {
                bool isEmailExist = false;
                var result = await _iRepository.FindAllByCondition(a => a.Email.Equals(email) && a.IsEmailVerified == true);
                if (result.Count > 0)
                {
                    isEmailExist = true;
                }
                else
                {
                    isEmailExist = false;
                }
                return isEmailExist;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }



        /// <summary>
        /// Generates password reset token during forgot password.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return token;

        }
        /// <summary>
        /// resets password during forgot password.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="email"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        public async Task<Response> ResetPasswordAsync(string token, string email, string NewPassword)
        {
            Response response = new Response();
            var user = await _dbContext.User.Where(a => a.Email == email).FirstOrDefaultAsync();
            var PasswordChanged = await _userManager.ResetPasswordAsync(user, token, NewPassword);
            if (PasswordChanged.Succeeded)
            {
                response.Message = ResourceBusinessLayer.PasswordChanged;
                response.Statuscode = System.Net.HttpStatusCode.OK;
            }
            else
            {
                response.Message = PasswordChanged.Errors.Select(x => x.Description).First();
                response.Statuscode = System.Net.HttpStatusCode.BadRequest;

            }
            return response;
        }


        public async Task<long> GenerateUniquememberID(string StateId)
        {
            long UniqueMemberId = 0;
            string CurrentYear = string.Empty;
            DateTime dt = DateTime.Now;
            CurrentYear = dt.ToString("yy");
            long CurrentId = await _dbContext.UniqueMemberId.Select(x => x.MemberId).FirstOrDefaultAsync();
            var CurrentState = await _dbContext.State.Where(x => x.StateName.Contains(StateId)).FirstOrDefaultAsync();

            // var GeneratedId = CurrentYear + CurrentState.USAStateCode + CurrentId.ToString();

            var GeneratedId = CurrentYear + CurrentId.ToString();

            UniqueMemberId = (long)Convert.ToDouble(GeneratedId);
            return UniqueMemberId + 1;

        }

        public async Task<ZipcodesAndCategoriesViewModel> StatesCategoriesAndStatesList(int userid)
        {
            try
            {
                ZipcodesAndCategoriesViewModel model = new ZipcodesAndCategoriesViewModel();
                model.States = await _dbContext.State.ToListAsync();
                model.ZipCodes = await _dbContext.ZipCode.ToListAsync();
                model.Categories = await _dbContext.Categories.ToListAsync();
                var subscriptions = await _dbContext.UserMembershipSubscriptions.Where(x => x.UserId == userid).FirstOrDefaultAsync();

                var benifitsPlan = await _dbContext.MembershipBenefitsPlan.FindAsync(subscriptions.PlanId);

                if (benifitsPlan == null)
                {
                    benifitsPlan = new MembershipBenifitsPlan();
                }

                if (benifitsPlan != null && benifitsPlan?.Title == "Diamond")
                {
                    var customplan = await _dbContext.CustomZipcodesRequest.Where(x => x.UserId == userid).SingleOrDefaultAsync();
                    if (customplan != null)
                    {
                        model.LimitCategories = Convert.ToInt32(customplan.NumberOfCategories);
                        model.LimitZipCodes = Convert.ToInt32(customplan.NumberOfZipcodes);
                    }

                }
                else
                {
                    model.LimitCategories = Convert.ToInt32(benifitsPlan.Categories);
                    model.LimitZipCodes = Convert.ToInt32(benifitsPlan.ZipCodes);
                }

                model.PlanId = subscriptions.PlanId;

                model.UserMembershipCategories = await _dbContext.UserMembershipCategories.Where(x => x.UserId == userid).ToListAsync();
                model.UserMembershipZipcodes = await _dbContext.UserMembershipZipcodes.Where(x => x.UserId == userid).ToListAsync();

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Checks users subscribed zipcodes
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserZipcodes(User user)
        {
            try
            {
                bool ZipsIsThere = false;
                var zips = await _dbContext.UserMembershipZipcodes.Where(x => x.UserId == user.Id).ToListAsync();
                if (zips.Any())
                {
                    ZipsIsThere = true;
                }
                return ZipsIsThere;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CheckUserCategories(User user)
        {
            try
            {
                bool CatIsThere = false;
                var Cats = await _dbContext.UserMembershipCategories.Where(x => x.UserId == user.Id).ToListAsync();
                if (Cats.Any())
                {
                    CatIsThere = true;
                }
                return CatIsThere;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<bool> CheckZipsAndCatsOnUpgradeOrDowngrade(User user)
        {
            try
            {
                bool ZipsAndCatsAreThere = false;
                var Cats = await _dbContext.UserMembershipCategories.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                var memplan = await _dbContext.UserMembershipSubscriptions.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                if (Cats != null)
                {
                    if (Cats?.PlanId == memplan?.PlanId)
                    {
                        ZipsAndCatsAreThere = true;
                    }
                }

                return ZipsAndCatsAreThere;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CheckContractorsOnZipcode(int zipcode)
        {
            bool Avail = false;
            //  var zips=await _dbContext.ZipCode.Where(x=>x.Zipcode==zipcode).ToListAsync();

            var Contrs = await _dbContext.UserMembershipZipcodes.Where(x => x.ZipCodeId == zipcode.ToString()).ToListAsync();
            if (Contrs.Count() >= 10)
            {
                Avail = true;
            }





            return Avail;
        }

        public async Task<User> GetUserByID(int Id)
        {
            try
            {
                return await _dbContext.User.FindAsync(Id);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<ProhzReferral> GetReferralsById(int id)
        {
            try
            {
                return await _dbContext.ProhzReferral.FirstOrDefaultAsync(r => r.UserId == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public async Task<List<User>> GetUserList()
        {
            try
            {
                return await _dbContext.User.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<bool> DeleteUser(int Id)
        {
            try
            {
                var data = await _dbContext.User.FindAsync(Id);
                if (data != null)
                {
                    data.IsDeleted = true;
                    _dbContext.User.Update(data);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<bool> CheckReferralId(string referralId)
        {
            if (referralId != null)
            {
                try
                {
                    bool valid = false;

                    // Ensure that referralId can be converted to a long
                    if (long.TryParse(referralId, out long parsedReferralId))
                    {
                        var result = await _iRefferal.FindAllByCondition(a => a.MemberId == parsedReferralId);

                        if (result != null && result.Count() > 0) // Updated to use Count() for IEnumerable
                        {
                            valid = true;
                        }
                    }
                    else
                    {
                        _logger.LogError("Invalid referral ID format.");
                    }

                    return valid;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error checking referral ID.");
                    throw;
                }
            }
            else
            {
                return true; // Adjust this according to your logic for handling null referralId
            }
        }

    }

}





