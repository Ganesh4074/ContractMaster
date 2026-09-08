using ContractMaster.Data;
using ContractMaster.DTOs;
using ContractMaster.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractMaster.Services;

public class ApprovalService
{
    private readonly ContractMasterContext _db;
    public ApprovalService(ContractMasterContext db)
    {
        _db=db;
    }
    public async Task<GetApprovalDTO?> GetApprovalById(int id)
    {
        var approval=await _db.Approvals.FindAsync(id);
        if(approval is null)
        {
            return null;
        }
        return new GetApprovalDTO(
            approval.Id,
            approval.Version,
            approval.Sequence,
            approval.ContractId,
            approval.ApproverId,
            approval.ApproverType,
            approval.ApprovalStatus,
            approval.ApprovedAt,
            approval.RejectedAt

        );
    }

    public async Task CreateApproval(NewApprovalDTO newApproval)
    {
        Approvals approval = new()
        {
            Version = newApproval.Version,
            Sequence = newApproval.Sequence,
            ContractId = newApproval.ContractId,
            ApproverId = newApproval.ApproverId,
            ApproverType = newApproval.ApproverType,
            ApprovalStatus = newApproval.ApprovalStatus
        };
        await _db.Approvals.AddAsync(approval);
        await _db.SaveChangesAsync();
    }

    public async Task<List<GetApprovalDTO>> GetApprovals()
    {
        return await _db.Approvals.Select(approval => new GetApprovalDTO(
            approval.Id,
            approval.Version,
            approval.Sequence,
            approval.ContractId,
            approval.ApproverId,
            approval.ApproverType,
            approval.ApprovalStatus,
            approval.ApprovedAt,
            approval.RejectedAt
        )).ToListAsync();
    }

    public async Task<Approvals?> UpdateApproval(int id, UpdateApprovalDTO update)
    {
        var approval = await _db.Approvals.FindAsync(id);
        if(approval is null)
        {
            return null;
        }
        approval.ApprovalStatus = update.ApprovalStatus;
        await _db.SaveChangesAsync();
        return approval;
    }
}