#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Modsen.Server.CarsElections/Modsen.Server.CarsElections.Api/Modsen.Server.CarsElections.Api.csproj", "Modsen.Server.CarsElections/Modsen.Server.CarsElections.Api/"]
COPY ["Modsen.Server.CarsElections/Modsen.Server.CarsElections.Application/Modsen.Server.CarsElections.Application.csproj", "Modsen.Server.CarsElections/Modsen.Server.CarsElections.Application/"]
COPY ["Modsen.Server.CarsElections/Modsen.Server.CarsElections.Domain/Modsen.Server.CarsElections.Domain.csproj", "Modsen.Server.CarsElections/Modsen.Server.CarsElections.Domain/"]
COPY ["Modsen.Server.Shared/Modsen.Server.Shared.csproj", "Modsen.Server.Shared/"]
COPY ["Modsen.Server.CarsElections/Modsen.Server.CarsElections.Infrastructure/Modsen.Server.CarsElections.Infrastructure.csproj", "Modsen.Server.CarsElections/Modsen.Server.CarsElections.Infrastructure/"]
RUN dotnet restore "./Modsen.Server.CarsElections/Modsen.Server.CarsElections.Api/Modsen.Server.CarsElections.Api.csproj"
COPY . .
WORKDIR "/src/Modsen.Server.CarsElections/Modsen.Server.CarsElections.Api"
RUN dotnet build "./Modsen.Server.CarsElections.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Modsen.Server.CarsElections.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Modsen.Server.CarsElections.Api.dll"]