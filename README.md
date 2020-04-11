# Base Solution Core

[![N|Solid](https://programaenlinea.net/wp-content/uploads/2019/05/net-core.png)](https://github.com/eddyv73/basesolutioncore)

![Dotnet build CI](https://github.com/eddyv73/basesolutioncore/workflows/Dotnet%20build%20CI/badge.svg)

This is a main template for start a new project and see how configurate a real solution with:

  - Jwt
  - Identity Core
  - Identity Core with Jwt
  - Net Core 3.1
  - Dockers
  
Current Versions
- Microsoft.AspNetCore.Authentication.JwtBearer  Version="3.1.3" 
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore Version="3.1.3"
- Microsoft.AspNetCore.Identity.EntityFrameworkCore Version="3.1.3"
- Microsoft.AspNetCore.Identity.UI Version="3.1.3"
- Microsoft.EntityFrameworkCore.SqlServer Version="3.1.3"
- Microsoft.EntityFrameworkCore.Tools Version="3.1.3"
- Microsoft.VisualStudio.Azure.Containers.Tools.Targets Version="1.10.8"
- Microsoft.VisualStudio.Web.CodeGeneration.Design Version="3.1.2"


Structure of solution:

  **Only mention a important files**
  
In root folder

cd /

        /DB     ->   SQL script to create a DB
        
        /WebBase    --> WebApp Solution
        
        /.github    --> Yaml Github Action for dotnet core 3.1
    
cd /WebBase/

    /Controllers
    
    ------------/AuthController.cs 
    
                -------------------------------/Index() -->This example you can see, the get user with _userManager and Login With Password
                
    ------------/AuthenticationController.cs
    
                ------------------------------/Get() -->This example you can see, the get user with _userManager and Login
                
                ------------------------------/Get(id) --> Login with Beare Token
                
                ------------------------------/BuildTOken() --> Token Creation
                
    ------------/HomeController.cs
    
    /Model
    
    -----------/ApplicationUser.cs  --> User Custom clas for Identity User
    
    /Areas
    
    /Views
    
    /Data  --> Database Migrations
    
    /appsetting.json --> Global Envs (Check the specific section)
    
    /ConfigurationManager.cs --> Use a global enviroments
    
    /Dockerfile
    
    /Program 
    
    /Startup  --> Check the specific section
    
    
### Customize a Identity


### Set up Jwt

License
----

Apache 2.0


