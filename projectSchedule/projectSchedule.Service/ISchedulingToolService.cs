public interface ISchedulingToolService
{
    Task ConfigureReportScheduling(string[] deliveryMethods, string[] emailAddresses, string ftpUrl, string ftpPassword, string ftpFilePath, string sharepointUrl, string sharepointDocumentLibrary, string fileType, Dictionary<string, string> deliverySchedule, string[] successNotificationEmails);
}