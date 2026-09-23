using ContractMaster.Models.Enums;

namespace ContractMaster.DTOs;

public record ContractDTO(int Id,
    int ContractId,
    int Version,
    ContractType ContractType,
    ContractStatus Status,
    DateOnly StartDate,
    DateOnly EndDate,
    string CounterPartyName,
    string CounterPartyEmail,
    DateOnly CreatedById
    );