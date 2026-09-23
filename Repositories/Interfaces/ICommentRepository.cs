using ContractMaster.Models;

namespace ContractMaster.Repositories.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetByContractIdVersionAsync(int contractId, int version);
    Task AddAsync(Comment comment);
}