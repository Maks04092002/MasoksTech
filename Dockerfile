# Etapa base para ejecución (usando la imagen ligera de ASP.NET Core)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# Railway inyecta el puerto dinámicamente en la variable $PORT.
# En .NET 8, Kestrel lee automáticamente la variable de entorno PORT.

# Etapa de compilación (usando el SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiar los archivos de proyecto para aprovechar el caché de Docker
COPY ["MasoksTech.API/MasoksTech.API.csproj", "MasoksTech.API/"]
# Si MasoksTech.API hace referencia a Domain, Infrastructure o Application, debes agregarlos aquí.
# Ejemplo:
COPY ["MasoksTech.Application/MasoksTech.Application.csproj", "MasoksTech.Application/"]
COPY ["MasoksTech.Domain/MasoksTech.Domain.csproj", "MasoksTech.Domain/"]
COPY ["MasoksTech.Infrastructure/MasoksTech.Infrastructure.csproj", "MasoksTech.Infrastructure/"]

# 2. Restaurar dependencias específicamente para el API principal
RUN dotnet restore "MasoksTech.API/MasoksTech.API.csproj"

# 3. Copiar el resto del código
COPY . .

# 4. Compilar y publicar directamente a la carpeta /app/publish
WORKDIR "/src/MasoksTech.API"
RUN dotnet publish "MasoksTech.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa final (generar la imagen limpia solo con los binarios)
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Definir el ENTRYPOINT asegurando que escuche en el puerto correcto de Railway
# (Nota: Kestrel en .NET 8 detecta la variable PORT automáticamente, pero lo aseguramos explícitamente en el ENTRYPOINT)
ENTRYPOINT ["dotnet", "MasoksTech.API.dll"]
