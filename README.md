# Getting Things Running

You'll need the following things:

* .NET Core SDK 3.1+ https://dotnet.microsoft.com/download/dotnet-core/3.1
* Visual Studio Code (optional) https://code.visualstudio.com

Configure the necessary tools (the EF Core tools no longer ship with .NET Core):

```bash
dotnet tool restore
```

To get the database configured and running

```bash
cd src/CeleryArchitectureDemo
dotnet ef database update
```

Then you can run the application using Visual Studio Code (the tasks.json is setup properly).

```bash
cd src/CeleryArchitectureDemo
dotnet run
```

Navigate to: https://localhost:5001 to see the site (ignore SSL / HTTPS issues, it's a self-signed certificate).

Navigate to: https://localhost:5001/swagger to access the API page (in case you don't have Postman).
