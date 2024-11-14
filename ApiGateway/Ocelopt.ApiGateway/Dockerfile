FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:9000
#ENV ASPNETCORE_HTTP_PORTS=9000

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["ApiGateway/Ocelopt.ApiGateway/Ocelopt.ApiGateway.csproj", "ApiGateway/Ocelopt.ApiGateway/"]
RUN dotnet restore "ApiGateway/Ocelopt.ApiGateway/Ocelopt.ApiGateway.csproj"

COPY . .
WORKDIR "/src/ApiGateway/Ocelopt.ApiGateway"
RUN dotnet build "Ocelopt.ApiGateway.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Ocelopt.ApiGateway.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ocelopt.ApiGateway.dll"]
