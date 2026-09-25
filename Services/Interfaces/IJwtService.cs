using ContractMaster.Models;

namespace ContractMaster.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}