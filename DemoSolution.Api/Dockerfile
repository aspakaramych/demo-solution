FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DemoSolution.Api/DemoSolution.Api.csproj", "DemoSolution.Api/"]
COPY ["DemoSolution.Application/DemoSolution.Application.csproj", "DemoSolution.Application/"]
COPY ["DemoSolution.Domain/DemoSolution.Domain.csproj", "DemoSolution.Domain/"]
COPY ["DemoSolution.Infrastructure/DemoSolution.Infrastructure.csproj", "DemoSolution.Infrastructure/"]
RUN dotnet restore "DemoSolution.Api/DemoSolution.Api.csproj"
COPY . .
WORKDIR "/src/DemoSolution.Api"
RUN dotnet build "./DemoSolution.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DemoSolution.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoSolution.Api.dll"]
