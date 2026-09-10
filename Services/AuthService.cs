using ContractMaster.DTOs;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ContractMaster.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly JwtService _jwtService;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        JwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return null!;
        }

        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
        {
            return null!;
        }

        var passwordResult = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password
        );

        if (passwordResult == PasswordVerificationResult.Failed)
        {
            return null!;
        }

        var token = _jwtService.GenerateToken(user);

        return new LoginResponseDTO(token);
    }
}