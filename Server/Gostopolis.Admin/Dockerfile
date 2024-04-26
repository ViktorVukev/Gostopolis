FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Gostopolis.Admin/Gostopolis.Admin.csproj", "Gostopolis.Admin/"]
COPY ["Gostopolis/Gostopolis.csproj", "Gostopolis/"]
RUN dotnet restore "./Gostopolis.Admin/./Gostopolis.Admin.csproj"
COPY . .
WORKDIR "/src/Gostopolis.Admin"
RUN dotnet build "./Gostopolis.Admin.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Gostopolis.Admin.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gostopolis.Admin.dll"]