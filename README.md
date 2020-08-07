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
    
    -----------/ApplicationUser.cs  --> User Custom class for Identity User
    
    /Areas
    
    /Views
    
    /Data  --> Database Migrations
    
    /appsetting.json --> Global Envs (Check the specific section)
    
    /ConfigurationManager.cs --> Use a global enviroments
    
    /Dockerfile
    
    /Program 
    
    /Startup  --> Check the specific section
    
    
### Customize  Identity
1.- Create a database

	/DB folder you can found a script

2.- Create a custom Class to User, this class can be modified but you need add the new properties in the table “Aspnetusers” in this case you don’t need do nothing.

3.- You need change this line in the ApplicationDbContext


    public class ApplicationDbContext : IdentityDbContext
    
  
 for this
  
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    
In this case we assume the ApplicationUser is a CustomClass (Step 2)

4.- Add Service Identity in the startup file
	Replace this line 

    Service.AddDefaultIdentity
  
  For this
  
     services.AddIdentity<ApplicationUser, IdentityRole>() 
          .AddEntityFrameworkStores<ApplicationDbContext>() 
          .AddDefaultTokenProviders();
  
5- Add a Custom View

  In this case I added Auth/index and AuthController
  
6.- Inject in the cshtml the contexts

    @using Microsoft.AspNetCore.Identity 

    @inject SignInManager<ApplicationUser> SignInManager 
  
    @inject UserManager<ApplicationUser> UserManager
  
  
7.- Modify the constructor controller

    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    public AuthController(UserManager<ApplicationUser> userManager,	SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
  
8.- Use the signinmanager and usermanager

You can use the usermanager to get all information from the Database.

    var user = await _userManager.FindByEmailAsync(ConfigurationManager.AppSetting["User"]);
           
Sign in, you need set the user want Sign in and Password.

    await _signInManager.PasswordSignInAsync(user, ConfigurationManager.AppSetting["Pwd"], true, false)



### Set up Jwt

1.- Install nugget Microsoft.AspNetCore.Authentication.JwtBearer 

2.-Add this line in the ConfiguredServices() in the startup

            // use Auth
            var key = Encoding.ASCII.GetBytes(ConfigurationManager.AppSetting["Secret_Key"]);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
		
3.- Add a new ApiController in this case AuthenticationController.

4.- Add the method BuildToken 

https://github.com/eddyv73/basesolutioncore/blob/cdf80e0a6873f80c86d52353b0b3151c1b948630/WebBase/WebBase/Controllers/AuthenticationController.cs#L61

5.-Create a mock login 

	Get() --> In the method test you can found the usermanager, remember the login route needs this tag [AllowAnonymous]
	
6.-Add Authorize tag

	[Authorize]
	    
7.-Test the login with you Bearer token here
	
	Get(id) 
	
### Enviroments


  User: Email test User
  
  Pwd: Password test User
  
  Secret_Key = Secret Key JWT
  
  
License
----

Apache 2.0


