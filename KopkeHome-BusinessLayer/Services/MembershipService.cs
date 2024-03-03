using Dapper;
using KopkeHome_BusinessLayer.Interface;
using KopkeHome_DataAccessLayer;
using KopkeHome_DataAccessLayer.GenericRepository;
using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.APIRequestModels;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KopkeHome_BusinessLayer.Services
{
#nullable disable
    public class MembershipService : IMembership
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<MembershipService> _logger;
        private readonly IGenericRepository _GenericRepository;
        public MembershipService(ILogger<MembershipService> logger, ApplicationDbContext dbContext, IGenericRepository GenericRepository)
        {
            _dbContext = dbContext;
            _logger = logger;
            _GenericRepository = GenericRepository;

        }

        public async Task<Response> AddZipcodeAndCategories(MembershipZipcodesAndCategoriesRequestModel model)
        {
            try
            {
                Response Response = new Response();
                IEnumerable<UserMembershipZipcodes> usersZips = await _dbContext.UserMembershipZipcodes.Where(x => x.UserId == Convert.ToInt32(model.UserId)).ToListAsync();
                IEnumerable<UserMembershipCategories> usersCats = await _dbContext.UserMembershipCategories.Where(x => x.UserId == Convert.ToInt32(model.UserId)).ToListAsync();

                if (usersZips.Any())
                {
                    // Use Remove Range function to delete all records at once
                    _dbContext.UserMembershipZipcodes.RemoveRange(usersZips);

                    await _dbContext.SaveChangesAsync();
                }
                if (usersCats.Any())
                {
                    // Use Remove Range function to delete all records at once
                    _dbContext.UserMembershipCategories.RemoveRange(usersCats);
                    await _dbContext.SaveChangesAsync();
                }

                foreach (var Zipcode in model.Zipcodes)
                {
                    UserMembershipZipcodes tableZip = new UserMembershipZipcodes();
                    var IsZipcodeExiest = await _dbContext.UserMembershipZipcodes.Where(x => x.ZipCodeId ==Zipcode && x.UserId == Convert.ToInt32(model.UserId)).FirstOrDefaultAsync().ConfigureAwait(false);
                    if (IsZipcodeExiest == null)
                    {
                        tableZip.ZipCodeId = Zipcode;
                        tableZip.UserId = Convert.ToInt32(model.UserId);
                        tableZip.PlanId = model.PlanId;

                        _dbContext.UserMembershipZipcodes.Add(tableZip);
                        await _dbContext.SaveChangesAsync();
                    }

                }
                foreach (var category in model.Categories)
                {
                    UserMembershipCategories TableCat = new UserMembershipCategories();
                    var IsCategoryExiest = await _dbContext.UserMembershipCategories.Where(x => x.CategoriesId == Convert.ToInt32(category) && x.UserId == Convert.ToInt32(model.UserId)).FirstOrDefaultAsync().ConfigureAwait(false);
                    if (IsCategoryExiest == null)
                    {
                        TableCat.CategoriesId = Convert.ToInt32(category);
                        TableCat.UserId = Convert.ToInt32(model.UserId);
                        TableCat.PlanId = model.PlanId;
                        _dbContext.UserMembershipCategories.Add(TableCat);
                        await _dbContext.SaveChangesAsync();

                    }

                }

                return Response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public async Task<CustomZipcodesRequest> GetCustomPlanDetailsByUserId(int UserId)
        {
            try
            {
                var data= await _dbContext.CustomZipcodesRequest.Where(x=>x.UserId== UserId).FirstOrDefaultAsync().ConfigureAwait(false);
                    if (data != null )
                {
                    return data;
                }
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<MembershipBenifitsPlan>> GetMembershipPlans()
        {
            try
            {
                return await _dbContext.MembershipBenefitsPlan.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public async Task<MembershipBenifitsPlan> GetMembershipPlansById(int Id)
        {
            try
            {
                return await _dbContext.MembershipBenefitsPlan.FindAsync(Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        public async Task<List<GetZipCodesByCityNameViewModel>> GetZipCodesByCityName(string CityName, string StateName)
        {
            try
            {
                List<GetZipCodesByCityNameViewModel> model = new List<GetZipCodesByCityNameViewModel>();

                string CitySaitized = CityName.ToLower();
                string StateSaitized = StateName.ToLower();
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@CityName", CitySaitized);
                ObjParm.Add("@StateName", StateSaitized);

                var result = _GenericRepository.GetEntities<GetZipCodesByCityNameViewModel>("Udp_GetZipcodesListByCityName", ObjParm);
                model = result.ToList();
                return model;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<CustomZipcodesRequest> SaveCustomZipcodeRequest(CustomZipcodesRequest model)
        {
            try
            {
                var isexiest = await _dbContext.CustomZipcodesRequest.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                if (isexiest == null)
                {
                    await _dbContext.CustomZipcodesRequest.AddAsync(model);
                    await _dbContext.SaveChangesAsync();
                    return model;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<CustomZipcodesRequest> UpdateCustomZipcodeRequest(CustomZipcodesRequest model)
        {
            try
            {
                var restable = await _dbContext.CustomZipcodesRequest.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                if (restable != null)
                {
                    restable.PriceMonthly = model.PriceMonthly;
                    restable.PriceYearly = model.PriceYearly;
                    restable.StripePriceMonthly = model.StripePriceMonthly;
                    restable.StripePriceYearly = model.StripePriceYearly;
                    restable.NumberOfZipcodes = model.NumberOfZipcodes;
                    restable.NumberOfCategories = model.NumberOfCategories;
                    restable.MobileApp = model.MobileApp;
                    restable.WebApp = model.WebApp;
                    restable.IsPlanCreated = true;
                    _dbContext.CustomZipcodesRequest.Update(restable);
                    await _dbContext.SaveChangesAsync();
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
