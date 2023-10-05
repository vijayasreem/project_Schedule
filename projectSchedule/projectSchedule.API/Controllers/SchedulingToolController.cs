
using Microsoft.AspNetCore.Mvc;
using projectSchedule.DTO;
using projectSchedule.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace projectSchedule.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulingToolController : ControllerBase
    {
        private readonly ISchedulingToolService _schedulingToolService;

        public SchedulingToolController(ISchedulingToolService schedulingToolService)
        {
            _schedulingToolService = schedulingToolService;
        }

        [HttpPost]
        public async Task<IActionResult> ConfigureReportScheduling([FromBody] SchedulingToolRequest request)
        {
            try
            {
                await _schedulingToolService.ConfigureReportScheduling(request.DeliveryMethods, request.EmailAddresses, request.FtpUrl, request.FtpPassword, request.FtpFilePath, request.SharepointUrl, request.SharepointDocumentLibrary, request.FileType, request.DeliverySchedule, request.SuccessNotificationEmails);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
