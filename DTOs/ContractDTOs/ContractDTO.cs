using ContractMaster.Models.Enums;

namespace ContractMaster.DTOs;

public record ContractDTO(int Id,
    int ContractNumber,
    int Version,
    ContractType ContractType,
    ContractStatus Status,
    DateOnly StartDate,
    DateOnly EndDate,
    string CreatedByEmail,
    string CounterPartyName,
    string CounterPartyEmail,
    DateOnly CreatedById
    );