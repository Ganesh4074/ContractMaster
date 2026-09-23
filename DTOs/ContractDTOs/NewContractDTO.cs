using ContractMaster.Models.Enums;

namespace ContractMaster.DTOs;

public record NewContractDTO(
    int ContractId,
    int Version,
    ContractType ContractType,
    ContractStatus Status,
    DateOnly StartDate,
    DateOnly EndDate,
    string CounterPartyName,
    string CounterPartyEmail,
    int CreatedById,
    DateOnly CreatedAt
    );