### Database Migrations

#### Building initial migration

* Add initial migration
  ```cmd
  dotnet ef migrations add InitDatabase --output-dir Persistence/Migrations --startup-project ../Api --context ApplicationDbContext
  ```
  * __output-dir__ - where the migrations objects will be created.
  * __startup-project__ - points to a project that can execute the migrations. Cannot run migrations in project without EF Core Tools.
  * __context__ - what context to use for creating migrations.
* Generate SQL script (optional if you don't want to run migration in code)
  ```cmd
  dotnet ef migrations script --startup-project ../Api --context ApplicationDbContext -o e:\temp\initdb.sql
  ```

