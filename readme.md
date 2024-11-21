# create a new webapi project
dotnet new webapi --use-controllers -o .  

# Run
dotnet run


# migrations

dotnet ef migrations add "name"

dotnet ef database update

http://localhost:5242/swagger/index.html



# postgresql connection : 
    "DefaultConnection": "User ID=myuser;Password=mypassword;Host=localhost;Port=5432;Database=car_project;Pooling=true;"

# sqlServer connection : 
    "DefaultConnection": "Server=.\\SQLExpress;Database=TestAuth;Trusted_Connection=True;TrustServerCertificate=True;"

# add Package 
dotnet add package Microsoft.EntityFrameworkCore.SqlServer