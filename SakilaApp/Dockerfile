# Etapa 1: Base para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa 2: Construcción de la aplicación
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["SakilaApp.csproj", "./"]
RUN dotnet restore "./SakilaApp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SakilaApp.csproj" -c Release -o /app/build

# Etapa 3: Publicación
FROM build AS publish
RUN dotnet publish "SakilaApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 4: Imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SakilaApp.dll"]
