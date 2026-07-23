# Sakila OLLAMA Integration

Este repositorio contiene una aplicación web construida con ASP.NET Core MVC (Entity Framework Core) utilizando la base de datos de ejemplo **Sakila**.

## Características
- **Integración con Inteligencia Artificial (Ollama)**: Se ha integrado Ollama para ejecutar y utilizar modelos de lenguaje (LLM) de forma local. La aplicación se comunica con la API de Ollama y permite a los usuarios interactuar con un asistente de IA directamente desde la plataforma.
- **Gestión de Base de Datos**: Interfaz para visualizar y gestionar registros de la base de datos Sakila (Actores, Películas, Categorías, Idiomas, etc.).
- **Autenticación**: Sistema de usuarios (Identity) y autenticación integrado.

## Requisitos Previos
- [.NET SDK](https://dotnet.microsoft.com/) instalado.
- [Ollama](https://ollama.com/) instalado y ejecutándose localmente.
- Un modelo de Ollama descargado (por ejemplo, `llama3.2`). Para descargarlo, ejecuta en tu terminal: `ollama pull llama3.2` u `ollama run llama3.2`.
- SQL Server con la base de datos Sakila configurada (la cadena de conexión debe estar configurada en tu `appsettings.json`).

## Configuración y Ejecución
1. Clona el repositorio:
   ```bash
   git clone https://github.com/Sahid-B/Sakila_OLLAMA.git
   ```
2. Asegúrate de tener Ollama corriendo:
   ```bash
   ollama serve
   ```
3. Ejecuta la aplicación:
   ```bash
   dotnet run --project SakilaApp
   ```
4. Abre tu navegador y dirígete a la URL indicada en la consola de ejecución (usualmente `https://localhost:7084` o `http://localhost:5033`).
