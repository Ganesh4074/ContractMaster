namespace ContractMaster.DTOs;
using ContractMaster.Models;
public record NewApprovalDTO(
    int ContractId,
    int ApproverId,
    string? ApproverType,
    string? ApprovalStatus
);