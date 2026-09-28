using ContractMaster.DTOs;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;
using ContractMaster.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace ContractMaster.Services;

public class CommentService:ICommentService
{
    private readonly ICommentRepository _repository;

    public CommentService(ICommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CommentDTO>> GetCommentsById(int contractId, int version)
    {
        var comments = await _repository.GetByContractIdVersionAsync(contractId, version);

        return comments.Select(comment => new CommentDTO(
                comment.Id,
                comment.ContractId,
                comment.Version,
                comment.UserId,
                comment.CommentText,
                comment.CreatedAt)).ToList();
    }

    public async Task CreateComment(NewCommentDTO newComment, int userId)
    {
        var comment = new Comment
        {
            ContractId = newComment.ContractId,
            Version=newComment.Version,
            UserId = userId,
            CommentText = newComment.CommentText,
            CreatedAt = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        await _repository.AddAsync(comment);
    }
    public async Task<List<CommentDTO>> GetCommentsAsync()
    {
        var comments=await _repository.GetCommentsAsync();
        return comments.Select(comment=>new CommentDTO(
            comment.Id,
            comment.ContractId,
            comment.Version,
            comment.UserId,
            comment.CommentText,
            comment.CreatedAt
        )).ToList();
    }
}