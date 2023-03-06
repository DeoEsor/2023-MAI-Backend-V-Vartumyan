FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/OrderingService/OrderingService.csproj", "src/Services/OrderingService/"]
RUN dotnet restore "src/Services/OrderingService/OrderingService.csproj"
COPY . .
WORKDIR "/src/src/Services/OrderingService"
RUN dotnet build "OrderingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderingService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderingService.dll"]
