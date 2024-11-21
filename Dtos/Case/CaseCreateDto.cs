namespace backend_dotnet.Dtos.Case
{
    public class CaseCreateDto
    {
        public required string Username { get; set; }
        public required int MobileNumber { get; set; }
        public string? Email { get; set; }
        public required string VisaNumber { get; set; }
        public required byte[] Attachment { get; set; }
    }
} 