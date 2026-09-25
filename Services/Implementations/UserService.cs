using ContractMaster.DTOs;
using ContractMaster.Exceptions;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ContractMaster.Services;

public class UserService:IUserService
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IRoleRepository _roleRepository;

    public UserService(IUserRepository repository, IPasswordHasher<User> passwordHasher, IRoleRepository roleRepository)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _roleRepository = roleRepository;
    }

    public async Task CreateUser(NewUserDto newUser)
    {
        var existingUser = await _repository.GetByEmailAsync(
            newUser.EMail);

        if (existingUser is not null)
        {
            throw new BusinessRuleException("User with this email already exists.");
        }

        var role = await _roleRepository.GetByIdAsync(newUser.RoleId);

        if (role is null)
        {
            throw new BusinessRuleException("Invalid role.");
        }

        User user = new()
        {
            Name = newUser.Name,
            DepartmentId = newUser.DepartmentId,
            RoleId = newUser.RoleId,
            EMail = newUser.EMail,
            PasswordHash = string.Empty
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, newUser.Password);

        await _repository.AddAsync(user);
    }

    public async Task<List<UserDto>> GetUsers()
    {
        var users = await _repository.GetAllAsync();

        return users.Select(user => new UserDto(
            user.Id,
            user.Name,
            user.DepartmentId,
            user.RoleId,
            user.Role.Name,
            user.EMail
        )).ToList();
    }
}