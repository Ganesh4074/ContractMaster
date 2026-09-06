namespace ContractMaster.DTOs;

public record ApprovalDTO(
    int Id,
    int Version,
    int Sequence,
    int ContractId,
    int ApproverId,
    string? ApproverType,
    string? ApprovalStatus,
    DateOnly ApprovedAt,
    DateOnly RejectedAt
);
