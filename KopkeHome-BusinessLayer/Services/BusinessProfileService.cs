using AutoMapper;
using KopkeHome_BusinessLayer.Interface;
using KopkeHome_DataAccessLayer;
using KopkeHome_DataAccessLayer.Repository;
using KopkeHome_ModelLayer.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KopkeHome_BusinessLayer.Services
{

    public class BusinessProfileService : IBusinessProfile
    {

        private readonly IRepository<BusinessProfileDataModel> _iRepository;
        private readonly IRepository<BusinessProfileSubContractor> _iRepositorySubContractor;
        private readonly ILogger<BusinessProfileService> _logger;
        private readonly ApplicationDbContext _dbContext;


        private readonly IMapper Mapper;
        public BusinessProfileService(IRepository<BusinessProfileSubContractor> iRepositorySubContractor, ApplicationDbContext dbContext, ILogger<BusinessProfileService> logger, IRepository<BusinessProfileDataModel> repository, IMapper mapper)
        {
            _iRepository = repository;
            _logger = logger;
            this.Mapper = mapper;
            _iRepositorySubContractor = iRepositorySubContractor;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates business profile of contractors
        /// </summary>
        /// <param name="businessProfile"></param>
        /// <returns></returns>
        public async Task<BusinessProfileDataModel> BusinessProfile(BusinessProfileDataModel businessProfile)
        {
            try
            {

                BusinessProfileDataModel model = this.Mapper.Map<BusinessProfileDataModel>(businessProfile);
                model.CreatedOn = DateTime.Now;
                return await _iRepository.Add(model);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        /// <summary>
        /// Updates business profile of contarctors.
        /// </summary>
        /// <param name="userDataModel"></param>
        /// <returns></returns>
        public async Task<BusinessProfileDataModel> UpdateBusinessProfile(BusinessProfileDataModel userDataModel)
        {
            try
            {

                BusinessProfileDataModel model = this.Mapper.Map<BusinessProfileDataModel>(userDataModel);
                model.ModifiedOn = DateTime.Now;
                var AprovedOrNot = await _dbContext.BusinessProfile.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                if (AprovedOrNot != null)
                {
                    if (AprovedOrNot.VerificationStatus == 3)
                    {
                        model.VerificationStatus = 3;
                    }
                }

                return await _iRepository.Update(model);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }
        /// <summary>
        /// Creates business profile of other contractors.
        /// </summary>
        /// <param name="businessProfile"></param>
        /// <returns></returns>
        public async Task<BusinessProfileSubContractor> BusinessProfileSubContractor(BusinessProfileSubContractor businessProfile)
        {
            try
            {

                BusinessProfileSubContractor model = this.Mapper.Map<BusinessProfileSubContractor>(businessProfile);
                model.CreatedOn = DateTime.Now;
                return await _iRepositorySubContractor.Add(model);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }


        /// <summary>
        /// Updates business profile of other contractors
        /// </summary>
        /// <param name="userDataModel"></param>
        /// <returns></returns>
        public async Task<BusinessProfileSubContractor> UpdateBusinessProfileSubContractor(BusinessProfileSubContractor userDataModel)
        {
            try
            {

                BusinessProfileSubContractor model = this.Mapper.Map<BusinessProfileSubContractor>(userDataModel);
                model.ModifiedOn = new DateTime();
                var AprovedOrNot = await _dbContext.BusinessProfileSubContractors.Where(x => x.UserId == model.UserId).FirstOrDefaultAsync();
                if (AprovedOrNot != null)
                {
                    if (AprovedOrNot.VerificationStatus == 3)
                    {
                        model.VerificationStatus = 3;
                    }
                }
                return await _iRepositorySubContractor.Update(model);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }
    }
}


