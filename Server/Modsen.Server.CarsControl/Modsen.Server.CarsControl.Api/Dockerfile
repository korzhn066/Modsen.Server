#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Modsen.Server.CarsControl/Modsen.Server.CarsControl.Api/Modsen.Server.CarsControl.Api.csproj", "Modsen.Server.CarsControl/Modsen.Server.CarsControl.Api/"]
COPY ["Modsen.Server.CarsControl/Modsen.Server.CarsControl.Business/Modsen.Server.CarsControl.Business.csproj", "Modsen.Server.CarsControl/Modsen.Server.CarsControl.Business/"]
COPY ["Modsen.Server.CarsControl/Modsen.Server.CarsControl.DataAccess/Modsen.Server.CarsControl.DataAccess.csproj", "Modsen.Server.CarsControl/Modsen.Server.CarsControl.DataAccess/"]
COPY ["Modsen.Server.Shared/Modsen.Server.Shared.csproj", "Modsen.Server.Shared/"]
RUN dotnet restore "./Modsen.Server.CarsControl/Modsen.Server.CarsControl.Api/./Modsen.Server.CarsControl.Api.csproj"
COPY . .
WORKDIR "/src/Modsen.Server.CarsControl/Modsen.Server.CarsControl.Api"
RUN dotnet build "./Modsen.Server.CarsControl.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Modsen.Server.CarsControl.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Modsen.Server.CarsControl.Api.dll"]