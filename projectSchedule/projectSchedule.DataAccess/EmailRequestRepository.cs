using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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

        public async Task<int> CreateAsync(EmailRequestModel model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("INSERT INTO EmailRequests (AttachedFileType, DestinationType) VALUES (@AttachedFileType, @DestinationType); SELECT SCOPE_IDENTITY();", connection))
                {
                    command.Parameters.AddWithValue("@AttachedFileType", model.AttachedFileType);
                    command.Parameters.AddWithValue("@DestinationType", model.DestinationType);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<EmailRequestModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT Id, AttachedFileType, DestinationType FROM EmailRequests WHERE Id = @Id;", connection))
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

                using (SqlCommand command = new SqlCommand("SELECT Id, AttachedFileType, DestinationType FROM EmailRequests;", connection))
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

        public async Task UpdateAsync(EmailRequestModel model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("UPDATE EmailRequests SET AttachedFileType = @AttachedFileType, DestinationType = @DestinationType WHERE Id = @Id;", connection))
                {
                    command.Parameters.AddWithValue("@AttachedFileType", model.AttachedFileType);
                    command.Parameters.AddWithValue("@DestinationType", model.DestinationType);
                    command.Parameters.AddWithValue("@Id", model.Id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("DELETE FROM EmailRequests WHERE Id = @Id;", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task SendEmailWithAttachmentAsync(EmailRequestModel model, string filePath)
        {
            // Code to send email with attachment
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("from@example.com");
                mail.To.Add("to@example.com");
                mail.Subject = "Email with Attachment";
                mail.Body = "This is a test email with attachment";

                Attachment attachment = new Attachment(filePath);
                mail.Attachments.Add(attachment);

                using (SmtpClient smtp = new SmtpClient("smtp.example.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("username", "password");
                    smtp.EnableSsl = true;

                    await smtp.SendMailAsync(mail);
                }
            }
        }

        public async Task SendFileToFtpAsync(EmailRequestModel model, string filePath)
        {
            // Code to send file to FTP server
            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential("username", "password");
                await client.UploadFileTaskAsync("ftp://ftp.example.com/file.txt", WebRequestMethods.Ftp.UploadFile, filePath);
            }
        }

        public async Task SendFileToSharepointAsync(EmailRequestModel model, string filePath)
        {
            // Code to send file to SharePoint
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                // Upload file to SharePoint
            }
        }
    }
}