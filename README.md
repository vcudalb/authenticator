# Repository

[![Develop](https://github.com/vcudalb/authenticator/actions/workflows/build_on_push.yml/badge.svg?branch=develop)](https://github.com/vcudalb/authenticator/actions/workflows/build_on_push.yml)

This repository is of the Authenticator that is maintained by [vcudalb](cudalb.vasile@gmail.com) and licensed under the [MIT License](LICENSE.txt).

# Authenticator
The **Authenticator** solution provides a specialized service designed to efficiently manage various user operations and facilitate the generation of secure tokens. 
This dedicated component plays a crucial role by handling all clients-related functionalities and ensuring the seamless creation and validation of tokens.


# Getting Started
## Prerequisites

### Install SQL Server on Docker Container
- Install Docker Desktop, it can be downloaded from [here](https://www.docker.com/products/docker-desktop/)
- Pull latest SQL Server docker image
```shell
docker pull mcr.microsoft.com/mssql/server:2022-latest
```
- Run the container and expose ports

PowerShell:
```shell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SqlDopu69^(" `
   -p 6901:1433 --name infra.sqlserver --hostname infra.sqlserver `
   -d `
   mcr.microsoft.com/mssql/server:2022-latest
```
Bash:
```shell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SqlDopu69^(" \
   -p 6901:1433 --name infra.sqlserver --hostname infra.sqlserver \
   -d \
   mcr.microsoft.com/mssql/server:2022-latest
```

### User Secrets
Please note that the project relies on user secrets to keep connection strings and other environment-specific materials from being committed and possibly causing issues.
Accordingly, please be aware that it is required to populate the `secrets.json` file as in the example below:

```json
{
  "ConnectionStrings": {
    "authenticator": "_SECRET_"
  }
}
```

> 📝 If you have any issues with certificate or trusted checks please add to the connection string the `TrustServerCertificate=True;` setting.

### Creating database
Once you have all prerequisites done and cloned the solution, please proceed to the following steps in order to set up and activate the solution:
1. Modify the connection string to the desired one or to match your local machine.
2. If you are utilizing an integrated security connection to SSMS (SQL Server Management Studio), ensure that the **UseCredentials** setting is set to **false**. However, if you are using a username and password for authentication, please refer to the **User Secrets** chapter for further instructions.
3. Execute the specified commands sequentially to create and populate your database. Alternatively, you can run the **Authenticator.DbMigrator** project in debug mode, and the necessary changes will be applied automatically.
> 📝 Before running the DbMigrator please ensure that you've changed `secrets.json` connections.

Visual Studio PMC:
```shell
Update-Database -context PersistedGrantDbContext
Update-Database -context ConfigurationDbContext
Update-Database -context AuthDbContext
```

CLI:
```shell
dotnet ef database update --context PersistedGrantDbContext --startup-project Authenticator.Api/Authenticator.Api.csproj
dotnet ef database update --context ConfigurationDbContext --startup-project Authenticator.Api/Authenticator.Api.csproj
dotnet ef database update --context AuthenticatorDbContext --startup-project Authenticator.Api/Authenticator.Api.csproj
```


# Build and Test
To build the solution execute following commands:
```shell 
dotnet clean 
```
```shell 
dotnet build 
```

To run all tests execute following command in PowerShell or use IDE tabs:
```shell
ForEach ($folder in (Get-ChildItem -Path test -Directory)) { dotnet test $folder.FullName }
```

# Contribute
If you desire to contribute to this repository, please send a message to:
- **cudalb.vasile@gmail.com** or here [vcudalb](https://github.com/vcudalb)
