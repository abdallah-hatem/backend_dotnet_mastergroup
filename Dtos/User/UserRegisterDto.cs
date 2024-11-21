namespace backend_dotnet.Dtos.User
{
    public class UserRegisterDto
    {
        // Required: User's chosen username
        public required string Username { get; set; }

        // Required: User's email address
        public required string Email { get; set; }

        // Required: User's password
        public required string Password { get; set; }

    }
}