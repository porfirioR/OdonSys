# OdonSys
Sistema Odontológico

## Migrations

- #### Nuget Package-Manager
Para crear migrations
ir a tools-> Nuget Package-Manager -> Package Manager Console
En la consola abierta
 Cambiar "Default project" a Access\Access.Sql
Para agregar una nueva migración, escribir:
 Add-Migration "nombre de la migracion"
Si no hay errores, escribir:
 update-database

- #### Con Power Shell

Estar en \OdonSys\OdonSysBackEnd\
cd .\Access\Access.Sql\
dotnet ef migrations add AddNewTables -s ..\..\Host.Api\
dotnet ef database update -s ..\..\Host.Api

## Data base

Para crear los registros basicos en la base de datos necesarios para iniciar el proyecto debe agregarlos en OdonSys\OdonSysBackEnd\Test\IntegrationTest\Test.IntegrationTest.Procedure\Resources.BasicSql.sql y ejecutar algun test.

## Npm
Ver versiones a actualizar
- npm outdated

Para actualizar todos
- npm update

## Ngrx Store

[Ngrx](https://ngrx.io/)

- ng g en core/store/procedure/procedure -m ../../core.module.ts --reducers ../index.ts --skip-tests true --dry-run
- ng g se core/store/procedure/procedure --skip-tests true --dry-run
- ng g ef core/store/procedure/procedure -m core/core.module.ts -a true --skip-tests true

## 🛠 Skills
- ## Front end
Typescript, HTML5, CSS3
- ## Back end
.Net 6
- ## Data Base
SQL Server