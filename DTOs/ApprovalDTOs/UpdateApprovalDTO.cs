namespace ContractMaster.DTOs;
public record UpdateApprovalDTO(
    string ApprovalStatus,
    DateOnly Date
);