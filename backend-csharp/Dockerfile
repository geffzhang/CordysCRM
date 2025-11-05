# CordysCRM ASP.NET Core Backend Dockerfile
# Multi-stage build for optimized image size

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY CordysCRM.sln ./
COPY CordysCRM.Framework/CordysCRM.Framework.csproj CordysCRM.Framework/
COPY CordysCRM.CRM/CordysCRM.CRM.csproj CordysCRM.CRM/
COPY CordysCRM.App/CordysCRM.App.csproj CordysCRM.App/

# Restore dependencies
RUN dotnet restore CordysCRM.sln

# Copy all source files
COPY . .

# Build the application
WORKDIR /src/CordysCRM.App
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Create directory for logs and config
RUN mkdir -p /opt/cordys/logs/cordys-crm && \
    mkdir -p /opt/cordys/conf

# Copy published application
COPY --from=publish /app/publish .

# Expose ports
EXPOSE 8081
EXPOSE 8082

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8081
ENV ASPNETCORE_ENVIRONMENT=Production

# Run the application
ENTRYPOINT ["dotnet", "CordysCRM.App.dll"]
