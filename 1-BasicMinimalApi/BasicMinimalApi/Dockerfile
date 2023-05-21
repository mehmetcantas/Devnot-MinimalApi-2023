FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["1-BasicMinimalApi/BasicMinimalApi/BasicMinimalApi.csproj", "1-BasicMinimalApi/BasicMinimalApi/"]
RUN dotnet restore "1-BasicMinimalApi/BasicMinimalApi/BasicMinimalApi.csproj"
COPY . .
WORKDIR "/src/1-BasicMinimalApi/BasicMinimalApi"
RUN dotnet build "BasicMinimalApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BasicMinimalApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasicMinimalApi.dll"]
