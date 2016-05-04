# aspnet-blog

command
```bash
dnu build
set ASPNET_ENV=Development
dnx web
dnx web --ASPNET_ENV=Development

dnx web --server.urls http://0.0.0.0:81 --ASPNET_ENV Development
```
Custom command in project.json
```bash
"commands": {
    "web": "Microsoft.AspNet.Server.Kestrel",
    "ef": "EntityFramework.Commands",
    "devo": "MyProject --server Microsoft.AspNet.Server.Kestrel --server.urls http://0.0.0.0:5001 --ASPNET_ENV Development",
    "staging": "MyProject --server Microsoft.AspNet.Server.Kestrel --server.urls http://0.0.0.0:5002 --ASPNET_ENV Staging",
    "prod": "MyProject --server Microsoft.AspNet.Server.Kestrel --server.urls http://0.0.0.0:5003 --ASPNET_ENV Production"
},
```

Migration
```bash
dnx ef

dnx ef migrations add init
dnx ef database update
```
