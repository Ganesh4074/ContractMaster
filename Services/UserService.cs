using ContractMaster.DTOs;
using ContractMaster.DTOs.ApprovalDTOs;
using ContractMaster.Exceptions;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ContractMaster.Services;

public class UserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        IUserRepository repository,
        IPasswordHasher<User> passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task CreateUser(NewUserDto newUser)
    {
        if(await _repository.GetByEmailAsync(newUser.EMail)!=null)
        {
            throw new BusinessRuleException("user with email already exists");
        }
        User user = new()
        {
            Name = newUser.Name,
            DepartmentId = newUser.DepartmentId,
            Role = newUser.Role,
            EMail = newUser.EMail,
            PasswordHash = string.Empty
        };

        user.PasswordHash = _passwordHasher.HashPassword(
            user,
            newUser.Password
        );

        await _repository.AddAsync(user);
    }

    public async Task<List<UserDto>> GetUsers()
    {
        var users = await _repository.GetAllAsync();

        return users.Select(user => new UserDto(
            user.Id,
            user.Name,
            user.DepartmentId,
            user.Role,
            user.EMail
        )).ToList();
    }

    public async Task<List<PendingApprovalsDTO>> GetPendingApprovals()
    {
        var approvals = await _repository.GetPendingApprovalsAsync();

        return approvals.Select(approval => new PendingApprovalsDTO(
            approval.ContractId,
            approval.Version,
            approval.ApproverId,
            approval.Sequence
        )).ToList();
    }
}