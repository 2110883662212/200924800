FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RegistroEventos.csproj", "./"]
RUN dotnet restore "./RegistroEventos.csproj"
COPY . .
RUN dotnet publish "RegistroEventos.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "RegistroEventos.dll"]