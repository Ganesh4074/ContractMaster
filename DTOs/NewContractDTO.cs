namespace ContractMaster.DTOs;

public record NewContractDTO(
    int ContractId,
    string ContracType,
    string status,
    DateOnly StartDate,
    DateOnly EndDate,
    string CounterPartyName,
    string CounterPartyEmail,
    string Name
    );