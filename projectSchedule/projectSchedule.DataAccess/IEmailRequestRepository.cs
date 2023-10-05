
using System.Collections.Generic;
using System.Threading.Tasks;
using projectSchedule.DTO;

namespace projectSchedule.Service
{
    public interface IEmailRequestRepository
    {
        Task<int> CreateAsync(EmailRequestModel model);
        Task<EmailRequestModel> GetByIdAsync(int id);
        Task<List<EmailRequestModel>> GetAllAsync();
        Task UpdateAsync(EmailRequestModel model);
        Task DeleteAsync(int id);
        Task SendEmailWithAttachmentAsync(EmailRequestModel model, string filePath);
        Task SendFileToFtpAsync(EmailRequestModel model, string filePath);
        Task SendFileToSharepointAsync(EmailRequestModel model, string filePath);
    }
}
