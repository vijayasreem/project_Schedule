
namespace projectSchedule
{
    public class EmailRequestModel
    {
        public int Id { get; set; }
        
        // Required properties
        public string AttachedFileType { get; set; }
        public string DestinationType { get; set; }
    }
}
