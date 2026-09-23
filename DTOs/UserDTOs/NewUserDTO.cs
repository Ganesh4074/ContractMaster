namespace ContractMaster.DTOs;
public record NewUserDto(
    string Name,
    int DepartmentId,
    string Role,
    string EMail,
    string Password
);