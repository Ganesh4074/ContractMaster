using ContractMaster.Data;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ContractMasterContext _db;

    public CommentRepository(ContractMasterContext db)
    {
        _db = db;
    }

    public async Task<List<Comment>> GetByContractIdVersionAsync(int contractId, int version)
    {
        return await _db.Comments.Where(comment =>
                comment.ContractId == contractId &&
                comment.Version == version).ToListAsync();
    }

    public async Task AddAsync(Comment comment)
    {
        await _db.Comments.AddAsync(comment);
        await _db.SaveChangesAsync();
    }
    public async Task<List<Comment>> GetCommentsAsync()
    {
        return await _db.Comments.ToListAsync();
    }
}