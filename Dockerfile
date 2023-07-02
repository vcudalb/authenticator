# Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
ARG BUILD_CONFIGURATION=Release

COPY "Authenticator.sln" ./
COPY "src/Authenticator.Api/Authenticator.Api.csproj" "src/Authenticator.Api/"
COPY "src/Authenticator.Application/Authenticator.Application.csproj" "src/Authenticator.Application/"
COPY "src/Authenticator.Domain/Authenticator.Domain.csproj" "src/Authenticator.Domain/"
COPY "src/Authenticator.Infrastructure/Authenticator.Infrastructure.csproj" "src/Authenticator.Infrastructure/"
COPY "src/Authenticator.DbMigrator/Authenticator.DbMigrator.csproj" "src/Authenticator.DbMigrator/"

RUN dotnet restore --source "https://api.nuget.org/v3/index.json" "src/Authenticator.Api/Authenticator.Api.csproj"

COPY . .
WORKDIR "/src/src/Authenticator.Api/"

RUN dotnet build "Authenticator.Api.csproj" --configuration $BUILD_CONFIGURATION --no-restore
RUN dotnet publish "Authenticator.Api.csproj" --configuration $BUILD_CONFIGURATION --no-build -o /publish

# Unit tests
FROM build AS testrunner
WORKDIR /src/tests/Authenticator.UnitTests/
ARG BUILD_CONFIGURATION=Release

COPY "tests/Authenticator.UnitTests/Authenticator.UnitTests.csproj" .

RUN dotnet restore "Authenticator.UnitTests.csproj"
RUN dotnet build "Authenticator.UnitTests.csproj" --configuration $BUILD_CONFIGURATION --no-restore

RUN dotnet test --logger:trx

#run stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 
EXPOSE 80
WORKDIR /app
ENTRYPOINT ["dotnet", "Authenticator.Api.dll"]

COPY --from=build /publish .
