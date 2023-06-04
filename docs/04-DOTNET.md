# Integrate NX with dotnetcore

## Initialize nx-dotnet

```bash
npm i --save-dev @nx-dotnet/core
npx nx g @nx-dotnet/core:init
```

## Create a webapi

```bash
npx nx g @nx-dotnet/core:app gatehub --pathScheme dotnet --template webapi --test-template nunit --language C# --solutionFile nineteensevenfour.sln
```

## Create a library

```bash
npx nx g @nx-dotnet/core:library gatehub-domain --pathScheme dotnet --template classlib --test-template nunit --language C# --solutionFile nineteensevenfour.sln

npx nx g @nx-dotnet/core:library gatehub-business --pathScheme dotnet --template classlib --test-template nunit --language C# --solutionFile nineteensevenfour.sln

npx nx g @nx-dotnet/core:library gatehub-business-sqllite --pathScheme dotnet --template classlib --test-template nunit --language C# --solutionFile nineteensevenfour.sln
```

## EFCore

!!! IMPORTANT !!! net7.0.302 is broken

- can't run dotnet format

- There seems to be existing issues between nx-dotnet and EFCore that prevent running dotnet ef commands.

```bash
dotnet tool restore

dotnet ef migrations add InitialCreate -c SqliteDbContext -s apps/gatehub -p libs/gatehub-data-sqlite

dotnet ef database update -c SqliteDbContext -s apps/gatehub -p libs/gatehub-data-sqlite --connection "Data Source=gatehub.db"
```

## Powered by

```text
  _   _ _            _                   _____                      ______               
 | \ | (_)          | |                 / ____|                    |  ____|              
 |  \| |_ _ __   ___| |_ ___  ___ _ __ | (___   _____   _____ _ __ | |__ ___  _   _ _ __ 
 | . ` | | '_ \ / _ \ __/ _ \/ _ \ '_ \ \___ \ / _ \ \ / / _ \ '_ \|  __/ _ \| | | | '__|
 | |\  | | | | |  __/ ||  __/  __/ | | |____) |  __/\ V /  __/ | | | | | (_) | |_| | |   
 |_| \_|_|_| |_|\___|\__\___|\___|_| |_|_____/ \___| \_/ \___|_| |_|_|  \___/ \__,_|_|
```
