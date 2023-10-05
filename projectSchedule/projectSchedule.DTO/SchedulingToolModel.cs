
namespace projectSchedule
{
    public class SchedulingToolModel
    {
        public int Id { get; set; }
        public List<string> DeliveryMethods { get; set; }
        public EmailModel Email { get; set; }
        public FTPModel FTP { get; set; }
        public SharepointModel Sharepoint { get; set; }
        public DeliveryScheduleModel DeliverySchedule { get; set; }
        public string FileTypeTransformation { get; set; }
        public SuccessNotificationsModel SuccessNotifications { get; set; }
    }

    public class EmailModel
    {
        public List<string> EmailAddresses { get; set; }
        public string TemplateSubject { get; set; }
        public string TemplateBody { get; set; }
    }

    public class FTPModel
    {
        public string FTPURL { get; set; }
        public string Password { get; set; }
        public string FilePath { get; set; }
    }

    public class SharepointModel
    {
        public string SharepointURL { get; set; }
        public string DocumentLibrary { get; set; }
    }

    public class DeliveryScheduleModel
    {
        public string Frequency { get; set; }
        public string SpecificDay { get; set; }
        public string TimeOfDay { get; set; }
    }

    public class SuccessNotificationsModel
    {
        public List<string> EmailAddresses { get; set; }
        public string TemplateSubject { get; set; }
        public string TemplateBody { get; set; }
    }
}
