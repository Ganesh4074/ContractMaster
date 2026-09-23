using ContractMaster.Models.Enums;

namespace ContractMaster.DTOs;
public record UpdateApprovalDTO(
    ApprovalStatus ApprovalStatus,
    DateOnly Date
);