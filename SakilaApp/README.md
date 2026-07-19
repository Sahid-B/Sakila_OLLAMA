# Proyecto Sakila - ASP.NET Core MVC con Identity

Este proyecto es una aplicación web MVC que integra ASP.NET Core Identity, autenticación mediante Google OAuth, confirmación por correos (Gmail SMTP) y almacenamiento en PostgreSQL. Cumple con todos los requisitos de integración de seguridad y gestión de roles.

## Tecnologías Utilizadas
* ASP.NET Core MVC (.NET 10)
* Entity Framework Core
* PostgreSQL
* ASP.NET Core Identity
* Google Cloud Console (OAuth)
* MailKit (SMTP)

## Instrucciones de Ejecución

Para correr este proyecto en tu entorno local, sigue estos pasos:

### 1. Clonar el repositorio
Si estás descargando el proyecto desde GitHub, clónalo en tu máquina local:
```bash
git clone <URL_DE_TU_REPO>
```

### 2. Restaurar la base de datos
El proyecto utiliza PostgreSQL. Debes restaurar la base de datos para tener todas las tablas de identidad y la información de prueba:
* Abre pgAdmin o tu gestor de PostgreSQL.
* Crea una base de datos vacía llamada `sakila`.
* Restaura el archivo `.sql` o `.backup` (copia de seguridad) incluido en este repositorio.

### 3. Configurar Credenciales (opcional)
El archivo `appsettings.json` ya incluye la cadena de conexión a PostgreSQL. Si tus credenciales locales de Postgres son diferentes (Username/Password), cámbialas en la sección `ConnectionStrings`:
```json
"DefaultConnection": "Host=localhost;Port=5432;Database=sakila;Username=TU_USUARIO;Password=TU_PASSWORD"
```
*(Nota: Las claves de Google y la contraseña de correo de prueba están en el archivo para fines académicos de demostración de la funcionalidad).*

### 4. Correr la aplicación
Abre una terminal en la carpeta raíz del proyecto (`SakilaApp`) y ejecuta los siguientes comandos:
```bash
dotnet restore
dotnet run
```
La aplicación se levantará normalmente en `http://localhost:5055` o el puerto configurado.

### 5. Usuarios de Prueba (Credenciales)
Una vez que la aplicación esté corriendo, puedes utilizar los siguientes usuarios semilla creados por defecto para probar los roles y restricciones del sistema:

* **Administrador:** 
  * Correo: `admin@espe.edu.ec` 
  * Contraseña: `Admin123*`
* **Empleado:** 
  * Correo: `empleado@espe.edu.ec` 
  * Contraseña: `Admin123*`

## Capturas de Pantalla (Evidencias)
Todas las capturas de evidencia de la rúbrica (Configuraciones de Google Cloud, Tablas en PostgreSQL, Login, 2FA, Accesos Denegados, etc.) se encuentran adjuntas en la carpeta `Evidencias/` de este repositorio.
