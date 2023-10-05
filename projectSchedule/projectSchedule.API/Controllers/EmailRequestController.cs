
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectSchedule.DTO;
using projectSchedule.Service;

namespace projectSchedule.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailRequestController : ControllerBase
    {
        private readonly IEmailRequestService _emailRequestService;

        public EmailRequestController(IEmailRequestService emailRequestService)
        {
            _emailRequestService = emailRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmailRequestModel>>> GetAllAsync()
        {
            var emailRequests = await _emailRequestService.GetAllAsync();
            return Ok(emailRequests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmailRequestModel>> GetByIdAsync(int id)
        {
            var emailRequest = await _emailRequestService.GetByIdAsync(id);
            if (emailRequest == null)
            {
                return NotFound();
            }
            return Ok(emailRequest);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAsync(EmailRequestModel emailRequest)
        {
            var id = await _emailRequestService.CreateAsync(emailRequest);
            return Ok(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, EmailRequestModel emailRequest)
        {
            if (id != emailRequest.Id)
            {
                return BadRequest();
            }
            await _emailRequestService.UpdateAsync(emailRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _emailRequestService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmailAsync(string filePath, string recipientEmail)
        {
            await _emailRequestService.SendEmailAsync(filePath, recipientEmail);
            return Ok();
        }

        [HttpPost("send-ftp")]
        public async Task<IActionResult> SendFileToFtpAsync(string filePath, string ftpUrl, string username, string password)
        {
            await _emailRequestService.SendFileToFtpAsync(filePath, ftpUrl, username, password);
            return Ok();
        }

        [HttpPost("send-sharepoint")]
        public async Task<IActionResult> SendFileToSharepointAsync(string filePath, string sharepointUrl, string username, string password)
        {
            await _emailRequestService.SendFileToSharepointAsync(filePath, sharepointUrl, username, password);
            return Ok();
        }
    }
}
