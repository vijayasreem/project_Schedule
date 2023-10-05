public interface IEmailRequestService
{
    Task<List<EmailRequestModel>> GetAllAsync();
    Task<EmailRequestModel> GetByIdAsync(int id);
    Task<int> CreateAsync(EmailRequestModel emailRequest);
    Task UpdateAsync(EmailRequestModel emailRequest);
    Task DeleteAsync(int id);
    Task SendEmailAsync(string filePath, string recipientEmail);
    Task SendFileToFtpAsync(string filePath, string ftpUrl, string username, string password);
    Task SendFileToSharepointAsync(string filePath, string sharepointUrl, string username, string password);
}