# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
USER root
RUN apt-get update && apt-get install -y --allow-unauthenticated apt-utils libc6-dev libxrender1 libfontconfig1 libgdiplus libjpeg62-turbo libfontconfig1 libfreetype6 libxext6 libx11-6 fontconfig zlib1g

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["api/newFitnet.csproj", "api/"]
RUN dotnet restore "./api/newFitnet.csproj"
COPY . .
WORKDIR "/src/api"
RUN dotnet build "./newFitnet.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./newFitnet.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "newFitnet.dll"]