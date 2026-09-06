namespace ContractMaster.DTOs.ApprovalDTOs;

public record PendingApprovalsDTO(
    int ContractId,
    int Version,
    int ApproverId,
    int Sequence );