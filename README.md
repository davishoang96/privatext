# privatext
One time private text

Run this command to create migration
``` powershell
dotnet ef migrations add migration_name --verbose --project privatext.Database --startup-project privatext
```

Run this to apply migration
``` powershell 
dotnet ef database update --verbose --project privatext.Database --startup-project privatext
```

Generate swagger endpoint

```
dotnet run --generateclients true
```