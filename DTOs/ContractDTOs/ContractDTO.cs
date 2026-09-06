namespace ContractMaster.DTOs;

public record ContractDTO(int Id,
    int ContractId,
    int Version,
    string ContracType,
    string Status,
    DateOnly StartDate,
    DateOnly EndDate,
    string CounterPartyName,
    string CounterPartyEmail,
    DateOnly CreatedById
    );