using ContractMaster.DTOs;
using ContractMaster.Exceptions;
using ContractMaster.Models;
using ContractMaster.Models.Enums;
using ContractMaster.Repositories.Interfaces;

namespace ContractMaster.Services;

public class ApprovalService
{
    private readonly IApprovalRepository _repository;
    private readonly SignatureService _signatureService;

    public ApprovalService(IApprovalRepository repository, SignatureService signatureService)
    {
        _repository = repository;
        _signatureService = signatureService;
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

    public async Task<GetApprovalDTO?> UpdateApproval(int id, UpdateApprovalDTO update, string userRole)
    {
        var approval = await _repository.GetByIdAsync(id);

        if (approval is null)
        {
            return null;
        }
        //To check if the Current user is allowed to approve a particular record
        var isAuthorized = approval.ApproverType switch
        {
            ApproverType.Legal => userRole == "Legal Approver",
            ApproverType.Internal => userRole == "Internal Approver",
            ApproverType.External => userRole == "External Approver",
            _ => false
        };

        if (!isAuthorized)
        {
            throw new BusinessRuleException("You are not authorized to approve this request.");
        }

        if (update.ApprovalStatus == ApprovalStatus.Approved)
        {
            if (approval.Sequence > 1)
            {
                var approvals = await _repository.GetByContractAndVersionAsync(approval.ContractId, approval.Version);

                var previousApproval = approvals.FirstOrDefault(a => 
                    a.Sequence == approval.Sequence - 1);

                if (previousApproval is null || previousApproval.ApprovalStatus != ApprovalStatus.Approved)
                {
                    throw new BusinessRuleException(
                        "Previous approval is still pending.");
                }
            }

            approval.ApprovedAt = update.Date;
            approval.RejectedAt = null;
        }
        else if (update.ApprovalStatus == ApprovalStatus.Rejected)
        {
            approval.RejectedAt = update.Date;
            approval.ApprovedAt = null;
        }
        else
        {
            throw new BusinessRuleException(
                "Approval status not allowed.");
        }

        approval.ApprovalStatus = update.ApprovalStatus;

        await _repository.UpdateAsync(approval);

        if (update.ApprovalStatus == ApprovalStatus.Approved)
        {
            var approvals =
                await _repository.GetByContractAndVersionAsync(
                    approval.ContractId,
                    approval.Version);

            var allApproved = approvals.All(
                a => a.ApprovalStatus == ApprovalStatus.Approved);

            if (allApproved)
            {
                await _signatureService.CreateSignatures(
                    approval.ContractId,
                    approval.Version);
            }
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
}