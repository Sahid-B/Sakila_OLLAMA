using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SakilaApp.Models;

namespace SakilaApp.Services
{
    /// <summary>
    /// Servicio inyectable para interactuar con la API local de Ollama.
    /// </summary>
    public class OllamaService
    {
        private readonly HttpClient _httpClient;

        // Inyección de dependencias de HttpClient
        public OllamaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Envía el prompt a Ollama y retorna la respuesta generada.
        /// </summary>
        public async Task<string> GenerarRespuestaAsync(string prompt)
        {
            try
            {
                // Configuramos la petición según la API de Ollama
                var requestBody = new OllamaRequest
                {
                    Model = "qwen2.5:1.5b",
                    Prompt = prompt,
                    Stream = false
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json"
                );

                // Realizamos el POST a la API local de Ollama
                var response = await _httpClient.PostAsync("http://localhost:11434/api/generate", jsonContent);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                
                // Deserializamos el JSON de respuesta
                var ollamaResponse = JsonSerializer.Deserialize<OllamaResponse>(responseString);

                return ollamaResponse?.Response ?? "El modelo no generó ninguna respuesta.";
            }
            catch (HttpRequestException ex)
            {
                // Captura errores cuando Ollama no está en ejecución o problemas de red
                return $"Error de conexión con Ollama: {ex.Message}. Asegúrate de que Ollama esté corriendo en http://localhost:11434 y el modelo 'qwen2.5:1.5b' esté descargado.";
            }
            catch (Exception ex)
            {
                // Manejo de otros errores imprevistos
                return $"Ocurrió un error inesperado al consultar a la IA: {ex.Message}";
            }
        }
    }
}
