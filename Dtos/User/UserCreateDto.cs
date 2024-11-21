
namespace backend_dotnet.Dtos.User
{
    public class UserCreateDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}