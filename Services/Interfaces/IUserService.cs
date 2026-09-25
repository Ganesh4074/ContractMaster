using ContractMaster.DTOs;

namespace ContractMaster.Services;

public interface IUserService
{
    Task CreateUser(NewUserDto newUser);

    Task<List<UserDto>> GetUsers();
}