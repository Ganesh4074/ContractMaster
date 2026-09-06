namespace ContractMaster.DTOs;

public record ContractDTO(int Id,
    int ContractId,
    string ContracType,
    string status,
    DateOnly StartDate,
    DateOnly EndDate,
    string CounterPartyName,
    string CounterPartyEmail
    );