FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar el archivo del proyecto con estructura nativa
COPY RegistroEventos.csproj ./
RUN dotnet restore RegistroEventos.csproj

# Copiar el resto del código y compilar la app
COPY . .
RUN dotnet publish RegistroEventos.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Configurar puertos requeridos por entornos en la nube como Railway
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "RegistroEventos.dll"]
