# Use the official ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["archival-library-backend/archival_library_backend.csproj", "archival-library-backend/"]
RUN dotnet restore "archival-library-backend/archival_library_backend.csproj"

# Copy the entire source code and build the application
COPY . .
WORKDIR "/src/archival-library-backend"
RUN dotnet build -c Release -o /app/build

# Publish the application to the /app/publish folder
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Use the base image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "archival_library_backend.dll"]
