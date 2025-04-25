using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupaController : ControllerBase
    {
        private readonly IGrupaService _grupaService;

        public GrupaController(IGrupaService grupaService)
        {
            _grupaService = grupaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrupaResponseDTO>>> GetAll()
        {
            var grupy = await _grupaService.GetAllAsync();
            return Ok(grupy);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GrupaResponseDTO>> GetById(int id)
        {
            var grupa = await _grupaService.GetByIdAsync(id);
            if (grupa == null)
                return NotFound();

            return Ok(grupa);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GrupaRequestDTO grupaDto)
        {
            await _grupaService.AddAsync(grupaDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GrupaRequestDTO grupaDto)
        {
            await _grupaService.UpdateAsync(id, grupaDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _grupaService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("full-name/{id}")]
        public async Task<ActionResult<string>> GetFullGroupName(int id)
        {
            var fullName = await _grupaService.GetFullGroupNameAsync(id);
            return Ok(fullName);
        }
    }
}
