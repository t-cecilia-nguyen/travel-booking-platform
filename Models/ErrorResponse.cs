namespace GBC_Travel_Group_90.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? StatusPhrase { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<string>? Errors { get; set; }

    }
}
