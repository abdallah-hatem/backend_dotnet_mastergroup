namespace backend_dotnet.Models
{
    public class FileMetadata
    {
        public int Id { get; set; }
        public required string FileName { get; set; }
        public required string FilePath { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        // public int CaseId { get; set; }
        // public Case? Case { get; set; }
    }
}