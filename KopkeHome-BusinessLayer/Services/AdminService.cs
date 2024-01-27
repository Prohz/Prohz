using AutoMapper;
using KopkeHome_BusinessLayer.Interface;
using KopkeHome_DataAccessLayer;
using KopkeHome_DataAccessLayer.Repository;
using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.AdminViewModels;
using KopkeHome_UtilityLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using XAct;

namespace KopkeHome_BusinessLayer.Services
{
#pragma warning disable

    public class AdminService : IAdmin
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<User> _iRepository;
        private readonly IEmailService _email;
        private readonly ApplicationDbContext _dbContext;
        private readonly IRepository<VerifyOTP> _OtpRepository;
        private readonly IMapper Mapper;
        private readonly ILogger<AccountService> _logger;
        public AdminService(ILogger<AccountService> logger, UserManager<User> userManager, IRepository<User> repository, IEmailService email,
             ApplicationDbContext dbContext, IRepository<VerifyOTP> OtpRepository, IMapper mapper)
        {

            _userManager = userManager;
            _iRepository = repository;
            _email = email;
            _dbContext = dbContext;
            this.Mapper = mapper;
            _OtpRepository = OtpRepository;
            _logger = logger;

        }


        /// <summary>
        /// add FAQ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> AddFAQ(FAQ model)
        {

            try
            {
                Response response = new Response();
                model.IsActive = true;
                await _dbContext.FAQ.AddAsync(model);
                var save = await _dbContext.SaveChangesAsync();
                if (save == 1)
                {
                    response.Statuscode = System.Net.HttpStatusCode.OK; ;
                    response.Message = Message.Faq;
                    response.Status = Message.Success;
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// custom membership plan list
        /// </summary>
        /// <returns></returns>
        public async Task<List<CustomMembershipPlanRequestViewModel>> CustomMembershipPlanList()
        {
            try
            {
                List<CustomMembershipPlanRequestViewModel> model = new List<CustomMembershipPlanRequestViewModel>();
                var Requestdata = _dbContext.CustomZipcodesRequest.ToList();
                foreach (var request in Requestdata)
                {
                    CustomMembershipPlanRequestViewModel mod=new CustomMembershipPlanRequestViewModel();
                    var user= await _dbContext.User.FindAsync(request.UserId);
                    if (user!=null)
                    { 
                    mod.CustomZipcodesRequest= request;
                    mod.UserName = user.FirstName + " " + user.LastName;
                    mod.HomePhone = user.PhoneNumber;
                    mod.OfficePhone = user.PhoneNumberOffice;
                    mod.Email = user.Email; 
                    model.Add(mod);
                    }
                }
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// DeletePAQ
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Response> DeleteFAQ(int Id)
        {
            try
            {
                Response response = new Response();
                var faq = await _dbContext.FAQ.FindAsync(Id);
                if (faq != null)
                {
                    faq.IsActive = false;
                    _dbContext.FAQ.Update(faq);
                    var s = await _dbContext.SaveChangesAsync();
                    if (s == 1)
                    {
                        response.Statuscode = System.Net.HttpStatusCode.OK;
                        response.Message = Message.FaqDelete;
                        response.Status = Message.Success;
                    }

                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// get FAQ by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<FAQ> GetFAQById(int Id)
        {
            try
            {

                var faq = await _dbContext.FAQ.FindAsync(Id);

                return faq;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Get all FAQ
        /// </summary>
        /// <returns></returns>

        public Task<List<FAQ>> GetAllFAQ()
        {
            try
            {
                return _dbContext.FAQ.Where(x => x.IsActive == true).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// get document list for verification
        /// </summary>
        /// <returns></returns>

        public List<DocumentsVerificationViewModels> GetDocumntsListForVerification()
        {
            try
            {
                List<DocumentsVerificationViewModels> ViewModel = new List<DocumentsVerificationViewModels>();
                //    ViewModel.ContractorsBusinessProfile   =_dbContext.BusinessProfile.ToListAsync();
                var res = (from biztable in _dbContext.BusinessProfile
                           join UserTable in _dbContext.User on biztable.UserId equals UserTable.Id
                           where biztable.VerificationStatus != VerificationStatusConstant.Verified
                           orderby UserTable.Id
                           select new
                           {
                               UserTable.Id,
                               UserTable.Email,
                               UserTable.FirstName,
                               UserTable.LastName,
                               biztable.ProfilePicture,
                               biztable.WorkmanCompensationInsuranceFile,
                               biztable.LiabilityInsuranceFile,
                               biztable.BusinessOrTradeLicenseFiles,
                               biztable.VerificationStatus,

                           }).ToList();

                var resOtherContractors = (from biztable in _dbContext.BusinessProfileSubContractors
                                           join UserTable in _dbContext.User on biztable.UserId equals UserTable.Id
                                           where biztable.VerificationStatus != VerificationStatusConstant.Verified
                                           orderby UserTable.Id
                                           select new
                                           {
                                               UserTable.Id,
                                               UserTable.Email,
                                               UserTable.FirstName,
                                               UserTable.LastName,
                                               biztable.ProfilePicture,
                                               biztable.WorkmanCompensationInsuranceFile,
                                               biztable.LiabilityInsuranceFile,
                                               biztable.BusinessOrTradeLicenseFiles,
                                               biztable.VerificationStatus,

                                           }).ToList();

                foreach (var r in res)
                {
                    DocumentsVerificationViewModels Model = new DocumentsVerificationViewModels();
                    Model.Id = r.Id;
                    Model.FirstName = r.FirstName;
                    Model.LastName = r.LastName;
                    Model.ProfilePicture = r.ProfilePicture;
                    Model.LiabilityInsuranceFile = r.LiabilityInsuranceFile;
                    Model.WorkmanCompensationInsuranceFile = r.WorkmanCompensationInsuranceFile;
                    Model.BusinessOrTradeLicenseFiles = r.BusinessOrTradeLicenseFiles;
                    Model.VerificationStatus = r.VerificationStatus;
                    Model.Email = r.Email;
                    ViewModel.Add(Model);
                }
                foreach (var r in resOtherContractors)
                {
                    DocumentsVerificationViewModels Model = new DocumentsVerificationViewModels();
                    Model.Id = r.Id;
                    Model.FirstName = r.FirstName;
                    Model.LastName = r.LastName;
                    Model.ProfilePicture = r.ProfilePicture;
                    Model.LiabilityInsuranceFile = r.LiabilityInsuranceFile;
                    Model.WorkmanCompensationInsuranceFile = r.WorkmanCompensationInsuranceFile;
                    Model.BusinessOrTradeLicenseFiles = r.BusinessOrTradeLicenseFiles;
                    Model.VerificationStatus = r.VerificationStatus;
                    Model.Email = r.Email;
                    ViewModel.Add(Model);
                }

                return ViewModel;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        /// <summary>
        /// update document verification
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> UpdateDocumentVerification(DocumentsVerificationStatusUpdateViewModel model)
        {
            try
            {
                Response response = new Response();
                var user = await _dbContext.User.FindAsync(model.UserId);
                if (user != null)
                {
                    if (user.RoleId == Constant.Contractor)
                    {
                        var contarctor = await _dbContext.BusinessProfile.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                        contarctor.VerificationStatus = model.VerificationStatus;
                        if (model.VerificationStatus == VerificationStatusConstant.Verified)
                        {
                            user.IsDocumentsVerified = true;
                            _dbContext.User.Update(user);
                            await _dbContext.SaveChangesAsync();
                        }
                        _dbContext.BusinessProfile.Update(contarctor);
                        var Res = await _dbContext.SaveChangesAsync();
                        if (Res == 1)
                        {
                            response.Statuscode = System.Net.HttpStatusCode.OK;
                        }

                    }
                    else if (user.RoleId != Constant.HomeOwner)
                    {
                        var othercontarctor = await _dbContext.BusinessProfileSubContractors.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                        othercontarctor.VerificationStatus = model.VerificationStatus;
                        if (model.VerificationStatus == VerificationStatusConstant.Verified)
                        {
                            user.IsDocumentsVerified = true;
                            _dbContext.User.Update(user);
                            await _dbContext.SaveChangesAsync();
                        }
                        _dbContext.BusinessProfileSubContractors.Update(othercontarctor);
                        var Res = await _dbContext.SaveChangesAsync();
                        if (Res == 1)
                        {
                            response.Statuscode = System.Net.HttpStatusCode.OK;
                        }
                    }

                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Update FAQ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> UpdateFAQ(FAQ model)
        {
            try
            {
                Response response = new Response();
                var faq = await _dbContext.FAQ.FindAsync(model.Id);
                if (faq != null)
                {
                    faq.IsActive = true;
                    faq.Question = model.Question;
                    faq.Answer = model.Answer;

                    _dbContext.FAQ.Update(faq);
                    var s = await _dbContext.SaveChangesAsync();
                    if (s == 1)
                    {
                        response.Statuscode = System.Net.HttpStatusCode.OK;
                        response.Message = Message.FaqUpdate;
                        response.Status = Message.Success;
                    }

                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CustomZipcodesRequest> GetCustomReqById(int Id)
        {
            try
            {
                var usercstmplanReq = await _dbContext.CustomZipcodesRequest.FindAsync(Id);

                return usercstmplanReq;
            }
            catch (Exception eex)
            {

                throw;
            }
        }
        public async Task<CustomZipcodesRequest> GetCustomReqByUserId(int Id)
        {
            try
            {
                var usercstmplanReq = await _dbContext.CustomZipcodesRequest.Where(x => x.UserId == Id).FirstOrDefaultAsync();

                return usercstmplanReq;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// add promo videos
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> AddPromoVideos(PromoVideos model)
        {
            try
            {
                Response response = new Response();
                model.IsActive = true;
                await _dbContext.PromoVideos.AddAsync(model);
                var save = await _dbContext.SaveChangesAsync();
                if (save == 1)
                {
                    response.Statuscode = System.Net.HttpStatusCode.OK; ;
                    response.Message = Message.PromoInsert;
                    response.Status = Message.Success;
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// delete promovideos
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Response> DeletePromoVideo(int Id)
        {
            try
            {
                Response response = new Response();
                var PromoVideos = await _dbContext.PromoVideos.FindAsync(Id);
                if (PromoVideos != null)
                {
                    PromoVideos.IsActive = false;
                    _dbContext.PromoVideos.Update(PromoVideos);
                    var s = await _dbContext.SaveChangesAsync();
                    if (s == 1)
                    {
                        response.Statuscode = System.Net.HttpStatusCode.OK;
                        response.Message = Message.PromoDelete;
                        response.Status = Message.Success;
                    }

                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// get promo video by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<PromoVideos> GetPromoVideoById(int Id)
        {
            try
            {

                var PromoVideo = await _dbContext.PromoVideos.FindAsync(Id);

                return PromoVideo;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Update promo video
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> UpdatePromoVideo(PromoVideos model)
        {
            try
            {
                Response response = new Response();
                var PromoVideo = await _dbContext.PromoVideos.FindAsync(model.Id);
                if (PromoVideo != null)
                {
                    PromoVideo.IsActive = true;
                    PromoVideo.FileName = model.FileName;
                    PromoVideo.FilePath = model.FilePath;
                    PromoVideo.OriginalName = model.OriginalName;

                    _dbContext.PromoVideos.Update(PromoVideo);
                    var s = await _dbContext.SaveChangesAsync();
                    if (s == 1)
                    {
                        response.Statuscode = System.Net.HttpStatusCode.OK;
                        response.Message = Message.PromoUpdate;
                        response.Status = Message.Success;
                    }

                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Get all promo videos
        /// </summary>
        /// <returns></returns>
        public Task<List<PromoVideos>> GetAllPromoVideos()
        {
            try
            {
                return _dbContext.PromoVideos.Where(x => x.IsActive == true).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// get dashboard status
        /// </summary>
        /// <returns></returns>
        public async Task<AdminDashboardViewModel> GetDashboardStatus()
        {
            try
            {
                AdminDashboardViewModel model=new AdminDashboardViewModel();
                model.NumberOfActiveUsers = await _dbContext.Users.Where(x => x.RoleId != 5).CountAsync();
                model.NumberOfHomeOwners =await _dbContext.Users.Where(x => x.RoleId ==4).CountAsync();
                model.NumberOfContractors =await _dbContext.Users.Where(x => x.RoleId==1 || x.RoleId==2 || x.RoleId==3).CountAsync();

                model.SubscriptionPurchased =await _dbContext.UserMembershipSubscriptions.Where(x => x.IsActive==true).CountAsync();

                model.User = await _dbContext.User.Where(x => x.IsEmailVerified == true && x.RoleId != 5).ToListAsync();

                var result = await _dbContext.User.Join(_dbContext.Roles, s => s.RoleId, fg => fg.Id, (s, fg) => new { s, fg })
                                  .Where(x => x.s.IsEmailVerified == true && x.s.RoleId!=5)
                                  .Select(x => new RoleName
                                  {
                                     UserType=x.fg.Name,
                                     FirstName=x.s.FirstName,
                                     LastName=x.s.LastName,
                                     Email=x.s.Email,
                                     BusinessAddress=x.s.BusinessAddress,
                                     PhoneNumber=x.s.PhoneNumber,
                                     PhoneNumberOffice=x.s.PhoneNumberOffice,
                                     UniqueMemberId=x.s.UniqueMemberId,
                                     Id=x.s.Id,

                                  }).ToListAsync();
                model.RoleName=result;


                return model;

            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// add category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> AddCategory(Categories model)
        {
            try
            {
                Response response = new Response();
               // model.IsActive = true;
               var  exist=await _dbContext.Categories.Where(x=>x.Name==model.Name).FirstOrDefaultAsync();
                if (exist != null)
                {
                    response.Statuscode = System.Net.HttpStatusCode.AlreadyReported; ;
                    response.Message = Message.CheckCategory;
                    response.Status = Message.Failed;
                    return response;
                }
                await _dbContext.Categories.AddAsync(model);
                var save = await _dbContext.SaveChangesAsync();
                if (save == 1)
                {
                    response.Statuscode = System.Net.HttpStatusCode.OK; ;
                    response.Message = Message.CategoryInsert;
                    response.Status = Message.Success;
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
        

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Response> DeleteCategory(int Id)
        {
            try
            {
                Response response = new Response();
                var categories = await _dbContext.Categories.FindAsync(Id);
                if (categories != null)
                {
                   
                    _dbContext.Categories.Remove(categories);
                    var s = await _dbContext.SaveChangesAsync();
                    if (s == 1)
                    {
                        response.Statuscode = System.Net.HttpStatusCode.OK;
                        response.Message = Message.CategoryDelete;
                        response.Status = Message.Success;
                    }

                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// get category by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Categories> GetCategoryById(int Id)
        {
            try
            {
                var cat = await _dbContext.Categories.FindAsync(Id);

                return cat;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> UpdateCategory(Categories model)
        {
            try
            {
                Response response = new Response();
                var Category = await _dbContext.Categories.FindAsync(model.Id);
                if (Category != null)
                {
                    Category.Name = model.Name;
                   

                    _dbContext.Categories.Update(Category);
                    var s = await _dbContext.SaveChangesAsync();
                    if (s == 1)
                    {
                        response.Statuscode = System.Net.HttpStatusCode.OK;
                        response.Message = Message.CategoryUpdate;
                        response.Status = Message.Success;
                    }

                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// get all category
        /// </summary>
        /// <returns></returns>

        public async Task<List<Categories>> GetAllCategories()
        {
            try
            {
                return await _dbContext.Categories.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// get all prohz sales person
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProhzSalesAssciates>> GetAllProhzSalesPerson()
        {
            try
            {
                var Member= await _dbContext.ProhzSalesAssciates.ToListAsync();
                return Member;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// add prohz legal files
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Response> AddProhzLegalFiles(ProhzLegalFiles model)
        {
            try
            {

                Response response=new Response ();  
                var UpdateExist=await _dbContext.ProhzLegalFiles.Where(x=>x.FileType==model.FileType).FirstOrDefaultAsync();
                if (UpdateExist!=null)
                {

                    UpdateExist.FileType = model.FileType;
                    UpdateExist.FilePath=model.FilePath;
                    UpdateExist.Name=model.Name;
                    var data =  _dbContext.ProhzLegalFiles.Update(UpdateExist);
                   var ess=await _dbContext.SaveChangesAsync();
                    if (ess==1)
                    {
                        response.Statuscode = System.Net.HttpStatusCode.OK;
                        response.Data = UpdateExist;
                    }
                }
                else
                {

                
              var filess=  await _dbContext.ProhzLegalFiles.AddAsync(model);
               await _dbContext.SaveChangesAsync();
                    if (filess != null)
                    {
                        response.Statuscode = System.Net.HttpStatusCode.OK;
                        response.Data = filess;
                    }
                }
               
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// get all prohz legal files
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProhzLegalFiles>> GetAllProhzLegalFiles()
        {
            try
            {
                return await _dbContext.ProhzLegalFiles.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
