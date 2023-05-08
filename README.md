# OdonSys
Sistema Odontológico
 
Para crear migrations
ir a tools-> Nuget Package-Manager -> Package Manager Console
En la consola abierta
 Cambiar "Default project" a Access\Access.Sql
Para agregar una nueva migración, escribir:
 Add-Migration "nombre de la migracion"
Si no hay errores, escribir:
 update-database


Para crear los registros basicos en la base de datos necesarios para iniciar el proyecto debe agregarlos en OdonSys\OdonSysBackEnd\Test\IntegrationTest\Test.IntegrationTest.Procedure\Resources.BasicSql.sql y ejecutar algun test.

Para crear migrations sin Package Manager Console
Estar en \OdonSys\OdonSysBackEnd\
cd .\Access\Access.Sql\
dotnet ef migrations add AddNewTables -s ..\..\Host.Api\
dotnet ef database update -s ..\..\Host.Api