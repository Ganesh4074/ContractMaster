namespace ContractMaster.DTOs;

public record LoginRequestDTO(
    string Email,
    string Password
);