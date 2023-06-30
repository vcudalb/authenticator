# Introduction 
The **Authenticator** solution provides a specialized service designed to efficiently manage various user operations and facilitate the generation of secure tokens. 
This dedicated component plays a crucial role by handling all clients-related functionalities and ensuring the seamless creation and validation of tokens.

# Getting Started
Once you have cloned the solution, please proceed to the following steps in order to set up and activate the solution:
1. Modify the connection string to the desired one or to match your local machine.
2. If you are utilizing an integrated security connection to SSMS (SQL Server Management Studio), ensure that the **UseCredentials** setting is set to **false**. However, if you are using a username and password for authentication, please refer to the **User Secrets** chapter for further instructions.
3. Execute the specified commands sequentially to create and populate your database. Alternatively, you can run the **Authenticator.DbMigrator** project in debug mode, and the necessary changes will be applied automatically.

Visual Studio PMC:
```shell
Update-Database -context PersistedGrantDbContext
Update-Database -context ConfigurationDbContext
Update-Database -context AuthDbContext
```

CLI:
```shell
dotnet ef database update --context PersistedGrantDbContext
dotnet ef database update --context ConfigurationDbContext
dotnet ef database update --context AuthDbContext
```

## User Secrets
Please note that the project relies on user secrets to keep connection strings and other environment-specific materials from being committed and possibly causing issues.
Accordingly, please note that you will be required to populate your `secrets.json` file with the following:

```json
{
  "ConnectionStrings": {
    "authenticator": "_SECRET_"
  }
}
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
