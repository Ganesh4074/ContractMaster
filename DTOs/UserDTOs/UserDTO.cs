namespace ContractMaster.DTOs;

public record UserDto(
    int Id,
    string Name,
    string Department,
    string Role
);