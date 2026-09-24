using ContractMaster.Models.Enums;

namespace ContractMaster.DTOs;

public record NewApprovalDTO(
    int Version,
    int Sequence,
    int ContractId,
    ApproverType ApproverType
);