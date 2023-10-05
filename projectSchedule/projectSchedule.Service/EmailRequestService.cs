using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using projectSchedule.DataAccess;
using projectSchedule.DTO;

namespace projectSchedule.Service
{
    public class EmailRequestService : IEmailRequestService
    {
        private readonly IEmailRequestRepository _repository;

        public EmailRequestService(IEmailRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EmailRequestModel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<EmailRequestModel> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> CreateAsync(EmailRequestModel emailRequest)
        {
            return await _repository.CreateAsync(emailRequest);
        }

        public async Task UpdateAsync(EmailRequestModel emailRequest)
        {
            await _repository.UpdateAsync(emailRequest);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task SendEmailAsync(string filePath, string recipientEmail)
        {
            // Code to send email using SMTP
        }

        public async Task SendFileToFtpAsync(string filePath, string ftpUrl, string username, string password)
        {
            // Code to send file to FTP
        }

        public async Task SendFileToSharepointAsync(string filePath, string sharepointUrl, string username, string password)
        {
            // Code to send file to SharePoint
        }
    }
}