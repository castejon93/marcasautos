# Linux-based Dockerfile for .NET 10
# See https://aka.ms/customizecontainer for guidance.

# Use Linux .NET images for Linux containers
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MarcasAutos/MarcasAutos.csproj", "MarcasAutos/"]
RUN dotnet restore "MarcasAutos/MarcasAutos.csproj"
COPY . .
WORKDIR "/src/MarcasAutos"
RUN dotnet build "MarcasAutos.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MarcasAutos.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarcasAutos.dll"]