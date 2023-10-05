using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using projectSchedule.DataAccess;
using projectSchedule.DTO;

namespace projectSchedule.Service
{
    public class EmailRequestService : IEmailRequestRepository
    {
        private readonly IDataAccess _dataAccess;

        public EmailRequestService(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public async Task<int> CreateAsync(EmailRequestModel emailRequest)
        {
            // Implementation to create email request in the database
            int id = await _dataAccess.CreateEmailRequestAsync(emailRequest);
            
            return id;
        }

        public async Task<EmailRequestModel> GetByIdAsync(int id)
        {
            // Implementation to get email request by id from the database
            EmailRequestModel emailRequest = await _dataAccess.GetEmailRequestByIdAsync(id);
            
            return emailRequest;
        }

        public async Task<List<EmailRequestModel>> GetAllAsync()
        {
            // Implementation to get all email requests from the database
            List<EmailRequestModel> emailRequests = await _dataAccess.GetAllEmailRequestsAsync();
            
            return emailRequests;
        }

        public async Task<int> UpdateAsync(EmailRequestModel emailRequest)
        {
            // Implementation to update email request in the database
            int updatedRows = await _dataAccess.UpdateEmailRequestAsync(emailRequest);
            
            return updatedRows;
        }

        public async Task<int> DeleteAsync(int id)
        {
            // Implementation to delete email request from the database
            int deletedRows = await _dataAccess.DeleteEmailRequestAsync(id);
            
            return deletedRows;
        }

        public async Task SendEmailAsync(int id, string emailSubject, string emailBody, string attachmentPath)
        {
            // Implementation to send email with attachment using SMTP
            EmailRequestModel emailRequest = await _dataAccess.GetEmailRequestByIdAsync(id);
            
            // Send email using SMTP
            // Implementation code here
        }

        public async Task SendFileToFtpAsync(int id, string filePath, string ftpServerUrl, string username, string password)
        {
            // Implementation to send file to FTP server
            EmailRequestModel emailRequest = await _dataAccess.GetEmailRequestByIdAsync(id);
            
            // Send file to FTP server
            // Implementation code here
        }

        public async Task SendFileToSharepointAsync(int id, string filePath, string sharepointUrl, string username, string password)
        {
            // Implementation to send file to Sharepoint
            EmailRequestModel emailRequest = await _dataAccess.GetEmailRequestByIdAsync(id);
            
            // Send file to Sharepoint
            // Implementation code here
        }
    }
}