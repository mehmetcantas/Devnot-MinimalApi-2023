FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["0-ControllerBased/MvcWebApiNet5/MvcWebApiNet5.csproj", "0-ControllerBased/MvcWebApiNet5/"]
RUN dotnet restore "0-ControllerBased/MvcWebApiNet5/MvcWebApiNet5.csproj"
COPY . .
WORKDIR "/src/0-ControllerBased/MvcWebApiNet5"
RUN dotnet build "MvcWebApiNet5.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MvcWebApiNet5.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MvcWebApiNet5.dll"]
