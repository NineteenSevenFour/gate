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

> Due to issues in EF Core related to intermediate path and split project, the `dotnet ef migrations` & `dotnet ef dbcontext scaffold` commands does not work !
> Known issues with efcore & nx-dotnet
>
> - [nx-dotnet issue 673](https://github.com/nx-dotnet/nx-dotnet/discussions/673)
> - [efcore issue 23691](https://github.com/dotnet/efcore/issues/23691)
> - [efcore issue 23853](https://github.com/dotnet/efcore/issues/23853)

### Migrations

```bash
dotnet tool restore

dotnet ef migrations add InitialCreate -c SqliteDbContext -s apps/gatehub -p libs/gatehub-data-sqlite

dotnet ef database update -c SqliteDbContext -s apps/gatehub -p libs/gatehub-data-sqlite --connection "Data Source=gatehub.db"
```

### Scaffold from database

```bash
dotnet tool restore

dotnet ef dbcontext scaffold "Data Source=gatehub.db" Microsoft.EntityFrameworkCore.Sqlite -s apps/gatehub  -p gatehub-data-sqllite  -c SqliteDbContextScaffold -n NineteenSevenFour.Gatehub.Data.Sqlite -o scafold/entities --context-dir scafold/context
```

### Optimization

Optimization has been made by pre-compiling the Entities using the command below. This is used by the DbContextFactory.

Note: Make sure to replace the <PWD> by the proper password

```bash
dotnet tool restore

dotnet ef dbcontext optimize -p gatehub-data-sqllite -o Entity/Compiled -n NineteenSevenFour.Gatehub.Data.Sqlite.Entity.Compiled -c SqliteDbContext
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
