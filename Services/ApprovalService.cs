using System.Data.Common;
using ContractMaster.Data;
using ContractMaster.DTOs;

namespace ContractMaster.Services;

public class ApprovalService
{
    private readonly ContractMasterContext _db;
    public ApprovalService(ContractMasterContext db)
    {
        _db=db;
    }
    public async Task<GetApprovalDTO> GetApprovalById(int id)
    {
        var approval=await _db.Approvals.FindAsync(id);
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