to get the project up & runing via dotnet CLI : 

1 - Make sure to have Sqlite Entitry framework's driver 
you can install it by runing : dotnet add package Microsoft.EntityFrameworkCore.Sqlite

2 - Make sure to have the entity framework Design Api You can install it using this command : dotnet add package Microsoft.EntityFrameworkCore.Design

3 - migrations add InitialCreate

4 - dotnet ef database update

5 - dotnet run

That's it .
