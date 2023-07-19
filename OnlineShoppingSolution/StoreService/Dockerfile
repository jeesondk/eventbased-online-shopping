FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Common/Common.csproj", "Common/"]
COPY ["StoreService/StoreService.csproj", "StoreService/"]
RUN dotnet restore "StoreService/StoreService.csproj"
COPY . .
WORKDIR "/src/StoreService"
RUN dotnet build "StoreService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StoreService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StoreService.dll"]
