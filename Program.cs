using System.Security;
using System.Text;
using ContractMaster.Configurations;
using ContractMaster.Data;
using ContractMaster.EndPoints;
using ContractMaster.Models;
using ContractMaster.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
builder.AddData();
builder.Services.Configure<JWTSettings>(
    builder.Configuration.GetSection("JwtConfig")
);
var jwtSettings=builder.Configuration.GetSection("JwtConfig").Get<JWTSettings>()?? throw new
InvalidOperationException("Jwt congiguration missing");

builder.Services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(Options=>
{
    Options.TokenValidationParameters=new TokenValidationParameters
    {
        ValidateIssuer=true,
        ValidateAudience=true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer=jwtSettings.Issuer,
        ValidAudience=jwtSettings.Audience,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
    {
        Name="Authorization",
        Type=Microsoft.OpenApi.SecuritySchemeType.Http,
        Scheme="bearer",
        BearerFormat="JWT",
        In=ParameterLocation.Header,
        Description="Enter Your JWT token"
    });
    options.AddSecurityRequirement(document=>
    new OpenApiSecurityRequirement
    {
        [

        new OpenApiSecuritySchemeReference("Bearer",document)
        ]=[]
    });
});
builder.Services.AddControllers();
builder.Services.AddScoped<ApprovalService>();
var app = builder.Build();
app.MapControllers();
app.GetUserEndPoints();
app.GetContractEndPoints();
app.GetApprovalsEndPoints();
app.GetAuthEndPoints();
app.MigrateDB();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.Run();

