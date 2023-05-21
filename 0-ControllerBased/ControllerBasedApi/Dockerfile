FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["0-ControllerBased/ControllerBasedApi/ControllerBasedApi.csproj", "0-ControllerBased/ControllerBasedApi/"]
RUN dotnet restore "0-ControllerBased/ControllerBasedApi/ControllerBasedApi.csproj"
COPY . .
WORKDIR "/src/0-ControllerBased/ControllerBasedApi"
RUN dotnet build "ControllerBasedApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControllerBasedApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControllerBasedApi.dll"]
