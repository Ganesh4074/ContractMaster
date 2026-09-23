namespace ContractMaster.DTOs;

public record CommentDTO(
    int Id,
    int ContractId,
    int Version,
    int UserId,
    string CommentText,
    DateOnly CreatedAt
);