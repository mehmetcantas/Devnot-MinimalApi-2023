FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["5-Repr/MinimalApiRepr/MinimalApiRepr.csproj", "5-Repr/MinimalApiRepr/"]
RUN dotnet restore "5-Repr/MinimalApiRepr/MinimalApiRepr.csproj"
COPY . .
WORKDIR "/src/5-Repr/MinimalApiRepr"
RUN dotnet build "MinimalApiRepr.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MinimalApiRepr.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MinimalApiRepr.dll"]
