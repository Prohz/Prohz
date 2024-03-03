using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.APIRequestModels;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KopkeHome_FMRS_API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {

        private readonly IMembership _membershipService;
        private readonly ILogger<MembershipController> _logger;
        public MembershipController(IMembership membership, ILogger<MembershipController> logger)
        {
            _membershipService = membership;
            _logger = logger;
        }

        /// <summary>
        /// This method gets all Membership Benifits Plan.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<MembershipBenifitsPlan>> GetMembershipPlans()
        {
            try
            {

                var MembershipPlans = await _membershipService.GetMembershipPlans();
                return MembershipPlans;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// save membership zipcodes and categories
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> SaveMembershipZipcodesAndCategories(MembershipZipcodesAndCategoriesRequestModel Model)
        {
            try
            {

                var MembershipPlans = await _membershipService.AddZipcodeAndCategories(Model);
                return MembershipPlans;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// save custom zipcoderequest
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> SaveCustomZipcodeRequest(CustomZipcodesRequest Model)
        {
            try
            {
                Response res = new Response();

                var MembershipPlans = await _membershipService.SaveCustomZipcodeRequest(Model);
                if (MembershipPlans == null)
                {
                    res.Statuscode = System.Net.HttpStatusCode.BadRequest;
                }
                return res;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }

        /// <summary>
        /// get zipcodes by city name
        /// </summary>
        /// <param name="CityName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<GetZipCodesByCityNameViewModel>> GetZipCodesByCityName([FromForm] string CityName, [FromForm] string StateName)
        {
            try
            {

                var ZipCodesList = await _membershipService.GetZipCodesByCityName(CityName, StateName);
                return ZipCodesList;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }


        /// <summary>
        /// get custom plan details by userid
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CustomZipcodesRequest> GetCustomPlanDetailsByUserId([FromForm] string UserId)
        {
            try
            {

                var member = await _membershipService.GetCustomPlanDetailsByUserId(Convert.ToInt32(UserId));
                return member;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }

    }
}
