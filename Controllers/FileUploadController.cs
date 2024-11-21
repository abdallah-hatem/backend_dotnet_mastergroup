using backend_dotnet.Dtos;
using backend_dotnet.Interfaces;
using backend_dotnet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace backend_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FileUploadController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadDto fileUploadDto)
        {
            if (fileUploadDto.File == null || fileUploadDto.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // var allowedExtensions = new[] { ".jpg", ".png", ".pdf" };
            var extension = Path.GetExtension(fileUploadDto.File.FileName).ToLower();

            // if (!allowedExtensions.Contains(extension))
            // {
            //     return BadRequest("Invalid file type.");
            // }

            // if (fileUploadDto.File.Length > 10 * 1024 * 1024) // 10 MB size limit
            // {
            //     return BadRequest("File size exceeds the limit of 10 MB.");
            // }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists

            var uniqueFileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Construct the public URL
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            var publicUrl = $"{baseUrl}/uploads/{uniqueFileName}";

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileUploadDto.File.CopyToAsync(stream);
                }

                var fileMetadata = new FileMetadata
                {
                    FileName = fileUploadDto.File.FileName,
                    FilePath = publicUrl,
                    UploadedAt = DateTime.UtcNow
                };

                await _unitOfWork.FileMetadatas.AddAsync(fileMetadata);
                await _unitOfWork.SaveChangesAsync();

                return CreatedAtAction(nameof(GetFileMetadata), new { id = fileMetadata.Id }, fileMetadata);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileMetadata(int id)
        {
            var fileMetadata = await _unitOfWork.FileMetadatas.GetByIdAsync(id);
            if (fileMetadata == null)
            {
                return NotFound($"File with ID {id} not found.");
            }

            return Ok(fileMetadata);
        }
    }
}