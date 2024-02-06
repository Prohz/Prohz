namespace KopkeHome_FMRS_API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using KopkeHome_UtilityLayer;
    using KopkeHome_ModelLayer.ViewModels;

    [Route("[controller]/[action]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmailToProhz([FromBody] EmailFormModel formData)
        {
            try
            {
                if (formData == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid model state or formData is null");
                }

                bool isEmailSent = _emailService.SendEmail("perryneal11@gmail.com", formData.Subject, formData.Message);

                if (isEmailSent)
                {
                    return Ok("Email sent successfully");
                }
                else
                {
                    return StatusCode(500, "Internal server error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }




    }
}
