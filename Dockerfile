# Base image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the project file from the subfolder
COPY CST-326TempoTunes/CST-326TempoTunes.csproj ./CST-326TempoTunes/
RUN dotnet restore ./CST-326TempoTunes/CST-326TempoTunes.csproj

# Copy the rest of the code
COPY . .

# Build the project
WORKDIR /src/CST-326TempoTunes
RUN dotnet publish CST-326TempoTunes.csproj -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CST-326TempoTunes.dll"]
