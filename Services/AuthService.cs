using System.Diagnostics.Contracts;
using ContractMaster.Data;
using ContractMaster.DTOs;
using ContractMaster.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.Services;

public class AuthService{
    private readonly ContractMasterContext _db;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly JwtService _jwtService;
    public AuthService(ContractMasterContext db, IPasswordHasher<User> passwordHasher, JwtService jwtService)
    {
        _db=db;
        _passwordHasher=passwordHasher;
        _jwtService=jwtService;

    }

    public async Task<LoginResponseDTO> Login(LoginRequestDTO request)
    {
        if(String.IsNullOrWhiteSpace(request.Email) || String.IsNullOrWhiteSpace(request.Password))
        {
            return null!;
        }
        var user=await _db.Users.FirstOrDefaultAsync(user=>user.EMail==request.Email);
        if(user is null)
        {
            return null;
        }
        var PasswordResult=_passwordHasher.VerifyHashedPassword(user,user.PasswordHash,request.Password);
        if (PasswordResult==PasswordVerificationResult.Failed)
        {
            return null!;
        }
        var Token=_jwtService.GenerateToken(user);
        return new LoginResponseDTO(Token);
    }
}