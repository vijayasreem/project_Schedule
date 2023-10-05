public interface IEmailRequestRepository
{
    Task<int> CreateAsync(EmailRequestModel emailRequest);
    Task<EmailRequestModel> GetByIdAsync(int id);
    Task<List<EmailRequestModel>> GetAllAsync();
    Task<int> UpdateAsync(EmailRequestModel emailRequest);
    Task<int> DeleteAsync(int id);
    Task SendEmailAsync(int id, string emailSubject, string emailBody, string attachmentPath);
    Task SendFileToFtpAsync(int id, string filePath, string ftpServerUrl, string username, string password);
    Task SendFileToSharepointAsync(int id, string filePath, string sharepointUrl, string username, string password);
}