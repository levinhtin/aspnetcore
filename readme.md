# Docker ASP.NET Core

```Docker image: 
docker pull microsoft/aspnetcore
docker run --name aspnetcore -t -d -p 81:81 -v /home/levinhtin/Project/aspnetcore:/projects/aspnetcore microsoft/aspnetcore
docker exec -it aspnetcore bash
cd projects/aspnetcore

dotnet restore
dotnet build
dotnet run --server.urls http://0.0.0.0:81 --ASPNET_ENV Development
```

# Command Line
```command
dotnet restore
dotnet build
dotnet watch run --environment "Development" --server.urls "http://localhost:7000"
```