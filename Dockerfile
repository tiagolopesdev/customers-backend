#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Customers.Api/Customers.Api.csproj", "Customers.Api/"]
COPY ["Customers.Application/Customers.Application.csproj", "Customers.Application/"]
COPY ["Customers.Domain/Customers.Domain.csproj", "Customers.Domain/"]
COPY ["Customers.Infrastructure/Customers.Infrastructure.csproj", "Customers.Infrastructure/"]
RUN dotnet restore "./Customers.Api/Customers.Api.csproj"
COPY . .
WORKDIR "/src/Customers.Api"
RUN dotnet build "./Customers.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Customers.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Customers.Api.dll"]