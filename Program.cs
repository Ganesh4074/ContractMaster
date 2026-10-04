using System.Text;
using ContractMaster.Configurations;
using ContractMaster.Data;
using ContractMaster.Exceptions;
using ContractMaster.Models;
using ContractMaster.Repositories;
using ContractMaster.Repositories.Interfaces;
using ContractMaster.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using ContractMaster.Services.Interfaces;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

builder.Services.AddValidation();

builder.AddData();

// builder.Services.Configure<JWTSettings>(
//     builder.Configuration.GetSection("JwtConfig")
// );

// var jwtSettings = builder.Configuration
//     .GetSection("JwtConfig")
//     .Get<JWTSettings>()
//     ?? throw new InvalidOperationException("Jwt configuration missing");


builder.Services.AddScoped<IApprovalRepository, ApprovalRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IApprovalRepository, ApprovalRepository>();
builder.Services.AddScoped<ISignatureRepository, SignatureRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();


builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();
builder.Services.AddScoped<IApprovalService, ApprovalService>();
builder.Services.AddScoped<IContractService,ContractService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<ISignatureService,SignatureService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IJwtService,JwtService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<ICommentService,CommentService>();
builder.Services.AddScoped<IApprovalService,ApprovalService>();
builder.Services.AddScoped<IContractService,ContractService>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IDepartmentService,DepartmentService>();
builder.Services.AddScoped<IPdfService, PdfService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(
        jwtOptions =>
        {
            // JWT bearer configuration if you need any
        },
        identityOptions =>
        {
            identityOptions.Instance =
                Environment.GetEnvironmentVariable("AZURE_INSTANCE")
                ?? throw new InvalidOperationException("AZURE_INSTANCE missing");

            identityOptions.TenantId =
                Environment.GetEnvironmentVariable("AZURE_TENANT_ID")
                ?? throw new InvalidOperationException("AZURE_TENANT_ID missing");

            identityOptions.ClientId =
                Environment.GetEnvironmentVariable("AZURE_CLIENT_ID")
                ?? throw new InvalidOperationException("AZURE_CLIENT_ID missing");
        });
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,

//             ValidIssuer = jwtSettings?.Issuer,
//             ValidAudience = jwtSettings?.Audience,


//             IssuerSigningKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(jwtSettings.Key)
//             )
//         };
//     });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
var clientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID")
    ?? throw new InvalidOperationException("AZURE_CLIENT_ID missing");

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2",
        new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl =
                        new Uri("https://login.microsoftonline.com/bdcfaa46-3f69-4dfd-b3f7-c582bdfbb820/oauth2/v2.0/authorize"),

                    TokenUrl =
                        new Uri("https://login.microsoftonline.com/bdcfaa46-3f69-4dfd-b3f7-c582bdfbb820/oauth2/v2.0/token"),
                    
                    Scopes = new Dictionary<string, string>
                    {
                        {
                            "api://c95c97ec-156c-4cc4-81bd-d23f0daa8ed5/access_as_user",
                            "Access ContractMaster API"
                        }
                    }
                }
            }
        });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [
                new OpenApiSecuritySchemeReference("oauth2", document)
            ] = []
        });
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "ContractMaster API"
    );

    options.OAuthClientId(
        Environment.GetEnvironmentVariable("AZURE_CLIENT_ID")
        ?? throw new InvalidOperationException("AZURE_CLIENT_ID missing")
    );

    options.OAuthUsePkce();
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MigrateDB();

app.Run();


