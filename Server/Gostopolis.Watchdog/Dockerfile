#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Gostopolis.Watchdog/Gostopolis.Watchdog.csproj", "Gostopolis.Watchdog/"]
RUN dotnet restore "./Gostopolis.Watchdog/./Gostopolis.Watchdog.csproj"
COPY . .
WORKDIR "/src/Gostopolis.Watchdog"
RUN dotnet build "./Gostopolis.Watchdog.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Gostopolis.Watchdog.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gostopolis.Watchdog.dll"]