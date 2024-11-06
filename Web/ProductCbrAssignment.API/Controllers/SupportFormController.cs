using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCbrAssignment.Domain.Abstractions.SupportForms;
using ProductCbrAssignment.Domain.DTOs.SupportForms;

namespace ProductCbrAssignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportFormController : ControllerBase
    {
        private readonly ISupportFormService _supportFormService;

        public SupportFormController(ISupportFormService supportFormService)
        {
            _supportFormService = supportFormService;
        }
   
        [HttpPost("CreateSupportForm")]
        [Authorize]
        public async Task<ActionResult> CreateSupportForm([FromBody] CreateSupportFormRequest request)
        {
            var supportForm = await _supportFormService.CreateSupportForm(request);
            return Ok(supportForm);
        }

        [HttpGet("GetAllSupportForms")]
        [Authorize]
        public async Task<ActionResult<List<SupportFormResponse>>> GetAllSupportForms()
        {
            var supportForms = await _supportFormService.GetSupportForms();
            if (supportForms == null || !supportForms.Any())
            {
                return NoContent();
            }
            return Ok(supportForms);
        }

        [HttpDelete("DeleteSupportForm/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSupportForm(string id)
        {
            var isDeleted = await _supportFormService.RemoveSupportForm(id);

            if (!isDeleted)
            {
                return NotFound($"Support form with ID '{id}' was not found or could not be deleted.");
            }

            return NoContent(); 
        }
    }
}
