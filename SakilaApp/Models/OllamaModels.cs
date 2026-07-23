using System.Text.Json.Serialization;

namespace SakilaApp.Models
{
    /// <summary>
    /// Modelo para la petición de IA desde el cliente (Frontend)
    /// </summary>
    public class ConsultaIARequest
    {
        public string Prompt { get; set; }
    }

    /// <summary>
    /// Modelo para la petición a la API local de Ollama
    /// </summary>
    public class OllamaRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("prompt")]
        public string Prompt { get; set; }

        [JsonPropertyName("stream")]
        public bool Stream { get; set; }
    }

    /// <summary>
    /// Modelo para deserializar la respuesta de la API de Ollama
    /// </summary>
    public class OllamaResponse
    {
        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("response")]
        public string Response { get; set; }

        [JsonPropertyName("done")]
        public bool Done { get; set; }
    }
}
