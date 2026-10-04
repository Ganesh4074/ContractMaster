using ContractMaster.DTOs;
using ContractMaster.Exceptions;
using ContractMaster.Models;
using ContractMaster.Models.Enums;
using ContractMaster.Repositories.Interfaces;
using ContractMaster.Services.Interfaces;

namespace ContractMaster.Services;

public class ApprovalService:IApprovalService
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
            ApproverId = 0,
            ApproverType = newApproval.ApproverType,
            ApprovalStatus = 0
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

    public async Task<GetApprovalDTO?> UpdateApproval(int id, UpdateApprovalDTO update, int userId, string userRole)
    {
        var approval = await _repository.GetByIdAsync(id);

        if (approval is null)
        {
            return null;
        }

        var isAuthorized = approval.ApproverType switch
        {
            ApproverType.Legal => userRole == "LegalApprover",
            ApproverType.Internal => userRole == "InternalApprover",
            ApproverType.External => userRole == "ExternalApprover",
            _ => false
        };

        if (!isAuthorized)
        {
            throw new BusinessRuleException("You are not authorized to approve this request.");
        }
        if (approval.ApprovalStatus != ApprovalStatus.Pending)
        {
            throw new BusinessRuleException("This approval has already been processed.");
        }

        if (update.ApprovalStatus == ApprovalStatus.Approved)
        {
            if (approval.Sequence > 1)
            {
                var approvals =await _repository.GetByContractAndVersionAsync(approval.ContractId, approval.Version);

                var previousApproval = approvals.FirstOrDefault(item => item.Sequence == approval.Sequence - 1);

                if (previousApproval is null || previousApproval.ApprovalStatus != ApprovalStatus.Approved)
                {
                    throw new BusinessRuleException("Previous approval is still pending.");
                }
            }

            approval.ApprovedAt = update.Date;
            approval.ApproverId = userId;
            approval.RejectedAt = null;
        }
        else if (update.ApprovalStatus == ApprovalStatus.Rejected)
        {
            approval.RejectedAt = update.Date;
            approval.ApproverId = userId;
            approval.ApprovedAt = null;
        }
        else
        {
            throw new BusinessRuleException("Approval status not allowed.");
        }

        approval.ApprovalStatus = update.ApprovalStatus;

        await _repository.UpdateAsync(approval);

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
    public async Task<List<GetApprovalDTO>> GetApprovalsByType(ApproverType approverType)
    {
        var approvals = await _repository.GetByApproverTypeAsync(approverType);

        return approvals.Select(approval => new GetApprovalDTO(
            approval.Id,
            approval.ContractId,
            approval.Version,
            approval.Sequence,
            approval.ApproverId,
            approval.ApproverType,
            approval.ApprovalStatus,
            approval.ApprovedAt,
            approval.RejectedAt
        )).ToList();
    }
}