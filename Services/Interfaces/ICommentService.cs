namespace ContractMaster.Services.Interfaces;
using ContractMaster.DTOs;

public interface ICommentService
{
    public Task<List<CommentDTO>> GetCommentsById(int contractId, int version);
    public Task CreateComment(NewCommentDTO newComment, int userId);
    public Task<List<CommentDTO>> GetCommentsAsync();

}