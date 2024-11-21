using backend_dotnet.Dtos.Case; // Assuming you have DTOs for Case
using backend_dotnet.Interfaces;
using backend_dotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cases = await _unitOfWork.Cases.GetAllAsync();
            return Ok(cases);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var caseItem = await _unitOfWork.Cases.GetByIdAsync(id);

            if (caseItem == null)
                return NotFound($"Case with ID {id} not found");

            return Ok(caseItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CaseCreateDto caseDto) // Assuming you have a CaseCreateDto
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var caseItem = new Case
            {
                Username = caseDto.Username,
                MobileNumber = caseDto.MobileNumber,
                Email = caseDto.Email,
                VisaNumber = caseDto.VisaNumber,
                Attachment = caseDto.Attachment,
            };

            await _unitOfWork.Cases.AddAsync(caseItem);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = caseItem.Id }, caseItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CaseUpdateDto caseDto) // Assuming you have a CaseUpdateDto
        {
            var existingCase = await _unitOfWork.Cases.GetByIdAsync(id);

            if (existingCase == null)
                return NotFound($"Case with ID {id} not found");

            existingCase.Username = caseDto.Username;
            existingCase.MobileNumber = caseDto.MobileNumber;
            existingCase.Email = caseDto.Email;
            existingCase.VisaNumber = caseDto.VisaNumber;
            existingCase.Attachment = caseDto.Attachment;

            _unitOfWork.Cases.Update(existingCase);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var caseItem = await _unitOfWork.Cases.GetByIdAsync(id);

            if (caseItem == null)
                return NotFound($"Case with ID {id} not found");

            _unitOfWork.Cases.Remove(caseItem);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}