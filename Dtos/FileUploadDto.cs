using Microsoft.AspNetCore.Http;

namespace backend_dotnet.Dtos
{
    public class FileUploadDto
    {
        public IFormFile? File { get; set; }
    }
} 