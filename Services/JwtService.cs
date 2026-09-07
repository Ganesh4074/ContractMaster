using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ContractMaster.Configurations;
using ContractMaster.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ContractMaster.Services;

public class JwtService
{
    private readonly JWTSettings _settings;
    public JwtService(IOptions<JWTSettings> settings)
    {
        _settings=settings.Value;
    }
    public string GenerateToken(User user)
    {
        var Claims = new[]
        {
            new Claim(
                JwtRegisteredClaimNames.Sub,user.Id.ToString()
            ),
            new Claim(
                JwtRegisteredClaimNames.Email,user.EMail.ToString()
            ),
            new Claim(
                ClaimTypes.Role,user.Role.ToString()
            )
        };
        var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));

        var Credentials=new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var Token=new JwtSecurityToken(
            issuer:_settings.Issuer,
            audience:_settings.Audience,
            claims:Claims,
            expires:DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
            signingCredentials:Credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(Token);
    }
}