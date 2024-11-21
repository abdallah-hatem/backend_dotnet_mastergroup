using backend_dotnet.Enums;

namespace backend_dotnet.Models
{
    public class Case
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required int MobileNumber { get; set; }
        public string? Email { get; set; }
        public required string VisaNumber { get; set; }
        public required byte[] Attachment { get; set; }
        public CaseStatus? Status { get; set; } = CaseStatus.PENDING;
        public DateTime CreatedAt { get; set; }
    }
}
