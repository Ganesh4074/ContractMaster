using ContractMaster.DTOs;
using ContractMaster.Models.Enums;

namespace ContractMaster.Services.Interfaces;

public interface IApprovalService
{
    public Task<GetApprovalDTO?> GetApprovalById(int id);
    public Task CreateApproval(NewApprovalDTO newApproval);
    public Task<List<GetApprovalDTO>> GetApprovals();
    public  Task<GetApprovalDTO?> UpdateApproval(int id, UpdateApprovalDTO update, int userId, string userRole);
    Task<List<GetApprovalDTO>> GetApprovalsByType(ApproverType approverType);
}