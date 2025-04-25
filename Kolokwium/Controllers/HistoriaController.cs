using BLL.DTOModels;
using BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoriaController : ControllerBase
    {
        private readonly IHistoriaService _historiaService;

        public HistoriaController(IHistoriaService historiaService)
        {
            _historiaService = historiaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoriaResponseDTO>>> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int size = 10)
        {
            if (page <= 0 || size <= 0)
                return BadRequest("Numer strony i rozmiar muszą być większe od 0.");

            var result = await _historiaService.GetPagedAsync(page, size);
            return Ok(result);
        }
    }
}
