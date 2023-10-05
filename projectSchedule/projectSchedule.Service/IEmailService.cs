public interface IEmailService
{
    Task SendEmailWithAttachment(string attachmentType);
    Task SendFileToFTP(string attachmentType);
    Task SendFileToSharepoint(string attachmentType);
}