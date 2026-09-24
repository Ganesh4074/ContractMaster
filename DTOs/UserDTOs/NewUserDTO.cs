namespace ContractMaster.DTOs;

public record NewUserDto(
    string Name,
    int DepartmentId,
    int RoleId,
    string EMail,
    string Password
);