# Stage base para usar na aplica��o
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Stage de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Debug # Altere o padr�o para Debug
WORKDIR /src
COPY ["ApiPedidos.Services/ApiPedidos.Services.csproj", "ApiPedidos.Services/"]
RUN dotnet restore "./ApiPedidos.Services/ApiPedidos.Services.csproj"
COPY . .
WORKDIR "/src/ApiPedidos.Services"
RUN dotnet build "./ApiPedidos.Services.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage de publica��o
FROM build AS publish
ARG BUILD_CONFIGURATION=Debug # Altere o padr�o para Debug
RUN dotnet publish "./ApiPedidos.Services.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiPedidos.Services.dll"]
