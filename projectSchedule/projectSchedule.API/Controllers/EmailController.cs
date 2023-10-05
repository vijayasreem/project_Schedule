
using Microsoft.AspNetCore.Mvc;
using projectSchedule.DTO;
using projectSchedule.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace projectSchedule.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRequestRepository _emailRequestRepository;

        public EmailController(IEmailRequestRepository emailRequestRepository)
        {
            _emailRequestRepository = emailRequestRepository;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(int id, string emailSubject, string emailBody, string attachmentPath)
        {
            try
            {
                await _emailRequestRepository.SendEmailAsync(id, emailSubject, emailBody, attachmentPath);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("SendFileToFtp")]
        public async Task<IActionResult> SendFileToFtp(int id, string filePath, string ftpServerUrl, string username, string password)
        {
            try
            {
                await _emailRequestRepository.SendFileToFtpAsync(id, filePath, ftpServerUrl, username, password);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("SendFileToSharepoint")]
        public async Task<IActionResult> SendFileToSharepoint(int id, string filePath, string sharepointUrl, string username, string password)
        {
            try
            {
                await _emailRequestRepository.SendFileToSharepointAsync(id, filePath, sharepointUrl, username, password);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
