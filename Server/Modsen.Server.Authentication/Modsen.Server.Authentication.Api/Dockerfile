#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Modsen.Server.Authentication/Modsen.Server.Authentication.Api/Modsen.Server.Authentication.Api.csproj", "Modsen.Server.Authentication/Modsen.Server.Authentication.Api/"]
COPY ["Modsen.Server.Authentication/Modsen.Server.Authentication.Application/Modsen.Server.Authentication.Application.csproj", "Modsen.Server.Authentication/Modsen.Server.Authentication.Application/"]
COPY ["Modsen.Server.Authentication/Modsen.Server.Authentication.Domain/Modsen.Server.Authentication.Domain.csproj", "Modsen.Server.Authentication/Modsen.Server.Authentication.Domain/"]
COPY ["Modsen.Server.Shared/Modsen.Server.Shared.csproj", "Modsen.Server.Shared/"]
COPY ["Modsen.Server.Authentication/Modsen.Server.Authentication.Infrastructure/Modsen.Server.Authentication.Infrastructure.csproj", "Modsen.Server.Authentication/Modsen.Server.Authentication.Infrastructure/"]
RUN dotnet restore "./Modsen.Server.Authentication/Modsen.Server.Authentication.Api/./Modsen.Server.Authentication.Api.csproj"
COPY . .
WORKDIR "/src/Modsen.Server.Authentication/Modsen.Server.Authentication.Api"
RUN dotnet build "./Modsen.Server.Authentication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Modsen.Server.Authentication.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Modsen.Server.Authentication.Api.dll"]