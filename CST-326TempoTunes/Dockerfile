# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CST-326TempoTunes.csproj", "./"]
RUN dotnet restore "./CST-326TempoTunes.csproj"
COPY . .
RUN dotnet publish "./CST-326TempoTunes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CST-326TempoTunes.dll"]
