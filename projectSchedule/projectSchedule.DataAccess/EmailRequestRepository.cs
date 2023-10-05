using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using projectSchedule.DTO;

namespace projectSchedule
{
    public class EmailRequestRepository : IEmailRequestRepository
    {
        private readonly string connectionString;

        public EmailRequestRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // CRUD operations

        public async Task<int> CreateAsync(EmailRequestModel emailRequest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO EmailRequests (AttachedFileType, DestinationType) VALUES (@AttachedFileType, @DestinationType); SELECT SCOPE_IDENTITY();";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AttachedFileType", emailRequest.AttachedFileType);
                    command.Parameters.AddWithValue("@DestinationType", emailRequest.DestinationType);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<EmailRequestModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM EmailRequests WHERE Id = @Id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new EmailRequestModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                AttachedFileType = reader["AttachedFileType"].ToString(),
                                DestinationType = reader["DestinationType"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public async Task<List<EmailRequestModel>> GetAllAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM EmailRequests;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<EmailRequestModel> emailRequests = new List<EmailRequestModel>();

                        while (await reader.ReadAsync())
                        {
                            emailRequests.Add(new EmailRequestModel
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                AttachedFileType = reader["AttachedFileType"].ToString(),
                                DestinationType = reader["DestinationType"].ToString()
                            });
                        }

                        return emailRequests;
                    }
                }
            }
        }

        public async Task<int> UpdateAsync(EmailRequestModel emailRequest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "UPDATE EmailRequests SET AttachedFileType = @AttachedFileType, DestinationType = @DestinationType WHERE Id = @Id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AttachedFileType", emailRequest.AttachedFileType);
                    command.Parameters.AddWithValue("@DestinationType", emailRequest.DestinationType);
                    command.Parameters.AddWithValue("@Id", emailRequest.Id);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "DELETE FROM EmailRequests WHERE Id = @Id;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        // Sending methods

        public async Task SendEmailAsync(int id, string emailSubject, string emailBody, string attachmentPath)
        {
            EmailRequestModel emailRequest = await GetByIdAsync(id);

            if (emailRequest != null && emailRequest.DestinationType == "smtp")
            {
                using (SmtpClient smtpClient = new SmtpClient())
                {
                    // Configure SMTP client settings

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        // Configure mail message settings

                        if (!string.IsNullOrEmpty(attachmentPath))
                        {
                            mailMessage.Attachments.Add(new Attachment(attachmentPath));
                        }

                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
        }

        public async Task SendFileToFtpAsync(int id, string filePath, string ftpServerUrl, string username, string password)
        {
            EmailRequestModel emailRequest = await GetByIdAsync(id);

            if (emailRequest != null && emailRequest.DestinationType == "ftp")
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Credentials = new NetworkCredential(username, password);

                    await webClient.UploadFileTaskAsync(ftpServerUrl, filePath);
                }
            }
        }

        public async Task SendFileToSharepointAsync(int id, string filePath, string sharepointUrl, string username, string password)
        {
            EmailRequestModel emailRequest = await GetByIdAsync(id);

            if (emailRequest != null && emailRequest.DestinationType == "sharepoint")
            {
                // Connect to SharePoint and upload the file
            }
        }
    }
}