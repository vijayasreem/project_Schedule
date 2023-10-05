
namespace projectSchedule
{
    public class EmailRequestModel
    {
        public int Id { get; set; }
        
        [Required]
        public string FileType { get; set; }
        
        [Required]
        public string DestinationType { get; set; }
    }
}
