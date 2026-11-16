# Stage 1: Build the app using the .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .

# Publish your specific Web API project path
RUN dotnet publish "DMello.Api/DMello.Api.csproj" -c Release -o /app/publish

# Stage 2: Run the app using the lightweight ASP.NET runtime (No SDK here)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Copy the built files from Stage 1
COPY --from=build /app/publish .

# Use your exact compiled DLL name (check your bin folder if unsure)
ENTRYPOINT ["dotnet", "DMello.WebAPI.dll"]