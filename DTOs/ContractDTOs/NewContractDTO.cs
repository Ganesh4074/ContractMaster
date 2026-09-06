namespace ContractMaster.DTOs;

public record NewContractDTO(
    int ContractId,
    int Version,
    string ContracType,
    string Status,
    DateOnly StartDate,
    DateOnly EndDate,
    string CounterPartyName,
    string CounterPartyEmail,
    int CreatedById,
    DateOnly CreatedAt
    );