# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .

# Restore and publish your specific Web API project
RUN dotnet restore "DMello.Api/DMello.Api.csproj"
RUN dotnet publish "DMello.Api/DMello.Api.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .  

# Entrypoint matches the project name (DMello.Api.dll)
ENTRYPOINT ["dotnet", "DMello.Api.dll"]