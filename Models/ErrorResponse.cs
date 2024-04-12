namespace GBC_Travel_Group_90.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        
        public DateTime TimeStamp { get; set; }
      

    }
}
