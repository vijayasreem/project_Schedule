
using Microsoft.AspNetCore.Mvc;
using projectSchedule.DTO;
using projectSchedule.Service;
using System.Threading.Tasks;

namespace projectSchedule.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("smtp")]
        public async Task<IActionResult> SendEmailWithAttachment([FromForm] string attachmentType)
        {
            await _emailService.SendEmailWithAttachment(attachmentType);
            return Ok();
        }

        [HttpPost("ftp")]
        public async Task<IActionResult> SendFileToFTP([FromForm] string attachmentType)
        {
            await _emailService.SendFileToFTP(attachmentType);
            return Ok();
        }

        [HttpPost("sharepoint")]
        public async Task<IActionResult> SendFileToSharepoint([FromForm] string attachmentType)
        {
            await _emailService.SendFileToSharepoint(attachmentType);
            return Ok();
        }
    }
}
