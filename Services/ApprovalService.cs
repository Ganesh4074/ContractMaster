using ContractMaster.DTOs;
using ContractMaster.Models;
using ContractMaster.Repositories.Interfaces;

namespace ContractMaster.Services;

public class ApprovalService
{
    private readonly IApprovalRepository _repository;

    public ApprovalService(IApprovalRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetApprovalDTO?> GetApprovalById(int id)
    {
        var approval = await _repository.GetByIdAsync(id);

        if (approval is null)
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

        await _repository.AddAsync(approval);
    }

    public async Task<List<GetApprovalDTO>> GetApprovals()
    {
        var approvals = await _repository.GetAllAsync();

        return approvals.Select(approval => new GetApprovalDTO(
            approval.Id,
            approval.Version,
            approval.Sequence,
            approval.ContractId,
            approval.ApproverId,
            approval.ApproverType,
            approval.ApprovalStatus,
            approval.ApprovedAt,
            approval.RejectedAt
        )).ToList();
    }

    public async Task<Approvals?> UpdateApproval(
        int id,
        UpdateApprovalDTO update)
    {
        var approval = await _repository.GetByIdAsync(id);

        if (approval is null)
        {
            return null;
        }

        approval.ApprovalStatus = update.ApprovalStatus;

        if (update.ApprovalStatus == "Approved")
        {
            approval.ApprovedAt = update.Date;
        }
        else if (update.ApprovalStatus == "Rejected")
        {
            approval.RejectedAt = update.Date;
        }
        else
        {
            throw new Exception("Approval Status not allowed");
        }

        await _repository.UpdateAsync(approval);

        return approval;
    }
}