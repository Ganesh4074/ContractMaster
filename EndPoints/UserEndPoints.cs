using System.Reflection.Metadata.Ecma335;
using ContractMaster.Data;
using ContractMaster.DTOs;
using ContractMaster.DTOs.ApprovalDTOs;
using ContractMaster.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.EndPoints;

public static class UserEndPoints
{
    public static void GetUserEndPoints(this WebApplication app)
    {
        PasswordHasher<User> passwordHasher=new ();
        var group = app.MapGroup("/Users");
        group.MapPost("/",async (NewUserDto NewUser, ContractMasterContext DB) =>
        {
            User user=new()
            {
                Name=NewUser.Name,
                Department=NewUser.Department,
                Role=NewUser.Role,
                EMail=NewUser.EMail,
                PasswordHash=String.Empty
            };
            user.PasswordHash=passwordHasher.HashPassword(user, NewUser.PasswordHash);
            await DB.Users.AddAsync(user);
            await DB.SaveChangesAsync();
        });
        group.MapGet("/", async(ContractMasterContext DB) =>
        {
           return await DB.Users.Select(user=> new UserDto(
                user.Id,
                user.Name,
                user.Department,
                user.Role,
                user.EMail,
                user.PasswordHash
            )).ToListAsync();
        });

        //get pending approvals
        group.MapGet("/Approver/{id}/pending", async (int id, ContractMasterContext Db) =>
        {
            return await Db.Approvals.Where(approval =>
            
                approval.ApproverId==id && approval.ApprovalStatus=="Pending"
            ).Select(approvals=> new PendingApprovalsDTO(
                approvals.ContractId,
                approvals.Version,
                approvals.ApproverId,
                approvals.Sequence
            )).ToListAsync();
        });
    }
}