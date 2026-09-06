namespace ContractMaster.DTOs;
using ContractMaster.Models;
public record NewApprovalDTO(
    int Version,
    int Sequence,
    int ContractId,
    int ApproverId,
    string? ApproverType,
    string? ApprovalStatus,
    DateOnly ApprovedAt,
    DateOnly RejectedAt
);