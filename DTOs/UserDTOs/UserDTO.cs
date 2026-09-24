namespace ContractMaster.DTOs;

public record UserDto(
    int Id,
    string Name,
    int DepartmentId,
    int RoleId,
    string RoleName,
    string Email
);