using ContractMaster.DTOs;
using ContractMaster.Services;

namespace ContractMaster.EndPoints;
public static class AuthEndPoints
{
    public static void GetAuthEndPoints(this WebApplication app)
    {
        var group=app.MapGroup("/Auth");
        group.MapPost("/login",async (LoginRequestDTO request,AuthService Auth) =>
        {
            var Response=await Auth.Login(request);
            if(Response is null)
            {
                return Results.Unauthorized();
            }
            return Results.Ok(Response);
        });
    }
}