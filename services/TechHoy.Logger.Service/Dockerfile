#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Docker
EXPOSE 10900

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/TechHoy.Logger.Service/TechHoy.Logger.Service.csproj", "services/TechHoy.Logger.Service/"]
COPY ["libs/TechHoy.Logging/TechHoy.Logging.csproj", "libs/TechHoy.Logging/"]
RUN dotnet restore "./services/TechHoy.Logger.Service/./TechHoy.Logger.Service.csproj"
COPY . .
WORKDIR "/src/services/TechHoy.Logger.Service"
RUN dotnet build "./TechHoy.Logger.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TechHoy.Logger.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TechHoy.Logger.Service.dll"]