using System;
using System.Collections.Generic;
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

        public async Task<List<EmailRequestModel>> GetAllAsync()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM EmailRequests", connection);
                var reader = await command.ExecuteReaderAsync();

                var emailRequests = new List<EmailRequestModel>();
                while (await reader.ReadAsync())
                {
                    emailRequests.Add(new EmailRequestModel
                    {
                        Id = (int)reader["Id"],
                        FileType = (string)reader["FileType"],
                        DestinationType = (string)reader["DestinationType"]
                    });
                }

                return emailRequests;
            }
        }

        public async Task<EmailRequestModel> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM EmailRequests WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                var reader = await command.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return new EmailRequestModel
                    {
                        Id = (int)reader["Id"],
                        FileType = (string)reader["FileType"],
                        DestinationType = (string)reader["DestinationType"]
                    };
                }

                return null;
            }
        }

        public async Task<int> CreateAsync(EmailRequestModel emailRequest)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("INSERT INTO EmailRequests (FileType, DestinationType) VALUES (@FileType, @DestinationType); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@FileType", emailRequest.FileType);
                command.Parameters.AddWithValue("@DestinationType", emailRequest.DestinationType);

                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
        }

        public async Task UpdateAsync(EmailRequestModel emailRequest)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("UPDATE EmailRequests SET FileType = @FileType, DestinationType = @DestinationType WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@FileType", emailRequest.FileType);
                command.Parameters.AddWithValue("@DestinationType", emailRequest.DestinationType);
                command.Parameters.AddWithValue("@Id", emailRequest.Id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM EmailRequests WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task SendEmailAsync(string filePath, string recipientEmail)
        {
            // TODO: Implement sending email with attachment using SmtpClient
        }

        public async Task SendFileToFtpAsync(string filePath, string ftpUrl, string username, string password)
        {
            // TODO: Implement sending file to FTP server
        }

        public async Task SendFileToSharepointAsync(string filePath, string sharepointUrl, string username, string password)
        {
            // TODO: Implement sending file to SharePoint
        }
    }
}