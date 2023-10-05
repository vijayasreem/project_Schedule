using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using projectSchedule.DTO;

namespace projectSchedule.Repository
{
    public class SchedulingToolRepository : ISchedulingToolService
    {
        private readonly string connectionString;

        public SchedulingToolRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<int> CreateAsync(SchedulingToolModel schedulingTool)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO SchedulingTool (DeliveryMethods, EmailAddresses, TemplateSubject, TemplateBody, FTPURL, Password, FilePath, SharepointURL, DocumentLibrary, Frequency, SpecificDay, TimeOfDay, FileTypeTransformation, SuccessEmailAddresses, SuccessTemplateSubject, SuccessTemplateBody)
                                 VALUES (@DeliveryMethods, @EmailAddresses, @TemplateSubject, @TemplateBody, @FTPURL, @Password, @FilePath, @SharepointURL, @DocumentLibrary, @Frequency, @SpecificDay, @TimeOfDay, @FileTypeTransformation, @SuccessEmailAddresses, @SuccessTemplateSubject, @SuccessTemplateBody);
                                 SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DeliveryMethods", string.Join(",", schedulingTool.DeliveryMethods));
                    command.Parameters.AddWithValue("@EmailAddresses", string.Join(",", schedulingTool.Email.EmailAddresses));
                    command.Parameters.AddWithValue("@TemplateSubject", schedulingTool.Email.TemplateSubject);
                    command.Parameters.AddWithValue("@TemplateBody", schedulingTool.Email.TemplateBody);
                    command.Parameters.AddWithValue("@FTPURL", schedulingTool.FTP.FTPURL);
                    command.Parameters.AddWithValue("@Password", schedulingTool.FTP.Password);
                    command.Parameters.AddWithValue("@FilePath", schedulingTool.FTP.FilePath);
                    command.Parameters.AddWithValue("@SharepointURL", schedulingTool.Sharepoint.SharepointURL);
                    command.Parameters.AddWithValue("@DocumentLibrary", schedulingTool.Sharepoint.DocumentLibrary);
                    command.Parameters.AddWithValue("@Frequency", schedulingTool.DeliverySchedule.Frequency);
                    command.Parameters.AddWithValue("@SpecificDay", schedulingTool.DeliverySchedule.SpecificDay);
                    command.Parameters.AddWithValue("@TimeOfDay", schedulingTool.DeliverySchedule.TimeOfDay);
                    command.Parameters.AddWithValue("@FileTypeTransformation", schedulingTool.FileTypeTransformation);
                    command.Parameters.AddWithValue("@SuccessEmailAddresses", string.Join(",", schedulingTool.SuccessNotifications.EmailAddresses));
                    command.Parameters.AddWithValue("@SuccessTemplateSubject", schedulingTool.SuccessNotifications.TemplateSubject);
                    command.Parameters.AddWithValue("@SuccessTemplateBody", schedulingTool.SuccessNotifications.TemplateBody);

