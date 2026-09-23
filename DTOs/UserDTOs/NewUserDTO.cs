namespace ContractMaster.DTOs;
public record NewUserDto(
    string Name,
    string Department,
    string Role,
    string EMail,
    string Password
);