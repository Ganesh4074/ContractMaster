using ContractMaster.Data;
using ContractMaster.DTOs;
using ContractMaster.DTOs.ApprovalDTOs;
using ContractMaster.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.Services;

public class UserService
{
    private readonly ContractMasterContext _db;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(ContractMasterContext db, IPasswordHasher<User> passwordHasher)
    {
        _db = db;
        _passwordHasher = passwordHasher;
    }

    public async Task CreateUser(NewUserDto newUser)
    {
        User user = new()
        {
            Name = newUser.Name,
            Department = newUser.Department,
            Role = newUser.Role,
            EMail = newUser.EMail,
            PasswordHash = string.Empty
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, newUser.PasswordHash);
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task<List<UserDto>> GetUsers()
    {
        return await _db.Users.Select(user => new UserDto(
            user.Id,
            user.Name,
            user.Department,
            user.Role,
            user.EMail,
            user.PasswordHash
        )).ToListAsync();
    }

    public async Task<List<PendingApprovalsDTO>> GetPendingApprovals(int id)
    {
        return await _db.Approvals.Where(approval =>
            approval.ApproverId == id && approval.ApprovalStatus == "Pending"
        ).Select(approval => new PendingApprovalsDTO(
            approval.ContractId,
            approval.Version,
            approval.ApproverId,
            approval.Sequence
        )).ToListAsync();
    }
}