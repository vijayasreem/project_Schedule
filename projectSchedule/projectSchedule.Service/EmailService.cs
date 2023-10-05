using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService : IEmailService
{
    private string smtpServer;
    private int smtpPort;
    private string smtpUsername;
    private string smtpPassword;
    private string ftpServer;
    private string ftpUsername;
    private string ftpPassword;
    private string sharepointUrl;

    public EmailService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword, string ftpServer, string ftpUsername, string ftpPassword, string sharepointUrl)
    {
        this.smtpServer = smtpServer;
        this.smtpPort = smtpPort;
        this.smtpUsername = smtpUsername;
        this.smtpPassword = smtpPassword;
        this.ftpServer = ftpServer;
        this.ftpUsername = ftpUsername;
        this.ftpPassword = ftpPassword;
        this.sharepointUrl = sharepointUrl;
    }

    public async Task SendEmailWithAttachment(string attachmentType)
    {
        // Get file path based on attachment type
        string filePath = GetFilePath(attachmentType);

        // Create email message
        MailMessage message = new MailMessage();
        message.From = new MailAddress(smtpUsername);
        message.To.Add(new MailAddress("recipient@example.com"));
        message.Subject = "Attachment Email";

        // Attach file
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            Attachment attachment = new Attachment(stream, Path.GetFileName(filePath));
            message.Attachments.Add(attachment);

            // Send email
            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                smtpClient.EnableSsl = true;
                await smtpClient.SendMailAsync(message);
            }
        }
    }

    public async Task SendFileToFTP(string attachmentType)
    {
        // Get file path based on attachment type
        string filePath = GetFilePath(attachmentType);

        // Upload file to FTP server
        using (WebClient ftpClient = new WebClient())
        {
            ftpClient.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            await ftpClient.UploadFileTaskAsync(ftpServer, filePath);
        }
    }

    public async Task SendFileToSharepoint(string attachmentType)
    {
        // Get file path based on attachment type
        string filePath = GetFilePath(attachmentType);

        // Upload file to Sharepoint
        using (FileStream fileStream = File.OpenRead(filePath))
        {
            var clientContext = new Microsoft.SharePoint.Client.ClientContext(sharepointUrl);
            var web = clientContext.Web;
            clientContext.Load(web);
            clientContext.ExecuteQuery();

            Microsoft.SharePoint.Client.File.SaveBinaryDirect(clientContext, "/Shared Documents/" + Path.GetFileName(filePath), fileStream, true);
        }
    }

    private string GetFilePath(string attachmentType)
    {
        // Get file path based on attachment type from database
        string connectionString = "your_connection_string";
        string query = "SELECT FilePath FROM AttachmentTable WHERE AttachmentType = @AttachmentType";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@AttachmentType", attachmentType);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetString(0);
                    }
                }
            }
        }

        return string.Empty;
    }
}