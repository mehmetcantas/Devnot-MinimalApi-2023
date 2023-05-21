FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["2-SwaggerIntegration/MinimalApiSwagger/MinimalApiSwagger.csproj", "2-SwaggerIntegration/MinimalApiSwagger/"]
RUN dotnet restore "2-SwaggerIntegration/MinimalApiSwagger/MinimalApiSwagger.csproj"
COPY . .
WORKDIR "/src/2-SwaggerIntegration/MinimalApiSwagger"
RUN dotnet build "MinimalApiSwagger.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinimalApiSwagger.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalApiSwagger.dll"]
