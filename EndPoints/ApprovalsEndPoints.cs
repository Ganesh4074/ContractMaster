using ContractMaster.Data;
using ContractMaster.DTOs;
using ContractMaster.Models;

namespace ContractMaster.EndPoints;
public static class ApprovalsEndPoints
{
    public static void GetApprovalsEndPoints(this WebApplication app)
    {
        var group=app.MapGroup("/Approvals");

        //post an approval
        group.MapPost("/", async (NewApprovalDTO NewApproval, ContractMasterContext DB) =>
        {
            Approvals approval = new()
            {
                Version = NewApproval.Version,
                Sequence = NewApproval.Sequence,
                ContractId = NewApproval.ContractId,
                ApproverId = NewApproval.ApproverId,
                ApproverType = NewApproval.ApproverType,
                ApprovalStatus = NewApproval.ApprovalStatus,
                ApprovedAt = NewApproval.ApprovedAt,
                RejectedAt = NewApproval.RejectedAt
            };
            await DB.AddAsync(approval);
            await DB.SaveChangesAsync();
        });
    }
}