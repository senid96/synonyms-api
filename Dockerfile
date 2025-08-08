# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY Synonyms_API/*.csproj ./Synonyms_API/
RUN dotnet restore Synonyms_API/Synonyms_API.csproj

# Copy the rest of the source code
COPY . .

# Build and publish the app
RUN dotnet publish Synonyms_API/Synonyms_API.csproj -c Release -o /app/publish --no-restore

# Use the official .NET 8 runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80 for the API
EXPOSE 80

# Set the entrypoint
ENTRYPOINT ["dotnet", "Synonyms_API.dll"]