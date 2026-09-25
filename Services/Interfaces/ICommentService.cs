namespace ContractMaster.Services.Interfaces;
using ContractMaster.DTOs;

public interface ICommentService
{
    public Task<List<CommentDTO>> GetComments(int contractId, int version);
    public Task CreateComment(NewCommentDTO newComment, int userId);
}