namespace ContractMaster.DTOs;

public record NewCommentDTO(
    int ContractId,
    int Version,
    string CommentText
);