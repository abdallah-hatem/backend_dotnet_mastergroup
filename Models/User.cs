using Microsoft.AspNetCore.Identity;
using backend_dotnet.Enums;

namespace backend_dotnet.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<EmployeeCase>? EmployeeCases { get; set; }

        // public UserRole Role { get; set; } = UserRole.EMPLOYEE;
    }
}