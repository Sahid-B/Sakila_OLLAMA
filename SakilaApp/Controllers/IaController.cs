using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SakilaApp.Models;
using SakilaApp.Services;

namespace SakilaApp.Controllers
{
    /// <summary>
    /// Controlador para exponer endpoints de Inteligencia Artificial a la vista.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IaController : ControllerBase
    {
        private readonly OllamaService _ollamaService;

        // Inyección del servicio de Ollama
        public IaController(OllamaService ollamaService)
        {
            _ollamaService = ollamaService;
        }

        /// <summary>
        /// Endpoint POST: /api/ia/generar
        /// Recibe el prompt y retorna la respuesta generada por la IA.
        /// </summary>
        [HttpPost("generar")]
        public async Task<IActionResult> Generar([FromBody] ConsultaIARequest request)
        {
            if (string.IsNullOrWhiteSpace(request?.Prompt))
            {
                return BadRequest(new { error = "El prompt no puede estar vacío." });
            }

            var respuesta = await _ollamaService.GenerarRespuestaAsync(request.Prompt);
            
            return Ok(new { response = respuesta });
        }
    }
}
