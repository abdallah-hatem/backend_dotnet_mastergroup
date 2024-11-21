namespace backend_dotnet.Models
{
    public class EmployeeCase
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CaseId { get; set; }
        public DateTime AssignedAt { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Case? Case { get; set; }
    }
}
