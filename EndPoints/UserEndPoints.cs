using ContractMaster.Data;
using ContractMaster.DTOs;
using ContractMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.EndPoints;

public static class UserEndPoints
{
    public static void GetUserEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("/Users");
        group.MapPost("/",async (NewUserDto NewUser, ContractMasterContext DB) =>
        {
            User User=new()
            {
                Name=NewUser.Name,
                Department=NewUser.Department,
                Role=NewUser.Role
            };
            await DB.Users.AddAsync(User);
            await DB.SaveChangesAsync();
        });
        group.MapGet("/", async(ContractMasterContext DB) =>
        {
           return await DB.Users.Select(user=> new UserDto(
                user.Id,
                user.Name,
                user.Department,
                user.Role
            )).ToListAsync();
        });
    }
}