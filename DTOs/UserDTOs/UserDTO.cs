namespace ContractMaster.DTOs;

public record UserDto(
    int Id,
    string Name,
    int DepartmentId,
    string Role,
    string Email
    );