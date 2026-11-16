# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .

# Restore and publish your actual Web API project
RUN dotnet restore "DMello_BackendAPI/DMello_BackendAPI.csproj"
RUN dotnet publish "DMello_BackendAPI/DMello_BackendAPI.csproj" -c Release -o /app/publish

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /app/publish .  

# Entrypoint must match your actual Web API DLL filename
ENTRYPOINT ["dotnet", "DMello_BackendAPI.dll"]