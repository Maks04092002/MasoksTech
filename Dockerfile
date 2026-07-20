FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
# Copiar todos los archivos csproj y restaurar las dependencias
COPY ["MasoksTech.API/MasoksTech.API.csproj", "MasoksTech.API/"]
COPY ["MasoksTech.Application/MasoksTech.Application.csproj", "MasoksTech.Application/"]
COPY ["MasoksTech.Domain/MasoksTech.Domain.csproj", "MasoksTech.Domain/"]
COPY ["MasoksTech.Infrastructure/MasoksTech.Infrastructure.csproj", "MasoksTech.Infrastructure/"]
RUN dotnet restore "MasoksTech.API/MasoksTech.API.csproj"

# Copiar el resto del código y construir la aplicación
COPY . .
WORKDIR "/src/MasoksTech.API"
RUN dotnet build "MasoksTech.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MasoksTech.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MasoksTech.API.dll"]
