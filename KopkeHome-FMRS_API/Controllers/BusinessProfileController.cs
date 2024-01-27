using KopkeHome_FMRS_API.Properties;
using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.DataModel;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KopkeHome_FMRS_API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BusinessProfileController : ControllerBase
    {
        private readonly IBusinessProfile service;
        private readonly ILogger<BusinessProfileController> _logger;
        public BusinessProfileController(ILogger<BusinessProfileController> logger, IBusinessProfile _service)
        {
            service = _service;
            _logger = logger;

        }

        /// <summary>
        /// Creates business profile of contractors.
        /// </summary>
        /// <param name="businessProfile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> BusinessProfileForContractor(BusinessProfileDataModel businessProfile)
        {
            try
            {
                Response response = new Response();
                if (!ModelState.IsValid)
                {
                    response.Error = true;
                    response.Status = Resources.NOT_FOUND;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = Resources.ALREADY_REG;
                    return BadRequest(response);
                }
                else
                {
                    var result = await service.BusinessProfile(businessProfile);
                    if (result != null)
                    {
                        response.Error = false;
                        response.Status = Resources.SuccessMsg;
                        response.Statuscode = HttpStatusCode.OK;
                        response.Message = Resources.SavedMsg ;
                        response.Data = result;
                    }
                    else
                    {
                        response.Error = true;
                        response.Status = Resources.NOT_FOUND;
                        response.Statuscode = HttpStatusCode.NotFound;
                        response.Message = Resources.NOT_FOUND;
                        response.Data = result;
                    }

                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// Updates business profile of Contractors.
        /// </summary>
        /// <param name="businessProfile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateBusinessProfileForContractor(BusinessProfileDataModel businessProfile)
        {
            try
            {
                Response response = new Response();
                if (!ModelState.IsValid)
                {
                    response.Error = true;
                    response.Status = Resources.NOT_FOUND;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = Resources.ALREADY_REG;
                    return BadRequest(response);
                }
                else
                {
                    var result = await service.UpdateBusinessProfile(businessProfile);
                    if (result != null)
                    {
                        response.Error = false;
                        response.Status = Resources.SUCCESS;
                        response.Statuscode = HttpStatusCode.OK;
                        response.Message = Resources.DataUpdated;
                        response.Data = result;
                    }
                    else
                    {
                        response.Error = true;
                        response.Status = Resources.NOT_FOUND;
                        response.Statuscode = HttpStatusCode.NotFound;
                        response.Message = Resources.NOT_FOUND;
                        response.Data = result;
                    }

                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }




        /// <summary>
        /// Creates business profile of Other Contractors.
        /// </summary>
        /// <param name="businessProfile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> BusinessProfileSubContractor(BusinessProfileSubContractor businessProfile)
        {
            try
            {
                Response response = new Response();
                if (!ModelState.IsValid)
                {
                    response.Error = true;
                    response.Status = Resources.NOT_FOUND;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = Resources.ALREADY_REG;
                    return BadRequest(response);
                }
                else
                {

                    var result = await service.BusinessProfileSubContractor(businessProfile);
                    if (result != null)
                    {
                        response.Error = false;
                        response.Status = Resources.SUCCESS;
                        response.Statuscode = HttpStatusCode.OK;
                        response.Message = Resources.DataSaved;
                        response.Data = result;
                    }
                    else
                    {
                        response.Error = true;
                        response.Status = Resources.NOT_FOUND;
                        response.Statuscode = HttpStatusCode.NotFound;
                        response.Message = Resources.NOT_FOUND;
                        response.Data = result;
                    }

                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// Update Business Profile Of Other Contractors 
        /// </summary>
        /// <param name="businessProfile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpdateBusinessProfileOfOtherContractors(BusinessProfileSubContractor businessProfile)
        {
            try
            {
                Response response = new Response();
                if (!ModelState.IsValid)
                {
                    response.Error = true;
                    response.Status = Resources.NOT_FOUND;
                    response.Statuscode = HttpStatusCode.NotFound;
                    response.Message = Resources.ALREADY_REG;
                    return BadRequest(response);
                }
                else
                {

                    var result = await service.UpdateBusinessProfileSubContractor(businessProfile);
                    if (result != null)
                    {
                        response.Error = false;
                        response.Status = Resources.SuccessMsg;
                        response.Statuscode = HttpStatusCode.OK;
                        response.Message = Resources.DataUpdated;
                        response.Data = result;
                    }
                    else
                    {
                        response.Error = true;
                        response.Status = Resources.NOT_FOUND;
                        response.Statuscode = HttpStatusCode.NotFound;
                        response.Message = Resources.NOT_FOUND;
                        response.Data = result;
                    }

                    return Ok(response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
    }
}
