using KopkeHome_BusinessLayer.Interface;
using KopkeHome_DataAccessLayer;
using KopkeHome_DataAccessLayer.GenericRepository;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;
using KopkeHome_UtilityLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KopkeHome_BusinessLayer.Services
{
#nullable disable
    public class DashboardService : IDashboard
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<DashboardService> _logger;
        private readonly IGenericRepository _GenericRepository;
        public DashboardService(ILogger<DashboardService> logger, ApplicationDbContext dbContext, IGenericRepository GenericRepository)
        {
            _dbContext = dbContext;
            _logger = logger;
            _GenericRepository = GenericRepository;
        }
        /// <summary>
        /// Gets zipcodes list
        /// </summary>
        /// <param name="Prefix"></param>
        /// <returns></returns>
        public async Task<List<ZipCode>> GetZipCodeList(string Prefix, int UserID)
        {
            try
            {
                List<ZipCode> zips = new List<ZipCode>();

               var ZipCode = await _dbContext.UserMembershipZipcodes.Where(X => X.ZipCodeId.Contains(Prefix) && X.UserId == UserID).Distinct().ToListAsync();
                if (ZipCode.Any())
                {
                    foreach (var zipCode in ZipCode)
                    {
                        ZipCode model = new ZipCode();
                        var ziponmebership = await _dbContext.UserMembershipZipcodes.Where(x => x.ZipCodeId == zipCode.ZipCodeId && x.UserId == UserID).FirstOrDefaultAsync();
                        if (ziponmebership != null)
                        {
                            model.Zipcode = ziponmebership.ZipCodeId;
                            model.Id = Convert.ToInt32(ziponmebership.ZipCodeId);
                            zips.Add(model);
                        }


                    }

                }


                return zips;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        /// <summary>
        /// Gets Categories List
        /// </summary>
        /// <param name="Prefix"></param>
        /// <returns></returns>
        ///
        public async Task<List<Categories>> GetCategoriesList(string Prefix, string zipcode)
        {
            try
            {
                int zipcodes = Convert.ToInt32(zipcode);
                List<Categories> CategoriesList = new List<Categories>();

                var CatList = await _dbContext.Categories.Where(X => X.Name.StartsWith(Prefix)).ToListAsync();
                foreach (var Category in CatList)
                {
                    Categories model = new Categories();

                    var zipcodeMebership = await _dbContext.UserMembershipZipcodes.Where(x => x.ZipCodeId ==zipcode).Select(s=>s.UserId).Distinct().ToListAsync();

                    foreach (var membersid in zipcodeMebership)
                    {
                        var CategoryMebership = await _dbContext.UserMembershipCategories.Include("Categories").Where(x => x.CategoriesId == Category.Id && x.UserId == membersid).FirstOrDefaultAsync();

                        if (CategoryMebership != null)
                        {
                            model.Name = CategoryMebership.Categories.Name.TrimEnd();
                            model.Id = CategoryMebership.Categories.Id;

                            CategoriesList.Add(model);
                        }
                    }
                }
                return CategoriesList.Distinct().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Gets Contractor Profile Details by id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<ContractorProfileDetailsViewModel> GetContractorProfileDetails(int Id)
        {
            try
            {
                List<Categories> Categories = new List<Categories>();
                List<ZipCode> ZipCodes = new List<ZipCode>();
                ContractorProfileDetailsViewModel model = new ContractorProfileDetailsViewModel();
                var User = await _dbContext.User.FindAsync(Id);

                if (User != null)
                {
                    model.User = User;
                    if (User.RoleId == Constant.Contractor)
                    {
                        var a = await _dbContext.BusinessProfile.Where(x => x.UserId == User.Id).FirstOrDefaultAsync();
                        if (a != null)
                        {
                            model.BusinessProfileContractor = a;
                        }
                    }
                    else if (User.RoleId == Constant.SubContractor || User.RoleId == Constant.IndependentContractor)
                    {
                        var othercontractors = await _dbContext.BusinessProfileSubContractors.Where(x => x.UserId == User.Id).FirstOrDefaultAsync();
                        if (othercontractors != null)
                        {
                            model.SubContractorBusinessProfile = othercontractors;
                        }

                    }

                    var UserMembershipCategories = await _dbContext.UserMembershipCategories.Where(z => z.UserId == model.User.Id).ToListAsync();
                    var UserMembershipZipcodes = await _dbContext.UserMembershipZipcodes.Where(z => z.UserId == model.User.Id).ToListAsync();
                    if (UserMembershipCategories.Any())
                    {
                        foreach (var category in UserMembershipCategories)
                        {
                            var cat = await _dbContext.Categories.Where(x => x.Id == category.CategoriesId).FirstOrDefaultAsync();
                            if (cat != null)
                            {
                                Categories.Add(cat);
                            }


                        }
                        model.Categories = Categories;


                    }
                    if (UserMembershipZipcodes.Any())
                    {
                        foreach (var zipcode in UserMembershipZipcodes)
                        {

                            ZipCode modelzip = new ZipCode();
                            modelzip.Zipcode = zipcode.ZipCodeId;
                            ZipCodes.Add(modelzip);



                        }
                        model.Zipcodes = ZipCodes;
                    }
                    var images = await _dbContext.WorkGallery.Where(x => x.UserId == model.User.Id).Select(s => s.ImageUrl).FirstOrDefaultAsync();
                    if (images != null)
                    {
                        model.WorkGallery = images;
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
        /// Gets Contractors list
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetContractors()
        {
            try
            {

                var ContractorsList = await _dbContext.User.Where(x => x.RoleId == Constant.Contractor).ToListAsync();

                return ContractorsList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Search Contractors List on behalf of zipcode id and category id
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<List<ContractorListViewModel>> SearchContractorsList(SearchContractorsViewModel model)
        {

            try
            {
                List<ContractorListViewModel> contract = new List<ContractorListViewModel>();
                var CategoryName = await _dbContext.Categories.Where(x => x.Name.Contains(model.Categories)).FirstOrDefaultAsync();
                var ZipCode = await _dbContext.UserMembershipZipcodes.Where(x => x.ZipCodeId==model.ZipCode).FirstOrDefaultAsync();

                if (ZipCode != null && CategoryName != null)
                {

                    var zipzz = await _dbContext.UserMembershipZipcodes.Where(x => x.ZipCodeId == ZipCode.ZipCodeId).Select(p => p.UserId).ToListAsync();

                    var catzz = await _dbContext.UserMembershipCategories.Where(x => x.CategoriesId == CategoryName.Id).Select(p => p.UserId).ToListAsync();

                    if (zipzz.Any() && catzz.Any())
                    {
                        var list = zipzz.Concat(catzz)
                                   .ToList();
                        IEnumerable<int> ContractorsList = list.GroupBy(x => x)
                                           .Where(g => g.Count() > 1)
                                           .Select(x => x.Key);

                        if (ContractorsList.Any())
                        {
                            var CurrentuserType = await _dbContext.User.FindAsync(model.UserId);

                            //using for loop
                            for (int i = 0; i < ContractorsList.Count(); i++)
                            {
                                ContractorListViewModel VM = new ContractorListViewModel();
                                var Contarctor = await _dbContext.User.FindAsync(ContractorsList.ElementAt(i));
                                if (Contarctor.RoleId == Constant.Admin)
                                {
                                    break;
                                }

                                //Getting the contarctors from contarctor review table. so that we can order them while displaying on dashboard index page.
                                var contractorsReview = await _dbContext.ContractorsReview.Where(x => x.UserId == model.UserId && x.ContractorId == Contarctor.Id).FirstOrDefaultAsync();
                                if (contractorsReview != null)
                                {
                                    if (contractorsReview.IsLiked)
                                    {
                                        VM.IsLiked = true;
                                    }
                                    else
                                    {
                                        VM.IsDisLiked = true;
                                    }
                                }

                                if (Contarctor.RoleId == Constant.Contractor)
                                {
                                    var BusinessDetailsz = await _dbContext.BusinessProfile.Where(x => x.UserId == ContractorsList.ElementAt(i) && x.VerificationStatus == VerificationStatusConstant.Verified).FirstOrDefaultAsync();
                                    if (BusinessDetailsz == null)
                                    {
                                        continue;
                                    }

                                    VM.ProfilePic = BusinessDetailsz.ProfilePicture;
                                    VM.ContractorType = "Contractor";
                                }
                                else if (Contarctor.RoleId != Constant.Admin)
                                {
                                    var BusinessDetailsz = await _dbContext.BusinessProfileSubContractors.Where(x => x.UserId == ContractorsList.ElementAt(i) && x.VerificationStatus == VerificationStatusConstant.Verified).FirstOrDefaultAsync();
                                    if (BusinessDetailsz == null)
                                    {
                                        continue;
                                    }

                                    VM.ProfilePic = BusinessDetailsz.ProfilePicture;
                                    if (Contarctor.RoleId == Constant.SubContractor)
                                    {
                                        VM.ContractorType = "Sub-Contractor";
                                    }
                                    else
                                    {
                                        VM.ContractorType = "Independent Contractor";
                                    }
                                }


                                VM.Category = CategoryName.Name;

                                VM.Id = Contarctor.Id;
                                VM.Name = Contarctor.FirstName + " " + Contarctor.LastName;
                                VM.Email = Contarctor.Email;
                                VM.CompanyName = Contarctor.BusinessName;
                                VM.RoleId = Contarctor.RoleId;
                                VM.Mobile = Contarctor.PhoneNumber;
                                VM.OfficeNumber = Contarctor.PhoneNumberOffice;
                                VM.WorkStatus = Contarctor.WorkStatus;
                                VM.WorkStatusUpdated = Contarctor.WorkStatusModifiedOn;
                                contract.Add(VM);
                            }
                        }

                    }
                }
                else
                {
                    return contract;
                }


                //var IsLikedContractorsOnTop = contract.OrderByDescending(x => x.IsLiked == true).ThenBy(x=>x.IsDisLiked==false).ToList();
                var IsLikedContractorsOnTop = contract.OrderBy(x => x.WorkStatus).ThenByDescending(x => x.WorkStatusUpdated).ToList();
                var nextFilter = IsLikedContractorsOnTop.OrderByDescending(x => x.IsLiked == true).ToList();
                //var ak= nextFilter.RemoveAll(x=>x.IsDisLiked==true);
                return nextFilter;



            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// This method saves contractors review based on logged in user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ContractorsReviewViewModel> ContractorsReview(ContractorsReviewViewModel model)
        {
            try
            {


                var review = await _dbContext.ContractorsReview.Where(x => x.UserId == model.UserId && x.ContractorId == model.ContractorId).FirstOrDefaultAsync();
                if (review != null)
                {
                    if (model.IsLiked)
                    {
                        review.IsLiked = true;
                        review.ContractorId = model.ContractorId;
                        review.UserId = model.UserId;

                        _dbContext.ContractorsReview.Update(review);
                        await _dbContext.SaveChangesAsync();
                    }
                    else if (model.IsDisLiked)
                    {
                        review.IsLiked = false;
                        review.ContractorId = model.ContractorId;
                        review.UserId = model.UserId;

                        _dbContext.ContractorsReview.Update(review);
                        await _dbContext.SaveChangesAsync();
                    }
                    else if (model.IsDisLiked == false && model.IsLiked == false)
                    {
                        _dbContext.ContractorsReview.Remove(review);
                        await _dbContext.SaveChangesAsync();
                    }



                }
                else
                {

                    if (model.IsLiked)
                    {
                        var contract = new ContractorsReview
                        {
                            ContractorId = model.ContractorId,
                            // Comments = model.Comments,
                            UserId = model.UserId,
                            IsLiked = true,
                        };
                        await _dbContext.ContractorsReview.AddAsync(contract);
                        await _dbContext.SaveChangesAsync();
                    }
                    else if (model.IsDisLiked)
                    {
                        var contract = new ContractorsReview
                        {
                            ContractorId = model.ContractorId,
                            // Comments = model.Comments,
                            UserId = model.UserId,
                            IsLiked = false,
                        };
                        await _dbContext.ContractorsReview.AddAsync(contract);
                        await _dbContext.SaveChangesAsync();
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
    }
}
