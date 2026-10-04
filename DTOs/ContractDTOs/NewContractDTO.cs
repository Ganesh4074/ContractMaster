using ContractMaster.Models.Enums;

namespace ContractMaster.DTOs;

public record NewContractDTO(
    int ContractNumber,
    int Version,
    ContractType ContractType,
    ContractStatus Status,
    DateOnly StartDate,
    DateOnly EndDate,
    string CounterPartyName,
    string CounterPartyEmail,
    DateOnly CreatedAt
    );