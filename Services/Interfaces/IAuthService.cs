using ContractMaster.DTOs;

namespace ContractMaster.Services.Interfaces;

public interface IAuthService
{
    public Task<LoginResponseDTO?> Login(LoginRequestDTO request);
    
}