                    await connection.OpenAsync();
                    var id = await command.ExecuteScalarAsync();
                    return Convert.ToInt32(id);
                }
            }
        }

        public async Task<SchedulingToolModel> GetByIdAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM SchedulingTool WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.Read())
                    {
                        return MapToSchedulingToolModel(reader);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public async Task<List<SchedulingToolModel>> GetAllAsync()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM SchedulingTool;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    List<SchedulingToolModel> schedulingTools = new List<SchedulingToolModel>();

                    while (reader.Read())
                    {
                        schedulingTools.Add(MapToSchedulingToolModel(reader));
                    }

                    return schedulingTools;
                }
            }
        }

        public async Task<bool> UpdateAsync(SchedulingToolModel schedulingTool)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"UPDATE SchedulingTool 
                                 SET DeliveryMethods = @DeliveryMethods,
                                     EmailAddresses = @EmailAddresses,
                                     TemplateSubject = @TemplateSubject,
                                     TemplateBody = @TemplateBody,
                                     FTPURL = @FTPURL,
                                     Password = @Password,
                                     FilePath = @FilePath,
                                     SharepointURL = @SharepointURL,
                                     DocumentLibrary = @DocumentLibrary,
                                     Frequency = @Frequency,
                                     SpecificDay = @SpecificDay,
                                     TimeOfDay = @TimeOfDay,
                                     FileTypeTransformation = @FileTypeTransformation,
                                     SuccessEmailAddresses = @SuccessEmailAddresses,
                                     SuccessTemplateSubject = @SuccessTemplateSubject,
                                     SuccessTemplateBody = @SuccessTemplateBody
                                 WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DeliveryMethods", string.Join(",", schedulingTool.DeliveryMethods));
                    command.Parameters.AddWithValue("@EmailAddresses", string.Join(",", schedulingTool.Email.EmailAddresses));
                    command.Parameters.AddWithValue("@TemplateSubject", schedulingTool.Email.TemplateSubject);
                    command.Parameters.AddWithValue("@TemplateBody", schedulingTool.Email.TemplateBody);
                    command.Parameters.AddWithValue("@FTPURL", schedulingTool.FTP.FTPURL);
                    command.Parameters.AddWithValue("@Password", schedulingTool.FTP.Password);
                    command.Parameters.AddWithValue("@FilePath", schedulingTool.FTP.FilePath);
                    command.Parameters.AddWithValue("@SharepointURL", schedulingTool.Sharepoint.SharepointURL);
                    command.Parameters.AddWithValue("@DocumentLibrary", schedulingTool.Sharepoint.DocumentLibrary);
                    command.Parameters.AddWithValue("@Frequency", schedulingTool.DeliverySchedule.Frequency);
                    command.Parameters.AddWithValue("@SpecificDay", schedulingTool.DeliverySchedule.SpecificDay);
                    command.Parameters.AddWithValue("@TimeOfDay", schedulingTool.DeliverySchedule.TimeOfDay);
                    command.Parameters.AddWithValue("@FileTypeTransformation", schedulingTool.FileTypeTransformation);
                    command.Parameters.AddWithValue("@SuccessEmailAddresses", string.Join(",", schedulingTool.SuccessNotifications.EmailAddresses));
                    command.Parameters.AddWithValue("@SuccessTemplateSubject", schedulingTool.SuccessNotifications.TemplateSubject);
                    command.Parameters.AddWithValue("@SuccessTemplateBody", schedulingTool.SuccessNotifications.TemplateBody);
                    command.Parameters.AddWithValue("@Id", schedulingTool.Id);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"DELETE FROM SchedulingTool WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    return rowsAffected > 0;
                }
            }
        }

        private SchedulingToolModel MapToSchedulingToolModel(SqlDataReader reader)
        {
            return new SchedulingToolModel
            {
                Id = Convert.ToInt32(reader["Id"]),
                DeliveryMethods = ((string)reader["DeliveryMethods"]).Split(',').ToList(),
                Email = new EmailModel
                {
                    EmailAddresses = ((string)reader["EmailAddresses"]).Split(',').ToList(),
                    TemplateSubject = (string)reader["TemplateSubject"],
                    TemplateBody = (string)reader["TemplateBody"]
                },
                FTP = new FTPModel
                {
                    FTPURL = (string)reader["FTPURL"],
                    Password = (string)reader["Password"],
                    FilePath = (string)reader["FilePath"]
                },
                Sharepoint = new SharepointModel
                {
                    SharepointURL = (string)reader["SharepointURL"],
                    DocumentLibrary = (string)reader["DocumentLibrary"]
                },
                DeliverySchedule = new DeliveryScheduleModel
                {
                    Frequency = (string)reader["Frequency"],
                    SpecificDay = (string)reader["SpecificDay"],
                    TimeOfDay = (string)reader["TimeOfDay"]
                },
                FileTypeTransformation = (string)reader["FileTypeTransformation"],
                SuccessNotifications = new SuccessNotificationsModel
                {
                    EmailAddresses = ((string)reader["SuccessEmailAddresses"]).Split(',').ToList(),
                    TemplateSubject = (string)reader["SuccessTemplateSubject"],
                    TemplateBody = (string)reader["SuccessTemplateBody"]
                }
            };
        }
    }
}