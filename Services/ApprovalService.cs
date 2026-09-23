using ContractMaster.DTOs;
using ContractMaster.Models;
using ContractMaster.Models.Enums;
using ContractMaster.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ContractMaster.Services;

public class ApprovalService
{
    private readonly IApprovalRepository _repository;
    private readonly SignatureService _signatureService;

    public ApprovalService(
        IApprovalRepository repository,
        SignatureService signatureService)
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

    public async Task<GetApprovalDTO?> UpdateApproval(
        int id,
        UpdateApprovalDTO update)
    {
        var approval = await _repository.GetByIdAsync(id);

        if (approval is null)
        {
            return null;
        }

        approval.ApprovalStatus = update.ApprovalStatus;

        if (update.ApprovalStatus == ApprovalStatus.Approved)
        {
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
            throw new Exception("Approval status not allowed.");
        }

        await _repository.UpdateAsync(approval);

        if (update.ApprovalStatus == ApprovalStatus.Approved)
        {
            var approvals = await _repository
                .GetByContractAndVersionAsync(
                    approval.ContractId,
                    approval.Version);

            var allApproved =
                approvals.Count == 3 &&
                approvals.All(a =>
                    a.ApprovalStatus == ApprovalStatus.Approved);

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